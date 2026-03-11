using Gestion.Config;

namespace Gestion.Models;

/// <summary>
/// Representa al Héroe con el campo de Inteligencia (enum).
/// </summary>
public sealed record HeroeInteligente: Heroe {
    public required Inteligencias Inteligencia { get; init; }
    public enum Inteligencias { Baja = 10, Media = 30, Alta = 50 }
    
    public override int CalcularPoder() => Energia + Nivel * HeroesConfig.MultiplicadorNivelCalcPoder + (int)Inteligencia + (int)Rareza;
    
    public override string ToString() => base.ToString() + $"  |  Inteligencia: {Inteligencia}";
}