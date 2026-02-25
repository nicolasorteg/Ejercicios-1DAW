namespace Gestion.Models;

public abstract record Ficha {
    
    public enum EstadoActual { Disponible, Prestado, EnPreparacion }

    public int Id { get; init; } = 0;
    public required string Nombre { get; init; }
    public required EstadoActual Estado { get; init; }
    
    public override string ToString() {
        return $"ID: {Id:D2} |  Tipo: {GetType().Name}  |  Nombre: {Nombre}  |  Estado: {Estado}";
    }
}