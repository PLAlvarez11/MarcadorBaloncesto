using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarcadorBaloncesto.API.Models;

public class Partido
{
    public int Id { get; set; }
    
    [Required] 
    [MaxLength(100)]
    public string EquipoLocal { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string EquipoVisitante { get; set; } = string.Empty;
    
    public int PuntosLocal { get; set; }
    public int PuntosVisitante { get; set; }
    public int FaltasLocal { get; set; }
    public int FaltasVisitante { get; set; }
    
    public DateTime FechaPartido { get; set; } = DateTime.Now;
}