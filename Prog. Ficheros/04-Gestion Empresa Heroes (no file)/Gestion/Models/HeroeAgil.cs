using Gestion.Config;

namespace Gestion.Models;

/// <summary>
/// Representa al Héroe con el campo de Agilidad.
/// </summary>
public sealed record HeroeAgil: Heroe {
    public required int Agilidad { get; init; }
    
    public override int CalcularPoder() => Energia + Nivel * HeroesConfig.MultiplicadorNivelCalcPoder + Agilidad + (int)Rareza;

    public override string ToString() => base.ToString() + $"  |  Agilidad: {Agilidad}";
}