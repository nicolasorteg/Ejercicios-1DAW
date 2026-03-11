using System.Text;
using System.Text.RegularExpressions;
using Gestion.Config;
using Gestion.Enums;
using Gestion.Exceptions;
using Gestion.Models;
using Gestion.Repositories;
using Gestion.Services;
using Gestion.Utils;
using Gestion.Validators;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day).CreateLogger();
Title = "Gestión Empresa Héroes";
OutputEncoding = Encoding.UTF8;
Clear();
Main();
Log.CloseAndFlush();
WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main() {
   
    var service = new EmpresaService(HeroeRepository.GetInstance(), MisionRepository.GetInstance(), new HeroeValidator(), new MisionValidator());
    
    WriteLine("--- 🦸 BIENVENIDO A LA EMPRESA DE HÉROES 🦸 ---");
    WriteLine("-----------------------------------------------");
    OpcionesMenuPrincipal opcion;
    do {
        Utilities.ImprimirMenu();
        opcion = (OpcionesMenuPrincipal)int.Parse(ValidarDato("- Opción elegida -> ", HeroesConfig.RegexOpcionMenuPrincipal));

        try {
            switch (opcion) {
                case OpcionesMenuPrincipal.Salir: break;
                case OpcionesMenuPrincipal.CrearHeroe: CrearHeroe(service); break;
                case OpcionesMenuPrincipal.ObtenerHeroes: ObtenerHeroes(service); break;
                case OpcionesMenuPrincipal.BuscarHeroePorId: BuscarHeroePorId(service); break;
                case OpcionesMenuPrincipal.ActualizarHeroe: ActualizarHeroe(service);break;
                case OpcionesMenuPrincipal.BorrarHeroe: BorrarHeroe(service); break;
                case OpcionesMenuPrincipal.CrearMision: CrearMision(service); break;
                case OpcionesMenuPrincipal.ObtenerMision: ObtenerMisiones(service); break;
                case OpcionesMenuPrincipal.BuscarMisionPorId: BuscarMisionPorId(service); break;
                case OpcionesMenuPrincipal.ActualizarMision: ActualizarMision(service); break;
                case OpcionesMenuPrincipal.BorrarMision: BorrarMision(service); break;
                case OpcionesMenuPrincipal.SimularMision: SimularMision(service); break;
                case OpcionesMenuPrincipal.DescansarHeroe: DescansarHeroe(service);break;
                case OpcionesMenuPrincipal.CalcularPoderHeroe: CalcularPoder(service); break;
                case OpcionesMenuPrincipal.AsignarHeroeAMision: AsignarHeroeAMision(service); break;
                default: // si se entra aquí ha fallado la validacion de la opcion
                    WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionesMenuPrincipal.AsignarHeroeAMision} opciones posibles.");
                    break;
            } 
        } 
        catch (HeroeExceptions.Validation ex) {
            WriteLine(ex.Message);
            foreach (var error in ex.Errores) WriteLine($"  -> {error}");
        } 
        catch (HeroeExceptions.NotFound ex) {
            WriteLine(ex.Message);
        }
        catch (HeroeExceptions.AlreadyExists ex) {
            WriteLine(ex.Message);
        }
        catch (HeroeExceptions.VidaExcesiva ex) {
            WriteLine(ex.Message);
        }
        catch (Exception) {
            WriteLine("⚠️ Ha ocurrido un error inesperado.");
        }
        
    } while (opcion != OpcionesMenuPrincipal.Salir);
}


