using Gestion.Models;

namespace Gestion.Factories;

public static class MisionFactory {
    /// <summary>
    /// Genera misiones iniciales de prueba para el sistema.
    /// </summary>
    /// <returns>Lista de misiones</returns>
    public static IEnumerable<Mision> DemoMisiones() {
        return new List<Mision> {
            new Mision { Id = 1, Nombre = "Infiltración en la base Hydra", Dificultad = 3, Estado = Mision.Estados.Pendiente },
            new Mision { Id = 2, Nombre = "Defensa de la Torre Stark", Dificultad = 7, Estado = Mision.Estados.Pendiente },
            new Mision { Id = 3, Nombre = "Recuperación del Guantelete", Dificultad = 10, Estado = Mision.Estados.Pendiente },
            new Mision { Id = 4, Nombre = "Patrulla Vecinal", Dificultad = 1, Estado = Mision.Estados.Pendiente }
        };
    }
}