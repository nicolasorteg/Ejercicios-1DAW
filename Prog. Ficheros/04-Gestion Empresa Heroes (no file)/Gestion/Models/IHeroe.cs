namespace Gestion.Models;

/// <summary>
/// Contrato que obliga a implementar los métodos de descansar, calcular poder y sumar xp
/// </summary>
public interface IHeroe {
    void Descansar();
    int CalcularPoder();
    void SumarExperiencia(int xp);
}