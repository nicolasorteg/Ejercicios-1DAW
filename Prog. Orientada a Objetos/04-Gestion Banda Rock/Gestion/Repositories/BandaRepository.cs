using Gestion.Config;
using Gestion.Models;
using Serilog;

namespace Gestion.Repositories;

public class BandaRepository {
    
    // singleton para  unica
    private static BandaRepository? _instance;
    public static BandaRepository GetInstance() {
        return _instance ??= new BandaRepository();
    }
    
    private Musico?[] _miembros = new Musico?[Configuration.MiembrosIniciales];
    private static int _idCounter;
    
    
    private BandaRepository() {
        InitMiembros();
    }

    private void InitMiembros() {
        Log.Debug("Inicializando Funkos...");
        var miembrosIniciales = MusicoFactory.DemoMiembros();
        foreach (var m in miembrosIniciales) Save(m);
    }
    
    private static int GetNextId() {
        return ++_idCounter;
    }

    public void Save(Musico musico) {
        var nuevoConId = musico with { Id = GetNextId() };
        _miembros = AñadirUnEspacio();
        for (var i = 0; i < _miembros.Length; i++) {
            if (_miembros[i] == null) {
                _miembros[i] = nuevoConId;
                break;
            } 
        }
    }
    private Musico[] AñadirUnEspacio() {
        var nuevosMiembros = new Musico[ObtenerCatalogoCompacto().Length + 1];
        var index = 0;
        foreach (var m in _miembros) {
            if (m is { } musicoValido)
                nuevosMiembros[index++] = musicoValido;
        }
        return nuevosMiembros;
    }
    private Musico[] ObtenerCatalogoCompacto() {
        var numMusicos = 0;
        foreach (var f in _miembros) {
            if (f != null) numMusicos++;
        }

        var musicosCompacto = new Musico[numMusicos];
        var index = 0;
        foreach (var f in _miembros) {
            if (f is {} musicoValido)
                musicosCompacto[index++] = musicoValido;
        }
        return musicosCompacto;
    }

    public Musico[] GetAll() => ObtenerCatalogoCompacto();
}