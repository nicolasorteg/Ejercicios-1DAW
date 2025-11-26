// ._.
using System.Text;
using Serilog;

// constantes globales


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
}