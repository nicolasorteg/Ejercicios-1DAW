namespace Gestion.Models;

public sealed record Bajista: Musico {
    public void TocarBajo() { // no implementado en ningun sitio, ver ultima linea del program.cs
        Console.WriteLine("Toca el bajo con estilo 🎼");
    }
    public void HacerSlapBase() { // no implementado en ningun sitio, ver ultima linea del program.cs
        Console.WriteLine("El bajista hace Slap Base 🔊");
    }

    public override void Ensayar() {
        Console.WriteLine("Ensayando el Bajo...");
    }

    public override void Afinar() {
        Console.WriteLine("Afinando el Bajo...");
    }
    public override string ToString() => base.ToString();
}