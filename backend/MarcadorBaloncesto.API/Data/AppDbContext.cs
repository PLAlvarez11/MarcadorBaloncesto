using Microsoft.EntityFrameworkCore;
using MarcadorBaloncesto.API.Models;

namespace MarcadorBaloncesto.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Partido> Partidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Partido>(entity =>
        {
            entity.HasKey(p => p.Id); 
            
            entity.Property(p => p.EquipoLocal)
                  .IsRequired() 
                  .HasMaxLength(100);
                  
            entity.Property(p => p.EquipoVisitante)
                  .IsRequired()
                  .HasMaxLength(100);
                  
            entity.Property(p => p.PuntosLocal)
                  .HasDefaultValue(0); 
                  
            entity.Property(p => p.PuntosVisitante)
                  .HasDefaultValue(0);
                  
            entity.Property(p => p.FaltasLocal)
                  .HasDefaultValue(0);
                  
            entity.Property(p => p.FaltasVisitante)
                  .HasDefaultValue(0);

            entity.HasIndex(p => p.EquipoLocal);
            entity.HasIndex(p => p.EquipoVisitante);
        });

        modelBuilder.Entity<Partido>().HasData(
            new Partido { 
                Id = 1, 
                EquipoLocal = "Lakers", 
                EquipoVisitante = "Warriors", 
                PuntosLocal = 110, 
                PuntosVisitante = 105,
                FaltasLocal = 15,
                FaltasVisitante = 12
            },
            new Partido { 
                Id = 2, 
                EquipoLocal = "Bulls", 
                EquipoVisitante = "Celtics", 
                PuntosLocal = 95, 
                PuntosVisitante = 98,
                FaltasLocal = 10,
                FaltasVisitante = 8
            }
        );
    }
}