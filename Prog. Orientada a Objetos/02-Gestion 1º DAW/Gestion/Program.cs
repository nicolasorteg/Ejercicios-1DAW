using System.Text;
using Gestion.Enums;
using Gestion.Services;
using Gestion.Utils;
using Serilog;

const string RegexOpcionMenuPrincipal = @"^[0-8]$";

// config daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.txt").CreateLogger();
Console.Title = "Gestión 1º DAW";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Main();
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main() {
    Log.Debug("Iniciando el Main...");
    var clase = new ServicioClase();
    int opcionElegida;
    do {
        Utilidades.ImprimirMenuPrincipal();
        opcionElegida = int.Parse(Utilidades.ValidarDato("- Introduzca la opción: ", RegexOpcionMenuPrincipal));
        Utilidades.ValidarOpcion(opcionElegida, clase);
    } while (opcionElegida != (int)OpcionMenu.Salir);
}