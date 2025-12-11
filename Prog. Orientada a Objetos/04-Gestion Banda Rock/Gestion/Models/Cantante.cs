namespace Gestion.Models;

public sealed record Cantante: Musico, ICantanteGuitarrista {
    public void TocarGuitarra() {
        Console.WriteLine("El cantante agarra una guitarra y comienza a tocarla 🎸");
    }
    public void Cantar() {
        Console.WriteLine("El cantante comienza a cantar 🎤");
    }
    
    public override void Ensayar() {
        Console.WriteLine("Ensayando el Canto...");
    }
    public override void Afinar() {
        Console.WriteLine("Afinando el Micrófono...");
    }
    
    public override string ToString() => base.ToString();
}