using Gestion.Models;
using Gestion.Repositories;
using Gestion.Validators;

namespace Gestion.Services;

public class BandaService(BandaRepository repository, BandaValidator bandaValidator) {
    public Musico[] GetAllMusicos() {
        var musicos = repository.GetAll();
        return musicos;
    }

    public Musico? GetMusicoById(int id) => repository.GetById(id);

    public ICantanteGuitarrista[] GetAllGuitarristas() {
        var guitarristas = repository.GetGuitarristas();
        return guitarristas;
    }

    public Bajista[] GetAllBajistas() {
        var bajistas = repository.GetBajistas();
        return bajistas;
    }
}