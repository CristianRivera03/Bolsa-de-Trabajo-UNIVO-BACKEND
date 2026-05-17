using PortalTrabajo.IOC;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Agregando CQRS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//se llama a la dependecias IOC
builder.Services.DependencyInjection(builder.Configuration);

var app = builder.Build();

//ACTIVANDO CORS
app.UseCors("NewPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
