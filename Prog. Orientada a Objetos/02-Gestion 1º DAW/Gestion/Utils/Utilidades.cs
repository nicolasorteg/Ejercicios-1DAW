using Gestion.Enums;

namespace Gestion.Utils;

public static class Utilidades {
    public static void ImprimirMenuPrincipal() {
        Console.WriteLine("--- 🏫 GESTIÓN 1º DAW ---");
        Console.WriteLine($"{(int)OpcionMenu.CrearPersona}.- Crear Persona.");
        Console.WriteLine($"{(int)OpcionMenu.VerClase}.- Ver Clase.");
        Console.WriteLine($"{(int)OpcionMenu.OrdenarPorNota}.- Ordenar por Nota.");
        Console.WriteLine($"{(int)OpcionMenu.OrdenarPorEdad}.- Ordenar por Edad.");
        Console.WriteLine($"{(int)OpcionMenu.OrdenarPorFaltas}.- Ordenar por Faltas.");
        Console.WriteLine($"{(int)OpcionMenu.ListarPorRol}.- Listar por Rol.");
        Console.WriteLine($"{(int)OpcionMenu.ActualizarPersona}.- Actualizar Persona.");
        Console.WriteLine($"{(int)OpcionMenu.BorrarPersona}.- Borrar Persona.");
        Console.WriteLine($"{(int)OpcionMenu.Salir}.- Salir.");
    }
}