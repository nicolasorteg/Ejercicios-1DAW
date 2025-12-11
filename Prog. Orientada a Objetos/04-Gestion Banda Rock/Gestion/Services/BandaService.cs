using Gestion.Models;
using Gestion.Repositories;
using Gestion.Validators;

namespace Gestion.Services;

public class BandaService(BandaRepository repository, BandaValidator bandaValidator) {
    public Musico[] GetAllMusicos() {
        var musicos = repository.GetAll();
        return musicos;
    }
}