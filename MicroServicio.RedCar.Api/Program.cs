using MicroServicio.RedCar.Api.Extensions;
using MicroServicio.RedCar.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// =========================
// SERVICIOS BASE
// =========================
builder.Services.AddControllers();

// =========================
// CONFIGURACIONES PERSONALIZADAS
// =========================
builder.Services.AddCustomApiVersioning();
builder.Services.AddCustomCors(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomSwagger();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAuthorization();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// =========================
// MIDDLEWARE GLOBAL DE ERRORES
// =========================
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();