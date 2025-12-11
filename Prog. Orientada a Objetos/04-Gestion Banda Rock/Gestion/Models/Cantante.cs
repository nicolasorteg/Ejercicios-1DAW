namespace Gestion.Models;

public sealed record Cantante: Musico, ICantanteGuitarrista {
    public void TocarGuitarra() {
        Console.WriteLine("El cantante toca la guitarra 🎸");
    }
    public void Cantar() {
        Console.WriteLine("El cantante comienza a cantar 🎤");
    }
    public override string ToString() => base.ToString();
}