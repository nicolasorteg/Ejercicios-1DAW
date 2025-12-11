namespace Gestion.Models;

public sealed record Cantante: Musico, ICantanteGuitarrista {
    public void TocarGuitarra() {
        Console.WriteLine("El cantante agarra una guitarra y comienza a tocarla 🎸");
    }
    public void Cantar() {
        Console.WriteLine("El cantante comienza a cantar 🎤");
    }
    public override string ToString() => base.ToString();
}