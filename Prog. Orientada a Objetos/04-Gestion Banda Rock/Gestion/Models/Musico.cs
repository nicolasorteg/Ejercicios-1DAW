namespace Gestion.Models;

public abstract record Musico {
    public int Id { get; init; }
    public required string Nombre { get; init; }
    public required int TiempoEnBanda { get; init; }
    public override string ToString() {
        return Id < 10
            ? $"ID: {Id}   |  Nombre: {Nombre}  |  Tiempo en la Banda: {TiempoEnBanda}"
            : $"ID: {Id}  |  Nombre: {Nombre}  |  Tiempo en la Banda: {TiempoEnBanda}";
    }
}