namespace Funko.Models;

public record FunkoPop {
    public enum Tipo { Superheroe, Anime, Disney }
    
    public int Id { get; init; }
    public required string Nombre { get; init; }
    public required Tipo Categoria { get; init; }
    public required decimal Precio { get; init; }

    public override string ToString() {
        return Id < 10
            ? $"{Id}   |  Nombre: {Nombre}  |  Categoria: {Categoria}  |  Precio: {Precio:F2}"
            : $"{Id}  |  Nombre: {Nombre}  |  Categoria: {Categoria}  |  Precio: {Precio:F2}";
    }
}