// ----------- opciones
void CrearHeroe(EmpresaService service) {
    var nombre = ValidarDato("- Nombre -> ", HeroesConfig.RegexNombre);
    var nivel = int.Parse(ValidarDato("- Nivel -> ", HeroesConfig.RegexNivel));
    var energia = int.Parse(ValidarDato("- Energía -> ", HeroesConfig.RegexEnergia));
    var xp = int.Parse(ValidarDato("- XP -> ", HeroesConfig.RegexXp));
    
    WriteLine("0:Común, 1:Especial, 2:Raro, 3:Épico, 4:Legendario");
    var rarezaInput = int.Parse(ValidarDato("- Rareza (0-4) -> ", HeroesConfig.RegexRarezas));
    
    WriteLine("Selecciona el tipo: 1. Fuerte | 2. Ágil | 3. Inteligente");
    var tipoHeroe = int.Parse(ValidarDato("- Tipo de Héroe", HeroesConfig.RegexTipoHeroe));
    
    Heroe nuevoHeroe;

    switch (tipoHeroe) {
        case 1:
            var agilidad = int.Parse(ValidarDato("- Agilidad -> ", HeroesConfig.RegexAgilidadFuerza));
            nuevoHeroe = new HeroeAgil { Nombre = nombre, Nivel = nivel, Energia = energia, Experiencia = xp, Rareza = (Heroe.Rarezas)HeroesConfig.ValoresRealesRarezas[rarezaInput], Agilidad = agilidad };
            break;
        case 2:
            var fuerza = int.Parse(ValidarDato("- Fuerza -> ", HeroesConfig.RegexAgilidadFuerza));
            nuevoHeroe = new HeroeFuerte { Nombre = nombre, Nivel = nivel, Energia = energia, Experiencia = xp, Rareza = (Heroe.Rarezas)HeroesConfig.ValoresRealesRarezas[rarezaInput], Fuerza = fuerza };
            break;
        default:
            WriteLine("Niveles: 0:Baja, 1:Media, 2:Alta");
            var inteligencia = int.Parse(ValidarDato("- Inteligencia -> ", HeroesConfig.RegexInteligencia));
            nuevoHeroe = new HeroeInteligente { Nombre = nombre, Nivel = nivel, Energia = energia, Experiencia = xp, Rareza = (Heroe.Rarezas)HeroesConfig.ValoresRealesRarezas[rarezaInput], Inteligencia = (HeroeInteligente.Inteligencias)HeroesConfig.ValoresRealesInteligencias[inteligencia] };
            break;
    }
    WriteLine(nuevoHeroe);
    var confirmacionIn = ValidarDato(HeroesConfig.MensajeConfirmacion, HeroesConfig.RegexConfirmacion).ToLower();
    if (confirmacionIn == "n") return;
    
    var creado = service.SaveHeroe(nuevoHeroe);
    WriteLine($"\n✅  Héroe {creado.Nombre} creado con éxito. ID: {creado.Id}");
}

void ObtenerHeroes(EmpresaService service) {
    Utilities.ImprimirMenuObtenerHeroes();
    var opcion = (OpcionObtenerHeroes)int.Parse(ValidarDato("- Opción elegida -> ", HeroesConfig.RegexOpcionMenuOrdenacion));
    switch (opcion) {
        case OpcionObtenerHeroes.Salir: break;
        case OpcionObtenerHeroes.ObtenerPorId: 
            var heroesId = service.GetAllHeroes();
            foreach (var h in heroesId) WriteLine(h);
            break;
        case OpcionObtenerHeroes.ObtenerPorNivel:
            var heroesLvl = service.GetAllOrderByLevel();
            foreach (var h in heroesLvl) WriteLine(h);
            break;
        case OpcionObtenerHeroes.ObtenerPorPoder:
            var heroesPoder = service.GetAllOrderByPower();
            foreach (var h in heroesPoder) WriteLine(h);
            break;
        default: // si se entra aquí ha fallado la validacion de la opcion
            WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionObtenerHeroes.ObtenerPorPoder} opciones posibles.");
            break;
    }
}

void BuscarHeroePorId(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID del Héroe: ", HeroesConfig.RegexId));
    var h = service.GetHeroeById(id);
    WriteLine($"🔎✅  Héroe encontrado:\n-----------------------\n{h}");
}

