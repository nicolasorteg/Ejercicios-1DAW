using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Funko.Config;
using Funko.Enums;
using Funko.Models;
using Funko.Validators;

namespace Funko.Utils;

public static class Utilities {
    
    public static string ValidarDato(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return input;
    }

    public static void ImprimirMenuPrincipal() {
        Console.WriteLine($"{(int)OpcionMenuPrincipal.VerFunkos}.- Ver Catálogo");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.ObtenerFunkoPorId}.- Buscar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.OrdenarFunkos}.- Ordenar Funkos");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.CrearFunko}.- Crear Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.ActualizarFunko}.- Actualizar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.EliminarFunko}.- Eliminar Funko");
        Console.WriteLine($"{(int)OpcionMenuPrincipal.Salir}.- Salir");
        Console.WriteLine("---------------------");
    }   
    public static void ImprimirMenuOrdenacion() {
        Console.WriteLine("---------------------");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.NombreAsc}.- Nombre ascendente  ⬆️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.NombreDesc}.- Nombre descendente ⬇️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.PrecioAsc}.- Precio ascendente  ⬆️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.PrecioDesc}.- Precio descendente ⬇️");
        Console.WriteLine($"{(int)OpcionMenuOrdenacion.Salir}.- Salir");
        Console.WriteLine("---------------------");
    }

    public static void ImprimirMenuActualizar() {
        Console.WriteLine("---------------------");
        Console.WriteLine($"{(int)OpcionMenuActualizar.Nombre}.- Nombre.");
        Console.WriteLine($"{(int)OpcionMenuActualizar.Categoria}.- Categoria.");
        Console.WriteLine($"{(int)OpcionMenuActualizar.Precio}.- Precio.");
        Console.WriteLine($"{(int)OpcionMenuActualizar.Salir}.- Salir");
        Console.WriteLine("---------------------");
    }

    public static void ImprimirCatalogo(FunkoPop[] catalogo) {
        foreach(var f in catalogo) Console.WriteLine(f);
    }

    public static string PedirNombre() => ValidarDato("- Introduce el Nombre: ", FunkoValidator.RegexNombre);
    public static FunkoPop.Tipo PedirRol() {
        var isRolOk = false;
        FunkoPop.Tipo rol;
        do {
            if (Enum.TryParse(ValidarDato("- Introduce el rol (Superheroe/Anime/Disney): ", FunkoValidator.RegexRol), out rol)) {
                isRolOk = true;
            } else Console.WriteLine("🔴  Rol desconocido. Intente de nuevo.");
        } while (!isRolOk);
        return rol;
    } 
    public static decimal PedirPrecio() {
        var isPrecioOk = false;
        decimal precio;
        do {
            precio = decimal.Parse(ValidarDato("- Introduce el Precio: ", FunkoValidator.RegexPrecio));
            if (precio < Configuracion.PrecioMinimo || precio > Configuracion.PrecioMaximo) {
                Console.WriteLine(
                    $"Precio inválido. Precio mínimo = {Configuracion.PrecioMinimo}  |  Precio máximo = {Configuracion.PrecioMaximo}");
            } else isPrecioOk = true;
        } while (!isPrecioOk);

        return precio;
    }
}