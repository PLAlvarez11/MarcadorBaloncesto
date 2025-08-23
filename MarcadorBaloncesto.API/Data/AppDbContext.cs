using MarcadorBaloncesto.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MarcadorBaloncesto.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Partido> Partidos => Set<Partido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var p = modelBuilder.Entity<Partido>();

        p.ToTable("Partidos");

        p.Property(x => x.EquipoLocal)
            .IsRequired()
            .HasMaxLength(100);
        p.Property(x => x.EquipoVisitante)
            .IsRequired()
            .HasMaxLength(100);

        p.Property(x => x.PuntosLocal).HasDefaultValue(0);
        p.Property(x => x.PuntosVisitante).HasDefaultValue(0);
        p.Property(x => x.FaltasLocal).HasDefaultValue(0);
        p.Property(x => x.FaltasVisitante).HasDefaultValue(0);

        p.HasIndex(x => x.EquipoLocal);
        p.HasIndex(x => x.EquipoVisitante);

       
    }
}
