using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;

namespace Equipo_Futbol.Utils;

public static class Utilidades {

    public static void InicializarJugadoresIniciales(Jugador?[] jugadores) {
        Log.Debug("🔵 Creando jugadores ejemplificativos...");
        var j1 = new Jugador("12345678A", "Iker", 38, 1, Jugador.Posiciones.Portero, 0, 1);
        jugadores[0] = j1;
        var j2 = new Jugador("23456789B", "Sergio", 35, 3, Jugador.Posiciones.Defensa, 20, 44);
        jugadores[1] = j2;
        var j3 = new Jugador("34567890C", "Luka", 19, 10, Jugador.Posiciones.Mediocentro, 60, 130);
        jugadores[2] = j3;
        var j4 = new Jugador("45678901D", "Karim", 36, 9, Jugador.Posiciones.Delantero, 300, 100);
        jugadores[3] = j4;
        var j5 = new Jugador("56789012E", "Cristiano", 37, 7, Jugador.Posiciones.Delantero, 900, 150);
        jugadores[4] = j5;
        Log.Information($"✅ Jugadores iniciales creados y asignados a la plantilla.");
    }

    public static void ImprimirMenu() {
        Console.WriteLine("--------- ⚽  GESTIÓN CD LEGANÉS ⚽  ---------");
        Console.WriteLine($"{(int)OpcionMenu.CrearJugador}.- Fichar Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.VerPlantilla}.- Ver plantilla.");
        Console.WriteLine($"{(int)OpcionMenu.OrdenarPorGoles}.- Ordenar por Goles.");
        Console.WriteLine($"{(int)OpcionMenu.ListarPorPosicion}.- Listar por Posición.");
        Console.WriteLine($"{(int)OpcionMenu.ActualizarJugador}.- Actualizar Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.BorrarJugador}.- Despedir Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.Salir}.- Salir.");
        Console.WriteLine("-------------------------------------------");
    }

    public static void ImprimirMenuPosicion() {
        Console.WriteLine("1.- Portero.");
        Console.WriteLine("2.- Defensa.");
        Console.WriteLine("3.- Mediocentro.");
        Console.WriteLine("4.- Delantero.");
    }
    

    public static string ValidarDato(string msg, string rgx) {
        string input;
        var isDatoOk = false;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim().ToUpper() ?? "-1";
            if (regex.IsMatch(input)) {
                Log.Information($"✅ Dato {input} leído correctamente.");
                isDatoOk = true;
            }
            else {
                Log.Warning($"⚠️ {input} no es un dato válido para este campo.");
                Console.WriteLine("🔴  Dato introducido inválido.");
            }
        } while (!isDatoOk);
        Console.WriteLine();
        return input;
    }
    
    public static void OrdenarPlantilla(Jugador[] plantillaSinNulos) {
        for (var i = 0; i < plantillaSinNulos.Length - 1; i++) {
            var swapped = false;
            for (var j = 0; j < plantillaSinNulos.Length - i - 1; j++) {
                if (plantillaSinNulos[j] is {} jugadorValido)
                    if (jugadorValido.Goles < plantillaSinNulos[j + 1].Goles) {
                        (plantillaSinNulos[j], plantillaSinNulos[j + 1]) = (plantillaSinNulos[j + 1], plantillaSinNulos[j]);
                        swapped = true;
                    }
            }
            if (!swapped) return; // si no se ha hecho un swap en esta pasada es pq ya está ordenado
        }
    }

    public static bool ValidarLetraDni(string dni) {
        const string LetrasDni = "TRWAGMYFPDXBNJZSQVHLCKE";
        var letraDni = dni.Substring(8, 1).ToUpper(); // el ToUpper ya se hace en ValidarDato
        var numerosDni = int.Parse(dni.Substring(0, 8));
        var indiceCorrecto = numerosDni % 23;
        return letraDni == LetrasDni[indiceCorrecto].ToString();
    }
    

    public static bool IsJugadorInPlantilla(Jugador?[] plantilla, string dni) {
        foreach (var jugador in plantilla) {
            if(jugador is {} jugadorValido)
                if (jugadorValido.Dni == dni) return true;
        }
        return false;
    }
    public static void ImprimirJugadorPorPosicion(Jugador?[] plantilla, string posicion) {

        bool isCargoOk = Enum.TryParse(posicion, out Jugador.Posiciones posicionFinal);
        Console.WriteLine($"-- {posicion}s ⚽ "); 
        // solo imprime si el posicionFinal es igual al cargo que tiene
        foreach (var jugador in plantilla) {
            if (jugador is { } empleadoValido) {
                if (empleadoValido.Posicion == posicionFinal) Console.WriteLine(empleadoValido);
            }
        }
        Console.WriteLine();
    }

    public static void GuardarJugadorEnPlantilla(Jugador nuevoJugador, ref Jugador?[] plantilla) {
        var index = -1;
        for (var i = 0; i < plantilla.Length; i++) {
            if (plantilla[i] == null) {
                index = i;
                break;
            }
        }
        if (index != -1) {
            plantilla[index] = nuevoJugador;
            Log.Information($"✅ Jugador {nuevoJugador.Dni} guardado.");
            Console.WriteLine($"✅ Jugador {nuevoJugador.Dni} guardado correctamente. Es el {index + 1}º Jugador de la plantilla.");
        } else {
            var nuevaPlantilla = new Jugador?[plantilla.Length + 1];
            Array.Copy(plantilla, nuevaPlantilla, plantilla.Length);
            nuevaPlantilla[nuevaPlantilla.Length - 1] = nuevoJugador;
            plantilla = nuevaPlantilla; 
            Log.Information($"✅ Jugador {nuevoJugador.Dni} guardado en la posición {plantilla.Length}");
            Console.WriteLine($"✅  Jugador guardado correctamente. Es el {plantilla.Length}º Jugador de la plantilla.");
        }
        Console.WriteLine();
    }

    public static void ImprimirMenuActualizar() {
        Console.WriteLine($"{(int)OpcionActualizar.Nombre}.- Nombre.");
        Console.WriteLine($"{(int)OpcionActualizar.Edad}.- Edad.");
        Console.WriteLine($"{(int)OpcionActualizar.Dorsal}.- Dorsal.");
        Console.WriteLine($"{(int)OpcionActualizar.Posicion}.- Posicion.");
        Console.WriteLine($"{(int)OpcionActualizar.Goles}.- Goles.");
        Console.WriteLine($"{(int)OpcionActualizar.Asistencias}.- Asistencias.");
        Console.WriteLine($"{(int)OpcionActualizar.Salir}.- Salir.");
    }

    public static void MostrarConfirmacionActualizacion(Jugador jugadorValido) {
        Console.WriteLine("✅  Jugador actualizado.");
        Console.WriteLine(jugadorValido);
        Console.WriteLine();
    }
}