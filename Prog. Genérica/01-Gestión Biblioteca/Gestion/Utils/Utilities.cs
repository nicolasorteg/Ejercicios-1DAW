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
}