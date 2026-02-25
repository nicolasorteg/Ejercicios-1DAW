namespace Gestion.Models;

public sealed record Dvd : Ficha {
    public required string Director { get; init; }
    public required int Duracion { get; init; }
    public override string ToString() {
        return $"{base.ToString()} | Director: {Director} | Duración: {Duracion} min";
    }
}