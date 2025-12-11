namespace Gestion.Models;

public sealed record Bajista: Musico {
    public void TocarBajo() {
        Console.WriteLine("Toca el bajo con estilo 🎼");
    }
    public void HacerSlapBase() {
        Console.WriteLine("El bajista hace Slap Base 🔊");
    }
    public override string ToString() => base.ToString();
}