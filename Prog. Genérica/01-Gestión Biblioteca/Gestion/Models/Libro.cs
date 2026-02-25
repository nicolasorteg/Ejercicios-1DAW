namespace Gestion.Models;

public sealed record Libro : Ficha {
    public required string Autor { get; init; }
    public required int Isbn { get; init; }
    public required int NumeroPaginas { get; init; }

    public override string ToString() {
        return $"{base.ToString()} | Autor: {Autor} | ISBN: {Isbn} |  Número de Páginas: {NumeroPaginas}";
    }
}