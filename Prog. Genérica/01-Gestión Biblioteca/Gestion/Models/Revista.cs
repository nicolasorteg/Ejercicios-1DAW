namespace Gestion.Models;

public sealed record Revista : Ficha {
    public required string Tematica { get; init; }
    public required int Edicion { get; init; }
    public override string ToString() {
        return $"{base.ToString()} | Temática: {Tematica} | Edición: {Edicion}";
    }
}