using Gestion.Config;
using Gestion.Enum;
using Gestion.Models;
using Gestion.Validators;

namespace Gestion.Utils;

public static class Utilities {

    public static void ImrimirMenuPrincipal() {
        Console.WriteLine("\n--- 🎶 MENU PRINCIPAL 🎶 ---");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerBanda}.-  Ver Integrantes.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerPorId}.-  Ver por Id.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerGuitarristas}.-  Ver posibles Guitarristas.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerSlapBase}.-  Ver Bajistas.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Crear}.-  Crear Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Actualizar}.-  Actualizar Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Borrar}.-  Borrar Músico.");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Salir}.-  Salir.");
    }

    public static void ImprimirMusicos(Musico[] musicos) {
        foreach(var m in musicos) Console.WriteLine(m);
    }
    public static void ImprimirGuitarristas(ICantanteGuitarrista[] guitarristas) {
        foreach(var g in guitarristas) Console.WriteLine(g);
    }
    public static void ImprimirBajistas(Bajista[] bajistas) {
        foreach(var b in bajistas) Console.WriteLine(b);
    }

    public static string PedirNombre() => BandaValidator.ValidarDato("- Introduce el Nombre: ", BandaValidator.RegexNombre);

    public static int PedirTiempo() {
        var isTiempoOk = false;
        int tiempo;
        do {
            tiempo = int.Parse(BandaValidator.ValidarDato("- Introduce el Tiempo: ", BandaValidator.RegexTiempoEnBanda));
            if (tiempo < Configuration.AñosMinimos || tiempo > Configuration.AñosMaximos) {
                Console.WriteLine(
                    $"Precio inválido. Precio mínimo = {Configuration.AñosMinimos}  |  Precio máximo = {Configuration.AñosMaximos}");
            } else isTiempoOk = true;
        } while (!isTiempoOk);

        return tiempo;
    }

    public static object PedirRol() {
        throw new NotImplementedException();
    }
}