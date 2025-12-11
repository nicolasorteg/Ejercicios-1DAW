namespace Gestion.Models;

public sealed record Baterista: Musico {
    public void AporrearBateria() { // no implementado en ningun sitio, ver ultima linea del program.cs
        Console.WriteLine("Aporra la batería con estilo 🥁");
    }
    
    public override void Ensayar() {
        Console.WriteLine("Ensayando la Batería...");
    }
    public override void Afinar() {
        Console.WriteLine("Afinando la Batería...");
    }
    
    public override string ToString() => base.ToString();
}