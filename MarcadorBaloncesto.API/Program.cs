using MarcadorBaloncesto.API.Data;
using MarcadorBaloncesto.API.Models;
using MarcadorBaloncesto.API.Dtos;   // 👈 usa el DTO ya movido
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
const string CorsPolicy = "AllowAngular";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CorsPolicy, p => p
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CorsPolicy);

// Endpoints
app.MapGet("/api/partidos", async (AppDbContext db) =>
{
    var data = await db.Partidos
        .OrderByDescending(p => p.FechaPartido)
        .Take(50)
        .ToListAsync();
    return Results.Ok(data);
});

app.MapPost("/api/partidos", async (PartidoCreateDto dto, AppDbContext db) =>
{
    var partido = new Partido
    {
        EquipoLocal     = dto.EquipoLocal,
        EquipoVisitante = dto.EquipoVisitante,
        PuntosLocal     = dto.PuntosLocal,
        PuntosVisitante = dto.PuntosVisitante,
        FaltasLocal     = dto.FaltasLocal,
        FaltasVisitante = dto.FaltasVisitante,
        FechaPartido    = dto.FechaPartido ?? DateTime.Now
    };

    db.Partidos.Add(partido);
    await db.SaveChangesAsync();
    return Results.Created($"/api/partidos/{partido.Id}", partido);
});

app.Run();
