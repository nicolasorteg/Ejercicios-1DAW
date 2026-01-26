using System.Text.RegularExpressions;
using Gestion.Enums;

namespace Gestion.Utils;

public static class Utilities {
    public static void ImrimirMenuPrincipal() {
        Console.WriteLine("\n--- 📚 MENU PRINCIPAL 📚 ---");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerTodos}.-  Ver Biblioteca.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.ListarEstado}.-  Listar por Estado.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.BuscarId}.-  Buscar por ID.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.OrdenarPaginas}.-  Ordenar Libros por Páginas.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Crear}.-  Crear Ficha.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Actualizar}.-  Actualizar Ficha.");
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