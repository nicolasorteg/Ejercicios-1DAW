namespace Funko.Models;

public record FunkoPop {
    public enum Tipo { Superherore, Anime, Disney }
    
    public int Id { get; init; }
    public required string Nombre { get; init; }
    public required Tipo Categoria { get; init; }
    public required decimal Precio { get; init; }
}