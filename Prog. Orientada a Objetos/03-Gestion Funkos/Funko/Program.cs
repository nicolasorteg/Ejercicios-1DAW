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
        Console.WriteLine("\n-- 🦸 GESTION DE FUNKOS 🦸 --");
        Utilities.ImprimirMenuPrincipal();
        opcion = (OpcionMenuPrincipal)int.Parse(Utilities.ValidarDato("- Opción elegida -> ", FunkoValidator.RegexOpcionMenuPrincipal));
        switch (opcion) {
            case (int)OpcionMenuPrincipal.Salir: break;
            case OpcionMenuPrincipal.VerFunkos: VerFunkos(service); break;
            case OpcionMenuPrincipal.ObtenerFunkoPorId: VerFunkoPorId(service); break;
            case OpcionMenuPrincipal.OrdenarFunkos: OrdenarFunkos(service); break;
            case OpcionMenuPrincipal.CrearFunko: CrearFunko(service); break;
            case OpcionMenuPrincipal.ActualizarFunko: ActualizarFunko(service); break;
            case OpcionMenuPrincipal.EliminarFunko: EliminarFunko(service); break;
            default: // si se entra aquí ha fallado la validacion de la opcion
                Console.WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionMenuPrincipal.Salir} opciones posibles.");
                break;
        }
    } while (opcion != OpcionMenuPrincipal.Salir);
}


void VerFunkos(FunkoService service, TipoOrdenamiento ordenamiento = TipoOrdenamiento.Id) {
    Log.Debug("Mostrando Funkos...");
    var catalogo = service.GetAllFunkos(ordenamiento);
    Utilities.ImprimirCatalogo(catalogo);
}


void VerFunkoPorId(FunkoService service) {
    Log.Debug("Buscando por ID...");
    var idIntroducido = int.Parse(Utilities.ValidarDato("- ID del Funko: ", FunkoValidator.RegexId));
    var funko = service.GetFunkoById(idIntroducido);
    if (funko == null) {
        Console.WriteLine($"🔎❌  No se ha encontrado ningún Funko de ID {idIntroducido}");
        return;
    }
    Console.WriteLine($"🔎✅  Funko encontrado:\n-----------------------\n{funko}");
}


void OrdenarFunkos(FunkoService service) {
    Log.Debug("Ordenando Funkos...");
    Utilities.ImprimirMenuOrdenacion();
    var opcion = (OpcionMenuOrdenacion)int.Parse(Utilities.ValidarDato("- Opción elegida ->", FunkoValidator.RegexOpcionMenuOrdenacion));
    switch (opcion) {
        case OpcionMenuOrdenacion.Salir: break;
        case OpcionMenuOrdenacion.NombreAsc: Utilities.ImprimirCatalogo(service.GetAllFunkos(TipoOrdenamiento.NombreAsc)); break;
        case OpcionMenuOrdenacion.NombreDesc: Utilities.ImprimirCatalogo(service.GetAllFunkos(TipoOrdenamiento.NombreDesc)); break;
        case OpcionMenuOrdenacion.PrecioAsc: Utilities.ImprimirCatalogo(service.GetAllFunkos(TipoOrdenamiento.PrecioAsc)); break;
        case OpcionMenuOrdenacion.PrecioDesc: Utilities.ImprimirCatalogo(service.GetAllFunkos(TipoOrdenamiento.PrecioDesc)); break;
    }
}


void CrearFunko(FunkoService service) {
    Console.WriteLine("No");
}
void ActualizarFunko(FunkoService service) {
    Console.WriteLine("No");
}


void EliminarFunko(FunkoService service) {
    Log.Debug("Eliminando Funko...");
    
    var idIntroducido = int.Parse(Utilities.ValidarDato("- ID del Funko a Eliminar: ", FunkoValidator.RegexId));
    var funko = service.GetFunkoById(idIntroducido);
    if (funko == null) {
        Console.WriteLine($"🔎❌  No se ha encontrado ningún Funko de ID {idIntroducido}");
        return;
    }
    Console.WriteLine($"🔎✅  Funko encontrado:\n-----------------------\n{funko}");
    
    var confirmacion = Utilities.ValidarDato("- ¿Desea eliminarlo? (s/n)", FunkoValidator.RegexConfirmacion).ToLower();
    if (confirmacion == "s") {
        var funkoEliminado = service.DeleteFunko(funko.Id);
        if (funkoEliminado == null) 
            Console.WriteLine("❌  Fallo inesperado en el borrado.");
        Console.WriteLine("🗑️✅  Funko eliminado.");
    }
    else {
        Console.WriteLine("❌  Eliminación cancelada.");
    }
}