using System.Diagnostics;
using System.Text;
using Funko.Enums;
using Funko.Repositories;
using Funko.Services;
using Funko.Utils;
using Funko.Validators;
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
    OpcionMenuPrincipal opcion;
    do {
        Console.WriteLine("-- 🦸 GESTION DE FUNKOS 🦸 --");
        Utilities.ImprimirMenuPrincipal();
        opcion = (OpcionMenuPrincipal)int.Parse(Utilities.ValidarDato("- Opción elegida -> ", FunkoValidator.RegexOpcionMenuPrincipal));
        switch (opcion) {
            case (int)OpcionMenuPrincipal.Salir: break;
            case OpcionMenuPrincipal.VerFunkos: VerFunkos(service); break;
            case OpcionMenuPrincipal.ObtenerFunkoPorId: VerFunkoPorId(service); break;
            case OpcionMenuPrincipal.OrdenarFunkos: OrdenarFUnkos(service); break;
            case OpcionMenuPrincipal.CrearFunko: CrearFunko(service); break;
            case OpcionMenuPrincipal.ActualizarFunko: ActualizarFunko(service); break;
            case OpcionMenuPrincipal.EliminarFunko: EliminarFunko(service); break;
            default: // si se entra aquí ha fallado la validacion de la opcion
                Console.WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionMenuPrincipal.Salir} opciones posibles.");
                break;
        }
    } while (opcion != OpcionMenuPrincipal.Salir);
}


void VerFunkos(FunkoService service) {
    Console.WriteLine("No");
}
void VerFunkoPorId(FunkoService service) {
    Console.WriteLine("No");
}
void OrdenarFUnkos(FunkoService service) {
    Console.WriteLine("No");
}

void CrearFunko(FunkoService service) {
    Console.WriteLine("No");
}
void ActualizarFunko(FunkoService service) {
    Console.WriteLine("No");
}
void EliminarFunko(FunkoService service) {
    Console.WriteLine("No");
}



