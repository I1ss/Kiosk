using Microsoft.EntityFrameworkCore;

using MassTransit;

using Kiosk.OrdersWebApi;
using Kiosk.OrdersWebApi.Repositories;
using Kiosk.Core.Requests;
using Kiosk.OrdersWebApi.Consumers;

using System.Reflection;

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
        configuration.ReceiveEndpoint("Orders", e =>
        {
            e.ConfigureConsumer(context, typeof(UpdateOrderConsumer));
            e.ConfigureConsumer(context, typeof(GetProductsInOrderConsumer));
        });
    }));

    options.AddConsumer<UpdateOrderConsumer>();
    options.AddConsumer<GetProductsInOrderConsumer>();
    options.AddRequestClient<UpdateOrderRequest>();
    options.AddRequestClient<CreateIssueRequest>();
    options.AddRequestClient<UpdateIssueRequest>();
    options.AddRequestClient<DeleteIssueRequest>();
});

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDefault"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductInOrderRepository, ProductInOrderRepository>();

var app = builder.Build();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceScope = serviceScopeFactory.CreateScope();
var productDbContext = serviceScope.ServiceProvider.GetService<OrderDbContext>();

if (productDbContext != null)
    productDbContext.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("swagger/v1/swagger.json", "Order API");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();