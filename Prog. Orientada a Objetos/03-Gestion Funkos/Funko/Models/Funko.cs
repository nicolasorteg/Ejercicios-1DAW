namespace Funko.Models;

public record Funko {
    public enum Tipo { Superherores, Anime, Disney }
    
    public int Id { get; init; }
    public required int Nombre { get; init; }
    public required Tipo Categoria { get; init; }
    public required decimal Precio { get; init; }
}