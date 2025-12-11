using System.Text;
using Gestion.Config;
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
            case OpcionMenuPrincipal.TocarGuitarra: TocarGuitarra(service); break;
            case OpcionMenuPrincipal.HacerSolo: HacerSolo(service); break;
            case OpcionMenuPrincipal.Cantar: Cantar(service); break;
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
    Utilities.ImprimirBajistas(service.GetAllBajistas());
}


void Crear(BandaService service) {
    var nombre = Utilities.PedirNombre();
    var tiempoBanda = Utilities.PedirTiempo();
    var rolMusico = Utilities.PedirRol();
    
    switch (rolMusico) {
        case "Baterista":
            var nuevoBaterista = new Baterista { Nombre = nombre, TiempoEnBanda = tiempoBanda };
            Console.WriteLine(nuevoBaterista);
            var confirmacionBt = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacionBt == "s") {
                service.SaveMusico(nuevoBaterista);
                Console.WriteLine("✅  Baterista guardado.");
            }
            break;
        case "Bajista":
            var nuevoBajista = new Bajista { Nombre = nombre, TiempoEnBanda = tiempoBanda };
            Console.WriteLine(nuevoBajista);
            var confirmacionBj = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacionBj == "s") {
                service.SaveMusico(nuevoBajista);
                Console.WriteLine("✅  Bajista guardado.");
            }
            break;
        case "Guitarrista":
            var nuevoGuitarrista = new Guitarrista { Nombre = nombre, TiempoEnBanda = tiempoBanda };
            Console.WriteLine(nuevoGuitarrista);
            var confirmacionG = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacionG == "s") {
                service.SaveMusico(nuevoGuitarrista);
                Console.WriteLine("✅  Guitarrista guardado.");
            }
            break;
        case "Cantante":
            var nuevoCantante = new Cantante { Nombre = nombre, TiempoEnBanda = tiempoBanda };
            Console.WriteLine(nuevoCantante);
            var confirmacionC = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacionC == "s") {
                service.SaveMusico(nuevoCantante);
                Console.WriteLine("✅  Cantante guardado.");
            }
            break;
    }
}

void Actualizar(BandaService service) {
    var musico = Utilities.GetMusico(service);
    if (musico == null) return;
    
    Utilities.ImprimirMenuActualizar();
    var opcion = (OpcionMenuActualizar)int.Parse(BandaValidator.ValidarDato("- Opción elegida ->", BandaValidator.RegexOpcionMenuActualizacion));
    switch (opcion) {
        case OpcionMenuActualizar.Salir: 
        default: break;
        case OpcionMenuActualizar.Nombre:
            var nombre = Utilities.PedirNombre();
            var nuevoFunkoN = musico with { Nombre = nombre };
            Console.WriteLine(nuevoFunkoN);
            var confirmacion = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacion == "s") {
                service.UpdateFunko(nuevoFunkoN);
                Console.WriteLine("✅  Funko actualizado.");
            }
            break;
        case OpcionMenuActualizar.TiempoBanda:
            var tiempo = Utilities.PedirTiempo();
            var nuevoMusicoT = musico with { TiempoEnBanda = tiempo };
            Console.WriteLine(nuevoMusicoT);
            var confirmacionCategoria = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
            if (confirmacionCategoria == "s") {
                service.UpdateFunko(nuevoMusicoT);
                Console.WriteLine("✅  Músico actualizado.");
            }
            break;
    }
}

void Borrar(BandaService service) {
    var musico = Utilities.GetMusico(service);
    if (musico == null) return;
    var confirmacion = BandaValidator.ValidarDato(Configuration.MensajeConfirmacion, BandaValidator.RegexConfirmacion).ToLower();
    if (confirmacion == "s") {
        var musicoEliminado = service.DeleteMusico(musico.Id);
        if (musicoEliminado == null) {
            Console.WriteLine("❌  Fallo inesperado en el borrado.");
            return;
        }
        Console.WriteLine("🗑️✅  Músico eliminado.");
    }
    else {
        Console.WriteLine("❌  Eliminación cancelada.");
    }
}

void TocarGuitarra(BandaService service) {
    var musico = Utilities.GetMusico(service);
    if (musico == null) return;
    if (musico is not IGuitarra guitarrista) {
        Console.WriteLine("🎸🔴  El músico seleccionado no puede tocar la guitarra.");
        
    } else guitarrista.TocarGuitarra();
}
void HacerSolo(BandaService service) {
    var musico = Utilities.GetMusico(service);
    if (musico == null) return;
    if (musico is not Guitarrista guitarrista) {
        Console.WriteLine("🎸🔴  El músico seleccionado no puede hacer un solo.");
        
    } else guitarrista.RealizarSolo();
}
void Cantar(BandaService service) {
    var musico = Utilities.GetMusico(service);
    if (musico == null) return;
    if (musico is not IMicrofono cantante) {
        Console.WriteLine("🎤🔴  El músico seleccionado no puede cantar.");
        
    } else cantante.Cantar();
}

// asi se haria con todos los metodos, tocar bateria, hacer slap base, etc.