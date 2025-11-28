using System.Text;
using Serilog;

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.txt").CreateLogger();
Console.Title = "Gestión 1º DAW";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Main(args);
Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;


void Main(string[] args) {
    Log.Debug("Iniciando el Main...");
}