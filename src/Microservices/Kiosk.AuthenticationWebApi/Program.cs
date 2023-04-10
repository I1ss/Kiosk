using Microsoft.EntityFrameworkCore;

using System.Reflection;

using Kiosk.AuthenticationWebApi;
using Kiosk.AuthenticationWebApi.Repositories;
using Kiosk.JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AuthenticationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDefault"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceScope = serviceScopeFactory.CreateScope();
var productDbContext = serviceScope.ServiceProvider.GetService<AuthenticationDbContext>();

if (productDbContext != null)
    productDbContext.Database.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("swagger/v1/swagger.json", "Authentication API");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();