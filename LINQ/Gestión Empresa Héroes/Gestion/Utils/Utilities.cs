using Gestion.Enums;

namespace Gestion.Utils;

public abstract class Utilities {
    public static void ImprimirMenu() {
        WriteLine("\n----- MENÚ PRINCIPAL -----");
        WriteLine($"{(int)OpcionesMenuPrincipal.CrearHeroe}.- Crear Héroe.");
        WriteLine($"{(int)OpcionesMenuPrincipal.ObtenerHeroes}.- Obtener Héroes.");
        WriteLine($"{(int)OpcionesMenuPrincipal.BuscarHeroePorId}.- Buscar Héroe por ID.");
        WriteLine($"{(int)OpcionesMenuPrincipal.ActualizarHeroe}.- Actualizar Héroe.");
        WriteLine($"{(int)OpcionesMenuPrincipal.BorrarHeroe}.- Borrar Héroe.");
        WriteLine("--------------------------");
        WriteLine($"{(int)OpcionesMenuPrincipal.CrearMision}.- Crear Misión.");
        WriteLine($"{(int)OpcionesMenuPrincipal.ObtenerMision}.- Obtener Misiones.");
        WriteLine($"{(int)OpcionesMenuPrincipal.BuscarMisionPorId}.- Buscar  por ID.");
        WriteLine($"{(int)OpcionesMenuPrincipal.ActualizarMision}.- Actualizar Misión.");
        WriteLine($"{(int)OpcionesMenuPrincipal.BorrarMision}.- Borrar Misión.");
        WriteLine("--------------------------");
        WriteLine($"{(int)OpcionesMenuPrincipal.SimularMision}.- Simular Misión.");
        WriteLine($"{(int)OpcionesMenuPrincipal.DescansarHeroe}.- Descansar Héroe.");
        WriteLine($"{(int)OpcionesMenuPrincipal.CalcularPoderHeroe}.- Calcular Poder Héroe.");
        WriteLine($"{(int)OpcionesMenuPrincipal.AsignarHeroeAMision}.- Asignar Héroe a Misión.");
        WriteLine($"{(int)OpcionesMenuPrincipal.Salir}.- Salir");
    }

    public static void ImprimirMenuObtenerHeroes() {
        WriteLine("----- TIPO DE ORDENACIÓN -----");
        WriteLine($"{(int)OpcionObtenerHeroes.ObtenerPorId}.- Obtener Todos.");
        WriteLine($"{(int)OpcionObtenerHeroes.ObtenerPorNivel}.- Obtener Todos ordenador por Nivel.");
        WriteLine($"{(int)OpcionObtenerHeroes.ObtenerPorPoder}.- Obtener Todos ordenador por Poder.");
        WriteLine($"{(int)OpcionObtenerHeroes.Salir}.- Salir");
    }
    
    public static void ImprimirMenuActualizarHeroes() {
        WriteLine("----- DATO A ACTUALIZAR -----");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Nombre}.- Nombre.");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Nivel}.- Nivel.");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Energia}.- Energia.");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Experiencia}.- Experiencia");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Rareza}.- Rareza");
        WriteLine($"{(int)OpcionMenuActualizarHeroe.Salir}.- Salir");
    }
}