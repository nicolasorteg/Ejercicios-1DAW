using Gestion.Enums;

namespace Gestion.Config;

public class Configuration {
    public static readonly string RegexMenuPrincipal = @$"^[{(int)OpcionMenuPrincipal.Salir}-{(int)OpcionMenuPrincipal.Eliminar}]$";
}