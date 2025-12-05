using Funko.Models;

namespace Funko.Factories;

public static class FunkosFactory {
    public static FunkoPop[] DemoFunkos() {
        return [
            new FunkoPop { Id = 1, Nombre = "Iron Man", Categoria = FunkoPop.Tipo.Superherore, Precio = 25.50m },
            new FunkoPop { Id = 2, Nombre = "Spider-Man (Clásico)", Categoria = FunkoPop.Tipo.Superherore, Precio = 30.00m },
            new FunkoPop { Id = 3, Nombre = "Goku Super Saiyan", Categoria = FunkoPop.Tipo.Anime, Precio = 18.99m },
            new FunkoPop { Id = 4, Nombre = "Pikachu", Categoria = FunkoPop.Tipo.Anime, Precio = 22.00m },
            new FunkoPop { Id = 5, Nombre = "Mickey Mouse", Categoria = FunkoPop.Tipo.Disney, Precio = 15.00m },
            new FunkoPop { Id = 6, Nombre = "Elsa", Categoria = FunkoPop.Tipo.Disney, Precio = 16.50m },
            new FunkoPop { Id = 7, Nombre = "Capitán América", Categoria = FunkoPop.Tipo.Superherore, Precio = 28.75m },
            new FunkoPop { Id = 8, Nombre = "Vegeta (Scouter)", Categoria = FunkoPop.Tipo.Anime, Precio = 45.00m },
            new FunkoPop { Id = 9, Nombre = "Stitch", Categoria = FunkoPop.Tipo.Disney, Precio = 19.95m },
            new FunkoPop { Id = 10, Nombre = "Black Widow", Categoria = FunkoPop.Tipo.Superherore, Precio = 17.00m },
            new FunkoPop { Id = 11, Nombre = "Luffy Gear 4", Categoria = FunkoPop.Tipo.Anime, Precio = 55.00m },
            new FunkoPop { Id = 12, Nombre = "Buzz Lightyear", Categoria = FunkoPop.Tipo.Disney, Precio = 21.50m },
            new FunkoPop { Id = 13, Nombre = "Thor (Ragnarok)", Categoria = FunkoPop.Tipo.Superherore, Precio = 23.00m },
            new FunkoPop { Id = 14, Nombre = "Sailor Moon", Categoria = FunkoPop.Tipo.Anime, Precio = 35.00m },
            new FunkoPop { Id = 15, Nombre = "Darth Vader (Chrome)", Categoria = FunkoPop.Tipo.Disney, Precio = 65.00m }
        ];
    }
}