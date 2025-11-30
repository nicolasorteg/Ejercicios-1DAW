using System.Text.RegularExpressions;
using Gestion.Enums;
using Gestion.Services;
using Serilog;

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
    
    public static void ValidarOpcion(int opcion) {
        switch (opcion) {
            case (int)OpcionMenu.Salir: Log.Debug("Saliendo de la Aplicación..."); break;
            case (int)OpcionMenu.CrearPersona: ServicioClase.CrearPersona(); break;
            case (int)OpcionMenu.VerClase: ServicioClase.VerClase(); break;
            case (int)OpcionMenu.OrdenarPorNota: ServicioClase.OrdenarPorNota(); break;
            case (int)OpcionMenu.OrdenarPorEdad: ServicioClase.OrdenarPorEdad(); break;
            case (int)OpcionMenu.OrdenarPorFaltas: ServicioClase.OrdenarPorFaltas(); break;
            case (int)OpcionMenu.ListarPorRol: ServicioClase.ListarPorRol(); break;
            case (int)OpcionMenu.ActualizarPersona: ServicioClase.ActualizarPersona(); break;
            case (int)OpcionMenu.BorrarPersona: ServicioClase.BorrarPersona(); break;
            default: Console.WriteLine("Error en la lectura."); Log.Error("Validación de opción ha fallado."); break;
        }
    }

    public static string ValidarDato(string msg, string rgx) {
        string input;
        var isDatoOk = false;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
            if (regex.IsMatch(input)) {
                Log.Information($"✅ Dato {input} leído correctamente.");
                isDatoOk = true;
            } else {
                Log.Warning($"⚠️ {input} no es un dato válido para este campo.");
                Console.WriteLine("🔴  Dato introducido inválido.");
            }
        } while (!isDatoOk);
        Console.WriteLine();
        return input;
    }
}