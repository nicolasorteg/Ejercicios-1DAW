using Funko.Config;
using Funko.Factories;
using Funko.Models;
using Serilog;

namespace Funko.Repositories;

public class FunkoRepository {
    
    // singleton para instancia unica
    private static FunkoRepository? _instance;
    public static FunkoRepository GetInstance() {
        return _instance ??= new FunkoRepository();
    }

    private FunkoPop?[] _catalogo = new FunkoPop?[Configuracion.FunkosIniciales];
    private static int _idCounter;
    
    
    private FunkoRepository() {
        InitFunkos();
    }

    private void InitFunkos() {
        Log.Debug("Inicializando Funkos...");
        var funkosIniciales = FunkosFactory.DemoFunkos();
        foreach (var funko in funkosIniciales)
            Save(funko);
    }
    
    private static int GetNextId() {
        return ++_idCounter;
    }

    /// <summary>
    /// Guarda el funko pasado en el catalogo
    /// </summary>
    /// <param name="funko">Funko a guardar</param>
    public void Save(FunkoPop funko) {
        var nuevoConId = funko with { Id = GetNextId() };
        _catalogo = AñadirUnEspacio();
        for (var i = 0; i < _catalogo.Length; i++) {
            if (_catalogo[i] == null) {
                _catalogo[i] = nuevoConId;
                break;
            } 
        }
    }
    /// <summary>
    /// Obtiene el listado de todos los Funkos
    /// </summary>
    /// <returns>Lista de Funkos</returns>
    public FunkoPop[] GetAll() => ObtenerCatalogoCompacto();

    /// <summary>
    /// Obtiene el listado de funkos extrayendolos del catálogo
    /// </summary>
    /// <returns>Lista de Funkos</returns>
    private FunkoPop[] ObtenerCatalogoCompacto() {
        var numFunkos = 0;
        foreach (var f in _catalogo) {
            if (f != null) numFunkos++;
        }

        var catalogoCompacto = new FunkoPop[numFunkos];
        var index = 0;
        foreach (var f in _catalogo) {
            if (f is {} funkoValido)
                catalogoCompacto[index++] = funkoValido;
        }
        return catalogoCompacto;
    }

    /// <summary>
    /// Añade un espacio nulo al catálogo
    /// para poder introducir otro Funko
    /// </summary>
    /// <returns>Catálogo con un espacio extra</returns>
    private FunkoPop[] AñadirUnEspacio() {
        var nuevoCatalogo = new FunkoPop[ObtenerCatalogoCompacto().Length + 1];
        var index = 0;
        foreach (var f in _catalogo) {
            if (f is { } funkoValido)
                nuevoCatalogo[index++] = funkoValido;
        }
        return nuevoCatalogo;
    }

    /// <summary>
    /// Busca un ID en el catálogo
    /// </summary>
    /// <param name="id">Identificar a Buscar</param>
    /// <returns>El funko encontrado o Nulo si no está</returns>
    public FunkoPop? GetById(int id) {
        foreach (var funko in _catalogo)
            if (funko?.Id == id)
                return funko;
        return null;
    }
    
    /// <summary>
    /// Elimina un Funko de ID pasado
    /// </summary>
    /// <param name="id">Identificar a Eliminar</param>
    /// <returns>El funko eliminado o nulo si falla el borrado</returns>
    public FunkoPop? Delete(int id) {
        for (var i = 0; i < _catalogo.Length; i++)
            if (_catalogo[i]?.Id == id) {
                var funko = _catalogo[i];
                _catalogo[i] = null;
                ObtenerCatalogoCompacto();
                return funko;
            }
        return null;
    }

    /// <summary>
    /// Actualiza un Funko con los datos
    /// de otro Funko pasado
    /// </summary>
    /// <param name="funko">Funko actualizado</param>
    public void Update(FunkoPop funko) {
        for (var i = 0; i < _catalogo.Length; i++) {
            if (_catalogo[i]?.Id != funko.Id) continue;
            _catalogo[i] = funko;
            return;
        }
    }
}