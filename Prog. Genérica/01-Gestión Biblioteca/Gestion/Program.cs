using System.Text;
using Gestion.Models;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day).CreateLogger();
Title = "Gestión Biblioteca";
OutputEncoding = Encoding.UTF8;
Clear();
Main();
WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main() {
   

}