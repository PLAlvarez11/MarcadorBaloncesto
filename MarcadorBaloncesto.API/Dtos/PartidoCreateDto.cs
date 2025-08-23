namespace MarcadorBaloncesto.API.Dtos;

public record PartidoCreateDto(
    string EquipoLocal,
    string EquipoVisitante,
    int PuntosLocal,
    int PuntosVisitante,
    int FaltasLocal,
    int FaltasVisitante,
    DateTime? FechaPartido
);
