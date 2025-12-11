using System.Text;
using Gestion.Enum;
using Gestion.Repositories;
using Gestion.Services;
using Gestion.Utils;
using Gestion.Validators;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day).CreateLogger();
Console.Title = "Gestión Banda Rock";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Main();
Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main() {
    Console.WriteLine("-- BIENVENIDO A LA GESTIÓN DE LA BANDA DE ROCK --");
    var service = new BandaService(BandaRepository.GetInstance(), new BandaValidator());
    OpcionMenuPrincipal opcion;
    do {
        Utilities.ImrimirMenuPrincipal();
        opcion = (OpcionMenuPrincipal)int.Parse(BandaValidator.ValidarDato("- Opción elegida -> ", BandaValidator.RegexOpcionMenuPrincipal));
        switch (opcion) {
            case OpcionMenuPrincipal.Salir: break;
            case OpcionMenuPrincipal.VerBanda: VerBanda(service); break;
            case OpcionMenuPrincipal.VerPorId: VerPorId(service); break;
            case OpcionMenuPrincipal.VerGuitarristas: VerGuitarristas(service); break;
            case OpcionMenuPrincipal.VerSlapBase: VerSlapBase(service); break;
            case OpcionMenuPrincipal.Crear: Crear(service); break;
            case OpcionMenuPrincipal.Actualizar: Actualizar(service); break;
            case OpcionMenuPrincipal.Borrar: Borrar(service); break;
            default: // si se entra aquí ha fallado la validacion de la opcion
                Console.WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionMenuPrincipal.Salir} opciones posibles.");
                break;
        }
    } while (opcion != OpcionMenuPrincipal.Salir);
}



void VerBanda(BandaService service) {
    throw new NotImplementedException();
}

void VerPorId(BandaService service) {
    throw new NotImplementedException();
}

void VerGuitarristas(BandaService service) {
    throw new NotImplementedException();
}

void VerSlapBase(BandaService service) {
    throw new NotImplementedException();
}

void Crear(BandaService service) {
    throw new NotImplementedException();
}

void Actualizar(BandaService service) {
    throw new NotImplementedException();
}

void Borrar(BandaService service) {
    throw new NotImplementedException();
}