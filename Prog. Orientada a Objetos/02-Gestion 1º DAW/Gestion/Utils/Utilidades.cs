using System.Text.RegularExpressions;
using Gestion.Enums;
using Gestion.Models;
using Gestion.Services;
using Serilog;

namespace Gestion.Utils;

public static class Utilidades {
    public static readonly string RegexDni = @"^[0-9]{8}[A-Z]$";
    public static readonly string RegexConfirmacion = @"^[sSnN]$";
    private static readonly string RegexNombre = @"^[A-Za-z]{3,}$";
    private static readonly string RegexEdad = @"^\d{1,}$";
    private static readonly string RegexAsistencia = @"^\d{1,}$";
    private static readonly string RegexNotaProg = @"^([0-9]|10)([,][0-9]+)?";
    private static readonly string RegexRol = @"^(Profesor|Alumno)$";
    public static readonly string RegexOpcionMenuActualizacionAlumno = @$"^[{(int)OpcionMenuActualizacionAlumno.Salir}-{(int)OpcionMenuActualizacionAlumno.NotaProg}]$";
    public static readonly string RegexOpcionMenuActualizacionProfesor = @$"^[{(int)OpcionMenuActualizacionProfesor.Salir}-{(int)OpcionMenuActualizacionAlumno.Edad}]$";

    public static void InicializarDatos(Persona?[] clase) {
        Log.Debug("Inicializando Personas...");
        clase[0] = new Persona { Dni="12345678A", Nombre="Laura Martínez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=1,Retrasos=0,NotaProgramacion=9}};
        clase[1] = new Persona { Dni="98765432B", Nombre="Carlos Pérez", Edad=19, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=3,Retrasos=1,NotaProgramacion=7}};
        clase[2] = new Persona { Dni="11223344C", Nombre="Marta Gómez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=0,Retrasos=2,NotaProgramacion=5}};
        clase[3] = new Persona { Dni="22334455D", Nombre="Sergio López", Edad=20, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=2,Retrasos=0,NotaProgramacion=2}};
        clase[4] = new Persona { Dni="55667788E", Nombre="Jorge Ruiz", Edad=45, Tipo=Persona.TipoPersona.Profesor, Datos=null};
    }
    
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
    
    public static void ImprimirMenuActualizacionAlumno() {
        Console.WriteLine("\n-- MENÚ ACTUALIZACIÓN:");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.Nombre}.- Nombre.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.Edad}.- Edad.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.Faltas}.- Faltas.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.Retrasos}.- Retrasos.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.NotaProg}.- Nota programación.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionAlumno.Salir}.- Salir.");
        Console.WriteLine("------------------------");
    }
    
    public static void ImprimirMenuActualizacionProfesor() {
        Console.WriteLine("\n-- MENÚ ACTUALIZACIÓN:");
        Console.WriteLine($"{(int)OpcionMenuActualizacionProfesor.Nombre}.- Nombre.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionProfesor.Edad}.- Edad.");
        Console.WriteLine($"{(int)OpcionMenuActualizacionProfesor.Salir}.- Salir.");
        Console.WriteLine("------------------------");
    }
    
    public static void ValidarOpcionMenuPrincipal(int opcion, ServicioClase clase) {
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

    public static Persona?[] EliminarNulos(ref Persona?[] clase) {
        Log.Debug("Eliminando nulos de la clase...");
        var numerosPersonas = 0;
        foreach (var persona in clase) {
            if (persona != null) numerosPersonas++;
        }
        var personas = new Persona?[numerosPersonas];
        var index = 0;
        foreach (var persona in clase) {
            if (persona is { } personaValida)
                personas[index++] = personaValida;
        }
        return personas;
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
    
    public static string PedirDni() => ValidarDato("- Introduce el DNI:", RegexDni);
    public static string PedirNombre() => ValidarDato("- Introduce el nombre:", RegexNombre);
    public static int PedirEdad() {
        var isEdadOk = false;
        int edad;
        do {
            edad = int.Parse(ValidarDato("- Introduce la edad:", RegexEdad));
            if (edad >= 0 && edad <= 100) {
                isEdadOk = true;
            } else Console.WriteLine("🔴  Edad incorrecta. Introduce una edad 0-100.");
        } while (!isEdadOk);
        return edad;
    } 
    public static Persona.TipoPersona PedirRol() {
        var isRolOk = false;
        Persona.TipoPersona rol;
        do {
            if (Enum.TryParse(ValidarDato("- Introduce el rol (Profesor/Alumno): ", RegexRol), out rol)) {
                isRolOk = true;
            } else Console.WriteLine("🔴  Rol desconocido. Recuerda que solo puede ser Profesor o Alumno");
        } while (!isRolOk);
        return rol;
    } 
    public static int PedirFaltas() {
        var isFaltasOk = false;
        int faltas;
        do {
            faltas =  int.Parse(ValidarDato("- Introduce las faltas:", RegexAsistencia));
            if (faltas >= 0 && faltas <= 1000) {
                isFaltasOk = true;
            } else Console.WriteLine("🔴  Faltas incorrectas. Introduce un valor entre 0-1000.");
        } while (!isFaltasOk);
        return faltas;
    } 
    public static int PedirRetrasos(){
        var isRetrasosOk = false;
        int retrasos;
        do {
            retrasos =   int.Parse(ValidarDato("- Introduce los retrasos:", RegexAsistencia));
            if (retrasos >= 0 && retrasos <= 1000) {
                isRetrasosOk = true;
            } else Console.WriteLine("🔴  Retrasos incorrectos. Introduce un valor entre 0-1000.");
        } while (!isRetrasosOk);
        return retrasos;
    } 
    public static double PedirNotaProg() {
        var isNotaOk = false;
        double nota;
        do {
            nota = double.Parse(ValidarDato("- Introduce la nueva nota:", RegexNotaProg));
            if (nota >= 0 && nota <= 10) isNotaOk = true;
            else Console.WriteLine("🔴  La nota debe estar entre 0-10");
        } while (!isNotaOk);
        return nota;
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

    public static Persona?[] AñadirEspacio(Persona?[] clase) {
        Log.Debug("Añadiendo nulos a la clase...");
        var numerosPersonas = 0;
        foreach (var persona in clase) {
            if (persona != null) numerosPersonas++;
        }
        var personas = new Persona?[numerosPersonas + 1];
        var index = 0;
        foreach (var persona in clase) {
            if (persona is { } personaValida)
                personas[index++] = personaValida;
        }
        return personas;
    }
}