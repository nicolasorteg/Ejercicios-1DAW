using System.Text.RegularExpressions;
using Gestion.Enums;
using Gestion.Models;
using Gestion.Services;
using Serilog;

namespace Gestion.Utils;

public static class Utilidades {
    public static readonly string RegexId = @"^[0-9]{8}[A-Z]$"; 
    public static readonly string RegexConfirmacion = @"^[sSnN]$"; 

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
        Console.WriteLine("--------------------------");
    }

    
    public static void ValidarOpcion(int opcion, ServicioClase clase) {
        switch (opcion) {
            case (int)OpcionMenu.Salir: Log.Debug("Saliendo de la Aplicación..."); break;
            case (int)OpcionMenu.CrearPersona: clase.CrearPersona(); break;
            case (int)OpcionMenu.VerClase: clase.VerClase(); break;
            case (int)OpcionMenu.OrdenarPorNota: clase.OrdenarPorNota(); break;
            case (int)OpcionMenu.OrdenarPorEdad: clase.OrdenarPorEdad(); break;
            case (int)OpcionMenu.OrdenarPorFaltas: clase.OrdenarPorFaltas(); break;
            case (int)OpcionMenu.ListarPorRol: clase.ListarPorRol(); break;
            case (int)OpcionMenu.ActualizarPersona: clase.ActualizarPersona(); break;
            case (int)OpcionMenu.BorrarPersona: clase.BorrarPersona(); break;
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

    public static void ImprimirDatosPorRol(Persona?[] clase, Persona.TipoPersona tipoPersona) {
        foreach (var persona in clase)
            if (persona is {} personaValida)
                if (personaValida.Tipo == tipoPersona) Console.WriteLine($"- {personaValida}");
    }

    public static bool IsDniInClase(Persona?[] clase, string dni, ref int posicion) {
        for (var i = 0; i < clase.Length; i++) {
            if (clase[i] is { } personaValida) {
                if (personaValida.Dni == dni) {
                    Console.WriteLine(personaValida);
                    posicion = i;
                    Log.Information($"Persona encontrada con DNI {dni}");
                    return true;
                }
            }
        }
        return false;
    }

    public static void RedimensionarSiEsNecesario(ref Persona?[] clase) {
        // redimensiona epicamente
    }
}