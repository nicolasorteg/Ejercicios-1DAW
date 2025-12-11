namespace Gestion.Models;

public class Guitarrista: Musico, ICantanteGuitarrista {
    public void TocarGuitarra() {
        Console.WriteLine("El guitarrista toca la guitarra 🎸");
    }
    public void RealizarSolo() {
        Console.WriteLine("El guitarrista toca un solo espectacular 🎸");
    }
    public void Cantar() {
        Console.WriteLine("El guitarrista comienza a cantar 🎤");
    }
}