using System.Text;
using Gestion.Enum;
using Gestion.Models;
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
    Utilities.ImprimirMusicos(service.GetAllMusicos());
}

void VerPorId(BandaService service) {
    var idIntroducido = int.Parse(BandaValidator.ValidarDato("- ID del Músico: ", BandaValidator.RegexId));
    var musico = service.GetMusicoById(idIntroducido);
    if (musico == null) {
        Console.WriteLine($"🔎❌  No se ha encontrado ningún Músico de ID {idIntroducido}");
        return;
    }
    Console.WriteLine($"🔎✅  Músico encontrado:\n-----------------------\n{musico}");
}

void VerGuitarristas(BandaService service) {
    Utilities.ImprimirGuitarristas(service.GetAllGuitarristas());
}

void VerSlapBase(BandaService service) {
    var nombre = Utilities.PedirNombre();
    var tiempoBanda = Utilities.PedirTiempo();
    var tipoMusico = Utilities.PedirRol();
    var nuevoMusico = new tipoMusico{Nombre = nombre, TiempoEnBanda = tiempoBanda};

    Console.WriteLine(nuevoMusico);
    var confirmacion = BandaValidator.ValidarDato("- ¿Desea guardarlo? (s/n)", BandaValidator.RegexConfirmacion).ToLower();
    if (confirmacion == "s") service.SaveMusico(nuevoMusico);
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