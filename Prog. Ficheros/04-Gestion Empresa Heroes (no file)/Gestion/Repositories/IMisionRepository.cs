using Gestion.Models;
using Gestion.Repositories.Common;

namespace Gestion.Repositories;
/// <summary>
/// Contrato especializado para el Repositorio de Misiones.
/// </summary>
public interface IMisionRepository: ICrudRepository<int, Mision> { }