using Gestion.Exceptions;
using Gestion.Models;

namespace Gestion.Services;

public interface IEmpresaService {
    
    /// <summary>
    /// Llama a repositorio para guardar un Héroe tras validarlo
    /// </summary>
    /// <param name="h">Heroe a guardar</param>
    /// <returns>Heroe registrado</returns>
    /// <exception cref="HeroeExceptions.Validation">Se lanza si la validación de campos falla.</exception>
    /// <exception cref="HeroeExceptions.NotFound">Se lanza si el identificador no existe.</exception>
    Heroe SaveHeroe(Heroe h);
    
    /// <summary>
    /// Obtiene todos los héroes del sistema
    /// </summary>
    /// <returns>Enumerable con todos los héroes activos.</returns>
    IEnumerable<Heroe> GetAllHeroes();
    
    /// <summary>
    /// Localiza un héroe activo por su ID
    /// </summary>
    /// <param name="id">Identificador único</param>
    /// <returns>El héroe encontrado</returns>
    /// <exception cref="HeroeExceptions.NotFound">Se lanza si el identificador no existe.</exception>
    Heroe GetHeroeById(int id);
    
    /// <summary>
    /// Actualiza al héroe
    /// </summary>
    /// <param name="id">Id del héroe a actualizar</param>
    /// <param name="h">Héroe con los datos actualizados</param>
    /// <returns>El héroe actualizado</returns>
    /// <exception cref="HeroeExceptions.Validation">Se lanza si la validación de campos falla.</exception>
    /// <exception cref="HeroeExceptions.NotFound">Se lanza si el identificador no existe.</exception>
    Heroe UpdateHeroe(int id, Heroe h);
    
    /// <summary>
    /// Elimina a un héroe del sistema
    /// </summary>
    /// <param name="id">Id del héroe a eliminar</param>
    /// <returns>El héroe eliminado</returns>
    /// <exception cref="HeroeExceptions.NotFound">Se lanza si el identificador no existe.</exception>
    Heroe DeleteHeroe(int id);

    /// <summary>
    /// Ordena a los héroes en base a su nivel
    /// </summary>
    /// <returns>Héroes ordenados</returns>
    IEnumerable<Heroe> GetAllOrderByLevel();
    
    /// <summary>
    /// Ordena a los héroes en base a su poder
    /// </summary>
    /// <returns>Héroes ordenados</returns>
    IEnumerable<Heroe> GetAllOrderByPower();

    /// <summary>
    /// Manda a la cama a un heroe para restaurar X su energía
    /// </summary>
    /// <param name="id">ID del heroe a descansar</param>
    void DescansarHeroe(int id);

    /// <summary>
    /// Calcula el poder de un Héroe
    /// </summary>
    /// <param name="id">Id del heroe</param>
    /// <returns>Poder</returns>
    int GetPoderHeroe(int id);
    
    Mision SaveMision(Mision m);
    
    IEnumerable<Mision> GetAllMisiones();
    
    IEnumerable<Mision> GetAllMisionesOrderByDificultad();

    IEnumerable<Mision> GetMisionesPendientes();
    
    Mision GetMisionById(int id);
    
    Mision UpdateMision(int id, Mision m);
    
    Mision DeleteMision(int id);
    
    void AsignarHeroeAMision(int heroeId, int misionId);
    void SimularMision(int misionId);
}