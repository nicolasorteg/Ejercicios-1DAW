// ._.
using System.Text;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;

// constantes globales
const int JugadoresIniciales = 5;

// config inicial
Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/logs.txt").MinimumLevel.Debug().CreateLogger();
Console.Title = "Gestor Plantilla";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(args); // daw's template

Console.WriteLine("🖖 Pulse cualquier tecla para salir.");
Console.ReadKey();
return;

void Main(string[] args) {
    Log.Debug("🔵 Iniciando Main...");

    // creacion array contenedor de jugadores
    var plantilla = new Jugador?[JugadoresIniciales]; // ahora mismo lleno de nulos
    InicializarJugadoresIniciales(plantilla);

}



// funciones auxiliares
void InicializarJugadoresIniciales(Jugador?[] jugadores) {
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
    Log.Information($"✅ {JugadoresIniciales} jugadores iniciales creados y asignados a la plantilla.");
}
