using System.Text.RegularExpressions;
using Gestion.Enum;

namespace Gestion.Validators;

public class BandaValidator {
    public static readonly string RegexConfirmacion = @"^[sSnN]$";
    public static readonly string RegexOpcionMenuPrincipal = @$"^[{(int)OpcionMenuPrincipal.Salir}-{(int)OpcionMenuPrincipal.Borrar}]$";
    public static readonly string RegexOpcionMenuActualizacion = @$"^[{(int)OpcionMenuActualizar.Salir}-{(int)OpcionMenuActualizar.TiempoBanda}]$";
    public static readonly string RegexId = @"^\d{1,}$";
    public static readonly string RegexNombre = @"^[A-Za-z]{3,}$";
    public static readonly string RegexTiempoEnBanda = @"^\d{1,}$"; 
    public static readonly string RegexRol = @"^(Bajistas|Baterista|Guitarrista|Cantante)$";
    
    public static string ValidarDato(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return input;
    }
}