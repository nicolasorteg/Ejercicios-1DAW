using Funko.Enums;
using Funko.Models;
namespace Funko.Validators;

public class FunkoValidator {
    public static readonly string RegexConfirmacion = @"^[sSnN]$";
    public static readonly string RegexOpcionMenuPrincipal = @$"^[{(int)OpcionMenuPrincipal.Salir}-{(int)OpcionMenuPrincipal.EliminarFunko}]$";
    public static readonly string RegexDni = @"^[0-9]{8}[A-Z]$";
    public static readonly string RegexNombre = @"^[A-Za-z]{2,}$";
    public static readonly string RegexRol = @"^(superheroe|anime|disney)$";
    public static readonly string RegexPrecio = @"^\d{1,}";

    public FunkoPop Validate(FunkoPop funko) {
        Console.WriteLine("Valida epicamente.");
        return funko;
    }
}