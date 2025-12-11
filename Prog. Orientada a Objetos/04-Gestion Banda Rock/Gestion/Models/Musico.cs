namespace Gestion.Models;

public abstract record Musico {
    public int Id { get; init; }
    public required string Nombre { get; init; }
    public required int TiempoEnBanda { get; init; }

    public abstract void Ensayar();
    public abstract void Afinar();
    public override string ToString() {
        return Id < 10
            ? $"ID: {Id}  |  Tipo: {GetType().Name}  |  Nombre: {Nombre}  |  Tiempo en la Banda: {TiempoEnBanda} años"
            : $"ID: {Id} |  Nombre: {Nombre}  |  Tiempo en la Banda: {TiempoEnBanda} años";
    }
}