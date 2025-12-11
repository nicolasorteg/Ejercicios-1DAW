using System.Text.RegularExpressions;
using Gestion.Config;
using Gestion.Enum;
using Gestion.Models;

namespace Gestion.Validators;

public class BandaValidator {
    public static readonly string RegexConfirmacion = @"^[sSnN]$";
    public static readonly string RegexOpcionMenuPrincipal = @$"^[{(int)OpcionMenuPrincipal.Salir}-{(int)OpcionMenuPrincipal.HacerSolo}]|[{(int)OpcionMenuPrincipal.Cantar}]$";
    public static readonly string RegexOpcionMenuActualizacion = @$"^[{(int)OpcionMenuActualizar.Salir}-{(int)OpcionMenuActualizar.TiempoBanda}]$";
    public static readonly string RegexId = @"^\d{1,}$";
    public static readonly string RegexNombre = @"^[A-Za-z\s]{3,}$";
    public static readonly string RegexTiempoEnBanda = @"^\d{1,}$"; 
    public static readonly string RegexRol = @"^(Bajista|Baterista|Guitarrista|Cantante)$";
    
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

    public Musico Validate(Musico musico) {
        if (string.IsNullOrEmpty(musico.Nombre) || string.IsNullOrWhiteSpace(musico.Nombre) || musico.Nombre.Length < 3)
            throw new ArgumentException("Nombre inválido. Introduzca un nombre de al menos 3 letras.");
        if (musico.TiempoEnBanda < Configuration.AñosMinimos || musico.TiempoEnBanda > Configuration.AñosMaximos)
            throw new ArgumentOutOfRangeException($"Los años deben estar entre {Configuration.AñosMinimos} y {Configuration.AñosMaximos} (ambos inclusive).");
        return musico;
    }
}