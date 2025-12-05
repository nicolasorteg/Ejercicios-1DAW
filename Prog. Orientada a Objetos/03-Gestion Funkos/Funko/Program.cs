using System.Text;
using Funko.Repositories;
using Funko.Services;
using Funko.Utils;
using Funko.Validators;
using Microsoft.VisualBasic.CompilerServices;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day).CreateLogger();
Console.Title = "Gestión Funko";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Main();
Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main() {
    var service = new FunkoService(FunkoRepository.GetInstance(), new FunkoValidator());
    Console.WriteLine("-- 🦸 GESTION DE FUNKOS 🦸 --");
    Utilities.ImprimirMenuPrincipal();
}