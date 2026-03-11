using Gestion.Models;

namespace Gestion.Factories;

public static class HeroeFactory {
    
    /// <summary>
    /// Genera los datos iniciales con 20 héroes de distintas clases.
    /// </summary>
    /// <returns>Heroe iniciales</returns>
    public static IEnumerable<Heroe> DemoHeroes() {
        var lista = new List<Heroe> {
            
            // fuertes
            new HeroeFuerte { Id = 1, Nombre = "Thorin", Nivel = 1, Energia = 100, Experiencia = 0, Rareza = Heroe.Rarezas.Común, Fuerza = 40 },
            new HeroeFuerte { Id = 2, Nombre = "Grog", Nivel = 3, Energia = 120, Experiencia = 20, Rareza = Heroe.Rarezas.Especial, Fuerza = 45 },
            new HeroeFuerte { Id = 3, Nombre = "Bárbol", Nivel = 5, Energia = 150, Experiencia = 50, Rareza = Heroe.Rarezas.Raro, Fuerza = 48 },
            new HeroeFuerte { Id = 4, Nombre = "Conan", Nivel = 2, Energia = 110, Experiencia = 10, Rareza = Heroe.Rarezas.Común, Fuerza = 35 },
            new HeroeFuerte { Id = 5, Nombre = "Brienne", Nivel = 4, Energia = 130, Experiencia = 40, Rareza = Heroe.Rarezas.Épico, Fuerza = 38 },
            new HeroeFuerte { Id = 6, Nombre = "Hércules", Nivel = 8, Energia = 180, Experiencia = 100, Rareza = Heroe.Rarezas.Legendario, Fuerza = 50 },
            new HeroeFuerte { Id = 7, Nombre = "Xena", Nivel = 4, Energia = 125, Experiencia = 35, Rareza = Heroe.Rarezas.Raro, Fuerza = 50 },
            
            // ágiles
            new HeroeAgil { Id = 8, Nombre = "Legolas", Nivel = 5, Energia = 90, Experiencia = 45, Rareza = Heroe.Rarezas.Épico, Agilidad = 46 },
            new HeroeAgil { Id = 9, Nombre = "Arya", Nivel = 3, Energia = 85, Experiencia = 25, Rareza = Heroe.Rarezas.Raro, Agilidad = 44 },
            new HeroeAgil { Id = 10, Nombre = "Ezzio", Nivel = 6, Energia = 95, Experiencia = 60, Rareza = Heroe.Rarezas.Legendario, Agilidad = 45 },
            new HeroeAgil { Id = 11, Nombre = "Katniss", Nivel = 2, Energia = 80, Experiencia = 15, Rareza = Heroe.Rarezas.Especial, Agilidad = 35 },
            new HeroeAgil { Id = 12, Nombre = "Drizzt", Nivel = 7, Energia = 100, Experiencia = 85, Rareza = Heroe.Rarezas.Legendario, Agilidad = 50 },
            new HeroeAgil { Id = 13, Nombre = "Robin", Nivel = 3, Energia = 88, Experiencia = 20, Rareza = Heroe.Rarezas.Común, Agilidad = 30 },
            new HeroeAgil { Id = 14, Nombre = "Fiora", Nivel = 4, Energia = 92, Experiencia = 38, Rareza = Heroe.Rarezas.Raro, Agilidad = 41 },
            
            // inteligentes
            new HeroeInteligente { Id = 15, Nombre = "Gandalf", Nivel = 10, Energia = 200, Experiencia = 0, Rareza = Heroe.Rarezas.Legendario, Inteligencia = HeroeInteligente.Inteligencias.Alta },
            new HeroeInteligente { Id = 16, Nombre = "Raistlin", Nivel = 4, Energia = 70, Experiencia = 40, Rareza = Heroe.Rarezas.Épico, Inteligencia = HeroeInteligente.Inteligencias.Alta },
            new HeroeInteligente { Id = 17, Nombre = "Morgana", Nivel = 3, Energia = 100, Experiencia = 20, Rareza = Heroe.Rarezas.Raro, Inteligencia = HeroeInteligente.Inteligencias.Media },
            new HeroeInteligente { Id = 18, Nombre = "Merlín", Nivel = 6, Energia = 150, Experiencia = 70, Rareza = Heroe.Rarezas.Legendario, Inteligencia = HeroeInteligente.Inteligencias.Alta },
            new HeroeInteligente { Id = 19, Nombre = "Hermione", Nivel = 2, Energia = 90, Experiencia = 12, Rareza = Heroe.Rarezas.Especial, Inteligencia = HeroeInteligente.Inteligencias.Media },
            new HeroeInteligente { Id = 20, Nombre = "Jaskier", Nivel = 1, Energia = 80, Experiencia = 0, Rareza = Heroe.Rarezas.Común, Inteligencia = HeroeInteligente.Inteligencias.Baja }
        };
        return lista;
    }
}