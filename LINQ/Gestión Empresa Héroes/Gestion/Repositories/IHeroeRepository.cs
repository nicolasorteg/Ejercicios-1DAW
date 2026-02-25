using Gestion.Models;
using Gestion.Repositories.Common;

namespace Gestion.Repositories;

/// <summary>
/// Contrato especializado para el Repositorio de Héroes.
/// </summary>
public interface IHeroeRepository : ICrudRepository<int, Heroe> {
    
    /// <summary>
    /// Manda a un Héroe a descansar y restaura X energia.
    /// </summary>
    /// <param name="h">Heroe a descansar</param>
    void DescansarHeroe(Heroe h);

    /// <summary>
    /// Calcula el poder de un héroe
    /// </summary>
    /// <param name="h">Héroe a calcular</param>
    /// <returns>El poder</returns>
    int CalcularPoder(Heroe h);
}