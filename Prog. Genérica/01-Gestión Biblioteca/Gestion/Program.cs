using System.Text;
using Gestion.Enums;
using Gestion.Models;
using Gestion.Utils;
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
   WriteLine("-- Gestión Biblioteca --");
   OpcionMenuPrincipal opcion;
   do {
      Utilities.ImrimirMenuPrincipal();
      //opcion
   }
}