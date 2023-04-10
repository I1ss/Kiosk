using Kiosk.JwtAuthenticationManager;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerForOcelotUI();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.UseOcelot();
app.Run();