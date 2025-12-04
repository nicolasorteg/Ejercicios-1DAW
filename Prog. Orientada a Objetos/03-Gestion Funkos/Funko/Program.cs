using System.Text;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/cine_debug-.log", rollingInterval: RollingInterval.Day).CreateLogger();
Console.Title = "Gestión Funko";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Main();
Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main() {
    
}