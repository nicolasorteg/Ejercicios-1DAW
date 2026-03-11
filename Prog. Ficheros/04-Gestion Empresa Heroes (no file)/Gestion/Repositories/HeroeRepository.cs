using Gestion.Factories;
using Gestion.Models;

namespace Gestion.Repositories;

/// <summary>
/// Repositorio para la gestión de Héroes.
/// Métodos de ordenacion delegados al servicio.
/// Patrón Singleton para instancia única.
/// </summary>
public class HeroeRepository : IHeroeRepository {

    // singleton para instancia unica
    private static HeroeRepository? _instance;
    public static HeroeRepository GetInstance() {
        return _instance ??= new HeroeRepository();
    }

    // listado de heroes
    private readonly List<Heroe> _heroes = [];

    // contador id
    private static int _idCounter;
    private static int GetNextId() => ++_idCounter;

    // constructor
    private HeroeRepository() {
        InitMiembros();
    }

    // llamada a factory para datos iniciales
    private void InitMiembros() {
        var miembrosIniciales = HeroeFactory.DemoHeroes();
        foreach (var h in miembrosIniciales) {
            _heroes.Add(h);
            // si se encuentra un ID más grande que el contador, este se sincroniza con el mayor
            if (h.Id > _idCounter) _idCounter = h.Id;
        }
    }

    /// <inheritdoc cref="IHeroeRepository.GetAll" />
    public IEnumerable<Heroe> GetAll() => _heroes;

    /// <inheritdoc cref="IHeroeRepository.GetById" />
    public Heroe? GetById(int id) => _heroes.FirstOrDefault(h => h.Id == id);

    /// <inheritdoc cref="IHeroeRepository.Create" />
    public Heroe? Create(Heroe entity) {
        if (GetById(entity.Id) != null) return null;

        var nuevoConId = entity with { Id = GetNextId() };
        _heroes.Add(nuevoConId);
        return nuevoConId;
    }

    /// <inheritdoc cref="IHeroeRepository.Update" />
    public Heroe? Update(int id, Heroe entity) {
        var existente = GetById(id);
        if (existente == null) return null;

        existente.Nombre = entity.Nombre;
        existente.Nivel = entity.Nivel;
        existente.Energia = entity.Energia;
        existente.Experiencia = entity.Experiencia;
        existente.Rareza = entity.Rareza;

        return existente;
    }

    /// <inheritdoc cref="IHeroeRepository.Delete" />
    public Heroe? Delete(int id) {
        var heroe = GetById(id);
        if (heroe != null) {
            _heroes.Remove(heroe);
        }

        return heroe;
    }

    /// <inheritdoc cref="IHeroeRepository.DescansarHeroe" />
    public void DescansarHeroe(Heroe h) {
        h.Descansar();
    }
    
    /// <inheritdoc cref="IHeroeRepository.CalcularPoder" />
    public int CalcularPoder(Heroe h) => h.CalcularPoder();
}