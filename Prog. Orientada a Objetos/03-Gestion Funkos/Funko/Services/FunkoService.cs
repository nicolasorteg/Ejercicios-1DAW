using Funko.Enums;
using Funko.Models;
using Funko.Repositories;
using Funko.Validators;

namespace Funko.Services;

public class FunkoService(FunkoRepository repository, FunkoValidator validator) {

    public FunkoPop[] GetAllFunkos(TipoOrdenamiento ordenamiento) {
        var funkos = repository.GetAll();
        OrdenarCatalogo(funkos, ordenamiento);
        return funkos;
    }

    
    private static void OrdenarCatalogo(FunkoPop[] funkos, TipoOrdenamiento ordenamiento) {
        if (funkos.Length <= 1) return;
        for (var i = 0; i < funkos.Length - 1; i++) {
            for (var j = 0; j < funkos.Length - i - 1; j++) {
                var debeIntercambiar = false;
                switch (ordenamiento) {
                    case TipoOrdenamiento.NombreAsc: if (string.Compare(funkos[j].Nombre, funkos[j+1].Nombre, StringComparison.Ordinal) > 0) debeIntercambiar = true; 
                        break;
                    case TipoOrdenamiento.NombreDesc: if (string.Compare(funkos[j].Nombre, funkos[j+1].Nombre, StringComparison.Ordinal) < 0) debeIntercambiar = true; 
                        break;
                    case TipoOrdenamiento.PrecioAsc: if (funkos[j].Precio > funkos[j + 1].Precio) debeIntercambiar = true;
                        break;
                    case TipoOrdenamiento.PrecioDesc: if (funkos[j].Precio < funkos[j+1].Precio) debeIntercambiar = true;
                        break;
                    default: if (funkos[j].Id > funkos[j+1].Id) debeIntercambiar = true;
                        break;
                }
                if (debeIntercambiar) // si se cumple, swap
                    (funkos[j], funkos[j+1]) = (funkos[j+1], funkos[j]);
            }
        }
    }
    /// <summary>
    /// Se encarga de llamar al repository para que busque en el catálogo
    /// el Funko con el ID pasado
    /// </summary>
    /// <param name="id">Identificador del Funko</param>
    /// <returns>O el Funko encontrado o nulo en caso de que no esté</returns>
    public FunkoPop? GetFunkoById(int id) => repository.GetById(id);
    public FunkoPop? DeleteFunko(int id) => repository.Delete(id);

    public void SaveFunko(FunkoPop funko) {
        var funkoValidado = validator.Validate(funko);
        repository.Save(funkoValidado);
    }

    public void UpdateFunko(FunkoPop funko) {
        var funkoValidado = validator.Validate(funko);
        repository.Update(funkoValidado);
    }
}