void ActualizarHeroe(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID del Héroe a actualizar: ", HeroesConfig.RegexId));
    var h = service.GetHeroeById(id);
    Heroe nuevoHeroe;
    WriteLine(h);
    Utilities.ImprimirMenuActualizarHeroes();
    var opcion = (OpcionMenuActualizarHeroe)int.Parse(ValidarDato("- Opción elegida -> ", HeroesConfig.RegexOpcionMenuActualizarHeroe));
    switch (opcion) {
        case OpcionMenuActualizarHeroe.Salir: return;
        case OpcionMenuActualizarHeroe.Nombre:
            nuevoHeroe = h with { Nombre = ValidarDato("- Nombre -> ", HeroesConfig.RegexNombre) };
            break;
        
        case OpcionMenuActualizarHeroe.Nivel:
            nuevoHeroe = h with { Nivel = int.Parse(ValidarDato("- Nivel -> ", HeroesConfig.RegexNivel)) };
            break;
        
        case OpcionMenuActualizarHeroe.Energia:
            nuevoHeroe = h with { Energia = int.Parse(ValidarDato("- Energía -> ", HeroesConfig.RegexEnergia)) };
            break;
        
        case OpcionMenuActualizarHeroe.Experiencia:
            var xp = int.Parse(ValidarDato("- XP -> ", HeroesConfig.RegexXp));
            if (h.Nivel < HeroesConfig.UmbralesNivel.Length && xp >= HeroesConfig.UmbralesNivel[h.Nivel]) {
                WriteLine($"⚠️ Error: La XP no puede ser mayor o igual a {HeroesConfig.UmbralesNivel[h.Nivel]} para el nivel {h.Nivel}.");
                return;
            }
            nuevoHeroe = h with { Experiencia = xp };
            break;
        
        case OpcionMenuActualizarHeroe.Rareza:
            WriteLine("0:Común, 1:Especial, 2:Raro, 3:Épico, 4:Legendario");
            var rarezaInput = int.Parse(ValidarDato("- Nueva Rareza (0-4) -> ", HeroesConfig.RegexRarezas));
            nuevoHeroe = h with { Rareza = (Heroe.Rarezas)HeroesConfig.ValoresRealesRarezas[rarezaInput] };
            break;
        
        default: // si se entra aquí ha fallado la validacion de la opcion
            WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionObtenerHeroes.ObtenerPorPoder} opciones posibles.");
            return;
    }
    WriteLine(nuevoHeroe);
    var confirmacion5 = ValidarDato(HeroesConfig.MensajeConfirmacion, HeroesConfig.RegexConfirmacion).ToLower();
    if (confirmacion5 == "n") return;
    
    service.UpdateHeroe(id, nuevoHeroe);
    WriteLine("✅  Héroe actualizado.");
}

void BorrarHeroe(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID del Héroe a borrar: ", HeroesConfig.RegexId));
    var borrado = service.DeleteHeroe(id);
    WriteLine($"\n🗑️ Héroe {borrado.Nombre} eliminado correctamente.");
}

void DescansarHeroe(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID del Héroe: ", HeroesConfig.RegexId));
    service.DescansarHeroe(id);
}

void CalcularPoder(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID del Héroe: ", HeroesConfig.RegexId));
    var poder = service.GetPoderHeroe(id);
    WriteLine($"🧨 Poder Cálculado -> {poder}");
}


void CrearMision(EmpresaService service) {
    var nombre = ValidarDato("- Nombre de la Misión -> ", HeroesConfig.RegexNombre);
    var dificultad = int.Parse(ValidarDato("- Dificultad (1-10) -> ", @"^([1-9]|10)$"));
    
    var nuevaMision = new Mision { 
        Nombre = nombre, 
        Dificultad = dificultad, 
        Estado = Mision.Estados.Pendiente 
    };

    WriteLine($"\n📋 Previsualización: {nuevaMision.Nombre} (Dif: {nuevaMision.Dificultad})");
    var confirmacion = ValidarDato(HeroesConfig.MensajeConfirmacion, HeroesConfig.RegexConfirmacion).ToLower();
    if (confirmacion == "n") return;

    var creada = service.SaveMision(nuevaMision);
    WriteLine($"\n✅ Misión {creada.Nombre} creada con éxito. ID: {creada.Id}");
}


