namespace Gestion.Models;

public sealed record Baterista: Musico {
    public void AporrearBateria() {
        Console.WriteLine("Aporra la batería con estilo 🥁");
    }
}