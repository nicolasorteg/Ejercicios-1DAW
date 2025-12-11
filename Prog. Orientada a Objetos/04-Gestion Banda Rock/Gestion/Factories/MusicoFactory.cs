using Gestion.Models;

public static class MusicoFactory {

    public static Musico[] DemoMiembros() {
        return [
            new Baterista { Id = 1, Nombre = $"Lars Ulrich", TiempoEnBanda = 15},
            new Bajista { Id = 2, Nombre = "Robert Trujillo", TiempoEnBanda = 10},
            new Guitarrista { Id = 3, Nombre = "Kirk Hammett", TiempoEnBanda = 15},
            new Guitarrista { Id = 4, Nombre = "Dave Mustaine", TiempoEnBanda = 15},
            new Cantante() { Id = 5, Nombre = "James Hetfield", TiempoEnBanda = 16},
        ];
    }
}