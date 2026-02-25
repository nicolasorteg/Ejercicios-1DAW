namespace Gestion.Repositories.Common;

public interface ICrudRepository<in TKey, TEntity> where TEntity : class {
    
    /// <summary>
    /// Obtener todos los datos.
    /// </summary>
    /// <returns>Lista de datos</returns>
    IEnumerable<TEntity> GetAll();
    
    /// <summary>
    /// Buscar por ID
    /// </summary>
    /// <param name="id">Identificar único</param>
    /// <returns>Nulo si no encuentra o la entidad encontrada</returns>
    TEntity? GetById(TKey id);
    
    /// <summary>
    /// Crea una entidad
    /// </summary>
    /// <param name="entity">La entidad a crear</param>
    /// <returns>Nulo si no se puede crear o la entidad ya creada</returns>
    TEntity? Create(TEntity entity);
    
    /// <summary>
    /// Actualiza una entidad
    /// </summary>
    /// <param name="id">Identificador de la entidad</param>
    /// <param name="entity">Nuevos datos de la entidad</param>
    /// <returns>Nulo si no se encontró a la entidad o la entidad actualizada</returns>
    TEntity? Update(TKey id, TEntity entity);
    
    /// <summary>
    /// Elimina una entidad
    /// </summary>
    /// <param name="id">Identificador de la entidad</param>
    /// <returns>Nulo si no la encuentra o la entidad eliminada</returns>
    TEntity? Delete(TKey id);
}