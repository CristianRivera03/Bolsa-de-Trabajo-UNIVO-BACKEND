using PortalTrabajo.IOC;
using Scalar.AspNetCore;
using System.IO;
using System.Linq;
var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
{
    foreach (var line in File.ReadAllLines(envPath))
    {
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
        var parts = line.Split('=', 2);
        if (parts.Length == 2)
        {
            var key = parts[0].Trim();
            var val = parts[1].Trim();
            if (val.StartsWith("\"") && val.EndsWith("\"") && val.Length > 1) val = val[1..^1];
            else if (val.StartsWith("'") && val.EndsWith("'") && val.Length > 1) val = val[1..^1];
            Environment.SetEnvironmentVariable(key, val);
        }
    }
}
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.DependencyInjection(builder.Configuration);
var app = builder.Build();
app.UseCors("NewPolicy");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Portal Trabajo API")
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecurityScheme = "Bearer"
        };
    });
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
