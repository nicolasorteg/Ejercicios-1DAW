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

    public FunkoPop Save(FunkoPop funko) {
        var nuevoConId = funko with { Id = GetNextId() };
        _catalogo = AñadirUnEspacio();
        for (var i = 0; i < _catalogo.Length; i++) {
            if (_catalogo[i] == null) {
                _catalogo[i] = nuevoConId;
                break;
            } 
        }
        return nuevoConId;
    }

    public FunkoPop[] GetAll() => ObtenerCatalogoCompacto();

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

    private FunkoPop[] AñadirUnEspacio() {
        var nuevoCatalogo = new FunkoPop[ObtenerCatalogoCompacto().Length + 1];
        var index = 0;
        foreach (var f in _catalogo) {
            if (f is { } funkoValido)
                nuevoCatalogo[index++] = funkoValido;
        }
        return nuevoCatalogo;
    }

    public FunkoPop? GetById(int id) {
        foreach (var funko in _catalogo)
            if (funko?.Id == id)
                return funko;
        return null;
    }

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
}