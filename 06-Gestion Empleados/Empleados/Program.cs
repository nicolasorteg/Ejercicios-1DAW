// ._.

using System.Text;
using Serilog;

// zona de constantes

const int EmpleadosIniciales = 15;
const int EmpleadosMaximos = 50;

var random = Random.Shared;

// config. del logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();

// titulo de la terminal 
Console.Title = "Gestión Empleados";
// emojis en terminal
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(args);

// para no salir directamente
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main(string[] args) {
    Log.Debug("🔵 Iniciando el Main...");
    Console.WriteLine("Diego's template configurada");
}