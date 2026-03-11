using Gestion.Config;

namespace Gestion.Models;

/// <summary>
/// Representa a una Misión dentro del Sistema.
/// En vez de lista de heroes tiene lista de LineaMision (esta almacena al heroe).
/// </summary>
public class Mision {
    public int Id { get; init; }
    public required string Nombre { get; set; }
    public int Dificultad { get; set; } 
    public Estados Estado { get; set; } 
    public List<LineaMision> Lineas { get; set; } = [];

    public enum Estados { Pendiente, Completada }
    
    public void AñadirHeroe(Heroe h) {
        
        if (Lineas.Any(l => l.Heroe.Id == h.Id)) {
            WriteLine($"⚠️ {h.Nombre} ya está asignado a esta misión.");
            return;
        }

        // validacion poca vida
        if (h.Energia < HeroesConfig.EnergiaMinima) {
            WriteLine($"😴 {h.Nombre} no tiene suficiente energía para esta misión.");
            return;
        }

        Lineas.Add(new LineaMision { Heroe = h });
        WriteLine($"✅ {h.Nombre} se ha unido a la misión {Nombre}.");
    }
    
    public override string ToString() {
        var heroesAsignados = Lineas.Count;
        return $"{Id:D2} | Misión: {Nombre} | Dificultad: {Dificultad} | Estado: {Estado} | Equipo: {heroesAsignados} héroes";
    }
}