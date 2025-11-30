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
        Console.WriteLine("\n--- 🏫 GESTIÓN 1º DAW ---");
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

    public static Persona?[] RedimensionarClase(ref Persona?[] clase) {
        Log.Debug("Redimensionando la clase...");
        var numerosPersonas = 0;
        foreach (var persona in clase) {
            if (persona != null) numerosPersonas++;
        }
        var personas = new Persona?[numerosPersonas];
        for (var i = 0;  i < clase.Length; i++) {
            if (clase[i] is { } personaValida)
                personas[i] = personaValida;
        }
        return personas;
    }
    

    public static void InicializarDatos(Persona?[] clase) {
        Log.Debug("Inicializando Personas...");
        clase[0] = new Persona { Dni="12345678A", Nombre="Laura Martínez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=1,Retrasos=0,NotaProgramacion=9}};
        clase[1] = new Persona { Dni="98765432B", Nombre="Carlos Pérez", Edad=19, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=3,Retrasos=1,NotaProgramacion=7}};
        clase[2] = new Persona { Dni="11223344C", Nombre="Marta Gómez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=0,Retrasos=2,NotaProgramacion=5}};
        clase[3] = new Persona { Dni="22334455D", Nombre="Sergio López", Edad=20, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=2,Retrasos=0,NotaProgramacion=2}};
        clase[4] = new Persona { Dni="55667788E", Nombre="Jorge Ruiz", Edad=45, Tipo=Persona.TipoPersona.Profesor, Datos=null};
    }

    public static void OrdenarClase(Persona?[] clase, string condicion) {
        for (var i = 0; i < clase.Length - 1; i++) {
            var swapped = false;
            for (var j = 0; j < clase.Length - i - 1; j++) {
                if (clase[j] != null) {
                    if (condicion == "Nota") {
                        if (clase[j]?.Datos?.NotaProgramacion < clase[j + 1]?.Datos?.NotaProgramacion) {
                            (clase[j], clase[j + 1]) = (clase[j + 1], clase[j]);
                            swapped = true;
                        }
                    } else if (condicion == "Edad") {
                        if (clase[j]?.Edad < clase[j + 1]?.Edad) {
                            (clase[j], clase[j + 1]) = (clase[j + 1], clase[j]);
                            swapped = true;
                        }
                    } else if (condicion == "Faltas") {
                        if (clase[j]?.Datos?.Faltas < clase[j + 1]?.Datos?.Faltas) {
                            (clase[j], clase[j + 1]) = (clase[j + 1], clase[j]);
                            swapped = true;
                        }
                    }
                }
            }
            if (!swapped) return;
        }
    }
}