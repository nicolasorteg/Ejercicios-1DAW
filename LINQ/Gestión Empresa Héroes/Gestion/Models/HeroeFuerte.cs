using Gestion.Config;

namespace Gestion.Models;

/// <summary>
/// Representa al Héroe con el campo de Fuerza.
/// </summary>
public sealed record HeroeFuerte: Heroe {
    public required int Fuerza { get; init; }
    
    public override int CalcularPoder() => Energia + Nivel * HeroesConfig.MultiplicadorNivelCalcPoder + Fuerza + (int)Rareza;
    
    public override string ToString() => base.ToString() + $"  |  Fuerza: {Fuerza}";
}