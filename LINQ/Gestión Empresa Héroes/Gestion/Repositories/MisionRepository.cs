using Gestion.Factories;
using Gestion.Models;

namespace Gestion.Repositories;

/// <summary>
/// Repositorio para la gestión de Misiones.
/// Implementa patrón Singleton y hereda de ICrudRepository.
/// </summary>
public class MisionRepository: IMisionRepository {
    
    // singleton para instancia única
    private static MisionRepository? _instance;
    public static MisionRepository GetInstance() {
        return _instance ??= new MisionRepository();
    }
    
    // listado de misiones
    private readonly List<Mision> _misiones = [];
    
    // contador id
    private static int _idCounter;
    private static int GetNextId() => ++_idCounter;

    // constructor privado
    private MisionRepository() {
        InitMiembros();
    }

    // llamada a factory para datos iniciales
    private void InitMiembros() {
        var misionesIniciales = MisionFactory.DemoMisiones();
        foreach (var m in misionesIniciales) {
            _misiones.Add(m);
            // si se encuentra un ID más grande que el contador, este se sincroniza con el mayor
            if (m.Id > _idCounter) _idCounter = m.Id;
        }
    }

    /// <inheritdoc cref="IMisionRepository.GetAll" />
    public IEnumerable<Mision> GetAll() => _misiones;
    
    /// <inheritdoc cref="IMisionRepository.GetById" />
    public Mision? GetById(int id) => _misiones.FirstOrDefault(m => m.Id == id);
    
    /// <inheritdoc cref="IMisionRepository.Create" />
    public Mision? Create(Mision entity) {
        if (GetById(entity.Id) != null) return null;

        var nueva = new Mision {
            Id = GetNextId(),
            Nombre = entity.Nombre,
            Dificultad = entity.Dificultad,
            Estado = entity.Estado,
            Lineas = new List<LineaMision>(entity.Lineas) 
        };
        
        _misiones.Add(nueva);
        return nueva;
    }
    
    /// <inheritdoc cref="IMisionRepository.Update" />
    public Mision? Update(int id, Mision entity) {
        var existente = GetById(id);
        if (existente == null) return null;
        
        existente.Nombre = entity.Nombre;
        existente.Dificultad = entity.Dificultad;
        existente.Estado = entity.Estado;
        existente.Lineas = entity.Lineas;

        return existente;
    }
    public Mision? Delete(int id) {
        var mision = GetById(id);
        if (mision != null) {
            _misiones.Remove(mision);
        }
        return mision;
    }
}