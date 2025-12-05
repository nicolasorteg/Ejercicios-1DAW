using Funko.Enums;
using Funko.Models;
using Funko.Repositories;
using Funko.Validators;

namespace Funko.Services;

public class FunkoService(FunkoRepository repository, FunkoValidator validator) {
    public FunkoValidator Validator { get; } = validator;

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
                if (ordenamiento == TipoOrdenamiento.Id) {
                    if (funkos[j].Id > funkos[j+1].Id) debeIntercambiar = true;
                    
                } else if (ordenamiento == TipoOrdenamiento.NombreAsc) {
                    if (string.Compare(funkos[j].Nombre, funkos[j+1].Nombre, StringComparison.Ordinal) > 0) debeIntercambiar = true;
                    
                } else if (ordenamiento == TipoOrdenamiento.NombreDesc) {
                    if (string.Compare(funkos[j].Nombre, funkos[j+1].Nombre, StringComparison.Ordinal) < 0) debeIntercambiar = true;
                    
                }else if (ordenamiento == TipoOrdenamiento.PrecioAsc) {
                    if (funkos[j].Precio > funkos[j+1].Precio) debeIntercambiar = true;
                    
                } else if (ordenamiento == TipoOrdenamiento.PrecioDesc) {
                    if (funkos[j].Precio < funkos[j+1].Precio) debeIntercambiar = true;
                }
                if (debeIntercambiar) // si se cumple, swap
                    (funkos[j], funkos[j+1]) = (funkos[j+1], funkos[j]);
            }
        }
    }
}