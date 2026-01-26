using System.Text.RegularExpressions;
using Gestion.Enums;

namespace Gestion.Utils;

public static class Utilities {
    public static void ImrimirMenuPrincipal() {
        Console.WriteLine("\n--- 📚 MENU PRINCIPAL 📚 ---");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Crear}.-  Crear Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Actualizar}.-  Actualizar Músico.")
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Salir}.-  Salir.");
        Console.WriteLine("--------------------------------");
    }
    
    public static string ValidarDatoRegex(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Write($"{msg} ");
            input = ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        WriteLine();
        return input;
    }
}