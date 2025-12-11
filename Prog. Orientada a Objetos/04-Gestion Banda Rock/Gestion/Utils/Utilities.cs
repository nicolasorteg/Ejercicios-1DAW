using Gestion.Enum;
using Gestion.Models;

namespace Gestion.Utils;

public static class Utilities {

    public static void ImrimirMenuPrincipal() {
        Console.WriteLine("\n--- 🎶 MENU PRINCIPAL 🎶 ---");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerBanda}.-  Ver Integrantes.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerPorId}.-  Ver por Id.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerGuitarristas}.-  Ver posibles Guitarristas.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerSlapBase}.-  Ver Bajistas.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Crear}.-  Crear Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Actualizar}.-  Actualizar Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Borrar}.-  Borrar Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Salir}.-  Salir.");
    }

    public static void ImprimirBanda(Musico[] musicos) {
        foreach(var m in musicos) Console.WriteLine(m);
    }
}