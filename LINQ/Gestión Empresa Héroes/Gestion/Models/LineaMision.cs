namespace Gestion.Models;

/// <summary>
/// Clase intermedia para romper la relación N:M de Misión - Héroe.
/// Almacena el Héroe.
/// </summary>
public record LineaMision {
    public required Heroe Heroe { get; init; }
}