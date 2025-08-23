namespace MarcadorBaloncesto.API.Models;

public class Partido
{
    public int Id { get; set; }

    public string EquipoLocal { get; set; } = default!;
    public string EquipoVisitante { get; set; } = default!;

    public int PuntosLocal { get; set; } = 0;
    public int PuntosVisitante { get; set; } = 0;

    public int FaltasLocal { get; set; } = 0;
    public int FaltasVisitante { get; set; } = 0;

    public DateTime FechaPartido { get; set; } = DateTime.Now;
}
