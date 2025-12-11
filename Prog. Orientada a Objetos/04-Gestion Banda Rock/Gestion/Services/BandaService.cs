using Gestion.Models;
using Gestion.Repositories;
using Gestion.Validators;

namespace Gestion.Services;

public class BandaService(BandaRepository repository, BandaValidator validator) {
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

    public void SaveMusico(Musico nuevoMusico) {
        var musicoValidado = validator.Validate(nuevoMusico);
        repository.Save(musicoValidado);
    }

    public Musico? DeleteMusico(int id) => repository.Delete(id);

    public void UpdateFunko(Musico musico) {
        var musicoValidado = validator.Validate(musico);
        repository.Update(musicoValidado);
    }
}