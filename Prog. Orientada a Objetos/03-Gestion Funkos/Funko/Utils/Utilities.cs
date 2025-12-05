using System.Text.RegularExpressions;
using Funko.Enums;
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
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerFunkos}.- Ver Catálogo");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.ObtenerFunkoPorId}.- Buscar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.OrdenarFunkos}.- Ordenar Funkos");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.CrearFunko}.- Crear Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.ActualizarFunko}.- Actualizar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.EliminarFunko}.- Eliminar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Salir}.- Salir");
    }
}