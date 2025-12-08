using Funko.Config;
using Funko.Enums;
using Funko.Models;
using Serilog;

namespace Funko.Validators;

public class FunkoValidator {
    public static readonly string RegexConfirmacion = @"^[sSnN]$";
    public static readonly string RegexOpcionMenuPrincipal = @$"^[{(int)OpcionMenuPrincipal.Salir}-{(int)OpcionMenuPrincipal.EliminarFunko}]$";
    public static readonly string RegexOpcionMenuOrdenacion = $@"^[{(int)OpcionMenuOrdenacion.Salir}-{(int)OpcionMenuOrdenacion.PrecioDesc}]$";
    public static readonly string RegexId = @"^\d{1,}$";
    public static readonly string RegexNombre = @"^[A-Za-z]{3,}$";
    public static readonly string RegexRol = @"^(Superheroe|Anime|Disney)$";
    public static readonly string RegexPrecio = @"^\d{1,}";

    public FunkoPop Validate(FunkoPop funko) {
        Log.Debug("Validando para introducir el funko...");
        if (string.IsNullOrEmpty(funko.Nombre) || string.IsNullOrWhiteSpace(funko.Nombre) || funko.Nombre.Length < 3)
            throw new ArgumentException("Nombre inválido. Introduzca un nombre de al menos 3 letras.");
        if (funko.Precio < Configuracion.PrecioMinimo || funko.Precio > Configuracion.PrecioMaximo)
            throw new ArgumentOutOfRangeException($"El precio debe estar entre {Configuracion.PrecioMinimo} y {Configuracion.PrecioMaximo} (ambos inclusive).");
        // aquí falta la validacion del enum pero no sé hacerla de momento
        return funko;
    }
}