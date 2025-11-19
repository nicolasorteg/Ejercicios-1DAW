// ._.

using System.Globalization;
using System.Text;
using Serilog;
using static System.Console;

// zona de constantes


var random = Random.Shared;
Log.Logger = new LoggerConfiguration().WriteTo.File("logs/log.txt").MinimumLevel.Debug().CreateLogger();
var localeEs = new CultureInfo("es-ES");
Title = "Piscina Infectada";
OutputEncoding = Encoding.UTF8;
Clear();

Main(args);

WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main(string[] args) {
    Console.WriteLine("Hola Caracola");
}