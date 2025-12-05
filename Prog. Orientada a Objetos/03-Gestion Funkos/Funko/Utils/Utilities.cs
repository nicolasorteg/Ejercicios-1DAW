using System.Text.RegularExpressions;
using Serilog;

namespace Funko.Utils;

public static class Utilities {
    
    public static string ValidarDato(string msg, string rgx) {
        string input;
        var isDatoOk = false;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
            if (regex.IsMatch(input)) {
                Log.Information("✅ Dato leído correctamente.");
                isDatoOk = true;
            } else {
                Log.Warning("⚠️ No es un dato válido para este campo.");
                Console.WriteLine("🔴  Dato introducido inválido.");
            }
        } while (!isDatoOk);
        Console.WriteLine();
        return input;
    }

    public static void ImprimirMenuPrincipal() {
        Console.WriteLine("imprime");
    }
}