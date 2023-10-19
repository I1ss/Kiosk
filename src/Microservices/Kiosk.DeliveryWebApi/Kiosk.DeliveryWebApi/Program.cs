using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using MassTransit;

using System.Reflection;

using Kiosk.DeliveryWebApi;
using Kiosk.DeliveryWebApi.Repositories;
using Kiosk.Core.Requests;
using Kiosk.DeliveryWebApi.Consumer;
using Kiosk.JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddMassTransit(options =>
{
    options.AddBus(context => Bus.Factory.CreateUsingRabbitMq(configuration =>
    {
        configuration.Host("45.140.19.120", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configuration.ReceiveEndpoint("Delivery", e =>
        {
            e.ConfigureConsumer(context, typeof(CreateIssueConsumer));
            e.ConfigureConsumer(context, typeof(UpdateIssueConsumer));
            e.ConfigureConsumer(context, typeof(DeleteIssueConsumer));
        });
    }));

    options.AddConsumer<CreateIssueConsumer>();
    options.AddConsumer<UpdateIssueConsumer>();
    options.AddConsumer<DeleteIssueConsumer>();
    options.AddRequestClient<UpdateOrderRequest>();
    options.AddRequestClient<GetProductsInOrderRequest>();
});

builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDefault"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IIssueRepository, IssueRepository>();

var app = builder.Build();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceScope = serviceScopeFactory.CreateScope();
var productDbContext = serviceScope.ServiceProvider.GetService<DeliveryDbContext>();

if (productDbContext != null)
    productDbContext.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("swagger/v1/swagger.json", "Delivery API");
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();