void ObtenerMisiones(EmpresaService service) {
    WriteLine("\n--- 📜 LISTADO DE MISIONES ---");
    var misiones = service.GetAllMisionesOrderByDificultad();
    foreach (var m in misiones) {
        WriteLine(m);
    }
}

void BuscarMisionPorId(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID de la Misión: ", HeroesConfig.RegexId));
    var m = service.GetMisionById(id);
    WriteLine($"🔎✅ Misión encontrada:\n-----------------------\n");
    WriteLine(m);
}

void ActualizarMision(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID de la Misión a actualizar: ", HeroesConfig.RegexId));
    var m = service.GetMisionById(id);
    
    var nuevaMision = new Mision {
        Id = m.Id,
        Nombre = m.Nombre,
        Dificultad = m.Dificultad,
        Estado = m.Estado,
        Lineas = m.Lineas 
    };
    
    WriteLine($"Actualizando: {m.Nombre}");
    WriteLine("1: Nombre, 2: Dificultad, 3: Estado (0 para salir)");
    var opcionInput = int.Parse(ValidarDato("- Opción -> ", @"^[0-3]$")); // regex temporal aqui
    var opcion = (OpcionMenuActualizarMision)opcionInput;

    if (opcion == OpcionMenuActualizarMision.Salir) return;

    switch (opcion) {
        case OpcionMenuActualizarMision.Nombre:
            nuevaMision.Nombre = ValidarDato("- Nuevo Nombre -> ", HeroesConfig.RegexNombre);
            break;
        case OpcionMenuActualizarMision.Dificultad:
            nuevaMision.Dificultad = int.Parse(ValidarDato("- Nueva Dificultad -> ", @"^([1-9]|10)$"));
            break;
        case OpcionMenuActualizarMision.Estado:
            WriteLine("0: Pendiente, 1: Completada");
            var st = int.Parse(ValidarDato("- Nuevo Estado -> ", @"^[0-1]$"));
            nuevaMision.Estado = (Mision.Estados)st;
            break;
    }

    service.UpdateMision(id, nuevaMision);
    WriteLine("✅ Misión actualizada con éxito.");
}

void BorrarMision(EmpresaService service) {
    var id = int.Parse(ValidarDato("- ID de la Misión a borrar: ", HeroesConfig.RegexId));
    var borrada = service.DeleteMision(id);
    WriteLine($"\n🗑️ Misión {borrada.Nombre} eliminada correctamente.");
}

void AsignarHeroeAMision(EmpresaService service) {
    WriteLine("\n--- 🛡️ ASIGNAR HÉROE A MISIÓN ---");
    var heroeId = int.Parse(ValidarDato("- ID del Héroe: ", HeroesConfig.RegexId));
    var misionId = int.Parse(ValidarDato("- ID de la Misión: ", HeroesConfig.RegexId));
    
    service.AsignarHeroeAMision(heroeId, misionId);
}

void SimularMision(EmpresaService service) {
    WriteLine("\n--- ⚔️ INICIAR SIMULACIÓN ---");
    var id = int.Parse(ValidarDato("- ID de la Misión a simular: ", HeroesConfig.RegexId));
    service.SimularMision(id);
}


string ValidarDato(string msg, string rgx) {
    string input;
    var regex = new Regex(rgx);
    do {
        Write($"{msg} ");
        input = ReadLine()?.Trim() ?? "-1";
    } while (!regex.IsMatch(input));
    WriteLine();
    return input;
}