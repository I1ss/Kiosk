using Microsoft.EntityFrameworkCore;

using System.Reflection;

using Kiosk.DeliveryWebApi;
using Kiosk.DeliveryWebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDefault"));
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