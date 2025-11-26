// ._.
using System.Text;
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
}