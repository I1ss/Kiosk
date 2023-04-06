using Microsoft.EntityFrameworkCore;

using MassTransit;

using System.Reflection;

using Kiosk.DeliveryWebApi;
using Kiosk.DeliveryWebApi.Repositories;
using Kiosk.Core.Requests;
using Kiosk.DeliveryWebApi.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddMassTransit(options =>
{
    options.AddBus(context => Bus.Factory.CreateUsingRabbitMq(configuration =>
    {
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

app.UseAuthorization();
app.MapControllers();

app.Run();