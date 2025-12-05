using System.Text.RegularExpressions;
using Funko.Enums;
using Funko.Models;
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
        Console.WriteLine("---------------------");
    }   
    public static void ImprimirMenuOrdenacion() {
        Console.WriteLine("---------------------");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.NombreAsc}.- Nombre ascendente  ⬆️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.NombreDesc}.- Nombre descendente ⬇️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.PrecioAsc}.- Precio ascendente  ⬆️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.PrecioDesc}.- Precio descendente ⬇️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.Salir}.- Salir");
        Console.WriteLine("---------------------");
    }

    public static void ImprimirCatalogo(FunkoPop[] catalogo) {
        foreach(var f in catalogo)
            Console.WriteLine(f);
    }
}