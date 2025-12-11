namespace Gestion.Models;

public sealed record Guitarrista: Musico, ICantanteGuitarrista {
    public void TocarGuitarra() {
        Console.WriteLine("El guitarrista toca la guitarra 🎸");
    }
    public void RealizarSolo() {
        Console.WriteLine("El guitarrista toca un solo espectacular 🎸");
    }
    public void Cantar() {
        Console.WriteLine("El guitarrista comienza a cantar los coros 🎤");
    }
    
    public override void Ensayar() {
        Console.WriteLine("Ensayando la Guitarra...");
    }
    public override void Afinar() {
        Console.WriteLine("Afinando la Guitarra...");
    }
    
    public override string ToString() => base.ToString();
}