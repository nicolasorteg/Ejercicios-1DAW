using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;

namespace Equipo_Futbol.Utils;

public static class Utilidades {
    
    public static void InicializarJugadoresIniciales(Jugador?[] jugadores) {
        Log.Debug("🔵 Creando jugadores ejemplificativos...");
        var j1 = new Jugador("12345678A", "Iker", 38, 1, PosicionJugador.Portero, 0, 1);
        jugadores[0] = j1;
        var j2 = new Jugador("23456789B", "Sergio", 35, 3, PosicionJugador.Defensa, 20, 44);
        jugadores[1] = j2;
        var j3 = new Jugador("34567890C", "Luka", 19, 10, PosicionJugador.Mediocentro, 60, 130);
        jugadores[2] = j3;
        var j4 = new Jugador("45678901D", "Karim", 36, 9, PosicionJugador.Delantero, 300, 100);
        jugadores[3] = j4;
        var j5 = new Jugador("56789012E", "Cristiano", 37, 7, PosicionJugador.Delantero, 900, 150);
        jugadores[4] = j5;
        Log.Information($"✅ Jugadores iniciales creados y asignados a la plantilla.");
    }

    public static void ImprimirMenu() {
        Console.WriteLine();
        Console.WriteLine("---------- ⚽ GESTIÓN CD LEGANÉS ⚽ ----------");
        Console.WriteLine($"{(int)OpcionMenu.CrearJugador}.- Fichar Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.VerPlantilla}.- Ver plantilla.");
        Console.WriteLine($"{(int)OpcionMenu.OrdenarPorGoles}.- Ordenar por Goles.");
        Console.WriteLine($"{(int)OpcionMenu.ListarPorPosicion}.- Listar por Posición.");
        Console.WriteLine($"{(int)OpcionMenu.ActualizarJugador}.- Actualizar Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.BorrarJugador}.- Despedir Jugador.");
        Console.WriteLine($"{(int)OpcionMenu.Salir}.- Salir.");
        Console.WriteLine("-------------------------------------------");
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