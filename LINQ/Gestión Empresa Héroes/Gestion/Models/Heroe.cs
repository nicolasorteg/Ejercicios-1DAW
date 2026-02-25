using Gestion.Config;

namespace Gestion.Models;

/// <summary>
/// Representa a un Héroe dentro de Sistema.
/// Al ser abstracta no puede existir ningún objeto que sea solo Heroe.
/// </summary>
public abstract record Heroe: IHeroe {
    public int Id { get; init; }
    public required string Nombre { get; set; }
    public required int Nivel { get; set; }
    public required int Energia { get; set; }
    public required int Experiencia { get; set; }
    public required Rarezas Rareza { get; set; }
    public enum Rarezas { Común = 100, Especial = 200, Raro = 300, Épico = 400, Legendario = 500}


    public void Descansar() {
        if (Energia >= HeroesConfig.EnergiaMaxima) {
            Energia = HeroesConfig.EnergiaMaxima;
            WriteLine($"🦸 {Nombre} ya está a tope.");
            return;
        }
        Energia = Math.Min(HeroesConfig.EnergiaMaxima, Energia + HeroesConfig.EnergiaRestauraDescansar);
        WriteLine($"🦸 {Nombre} ha descansado bien. +{HeroesConfig.EnergiaRestauraDescansar} Energía.");
    }

    public abstract int CalcularPoder();
    
    public void SumarExperiencia(int xp) {
        
        while (xp > 0) {
            if (Nivel >= HeroesConfig.UmbralesNivel.Length - 1) {
                WriteLine($"🛑 {Nombre} ya es el nivel máximo.");
                break;
            }
            
            var objetivo = HeroesConfig.UmbralesNivel[Nivel];
            var faltaParaSubir = objetivo - Experiencia;

            if (xp >= faltaParaSubir) {
                xp -= faltaParaSubir;
                Experiencia = 0;
                Nivel++;
                WriteLine($"⭐ ¡SUBIDA DE NIVEL! {Nombre} ahora es Nivel {Nivel}.");
            } else {
                Experiencia += xp;
                xp = 0;
            }
        } 
    }

    public override string ToString() => 
        $"{Id:D2} |  Nombre: {Nombre}  |  Nivel: {Nivel}  |  Energía: {Energia}  |  Experiencia: {Experiencia}  | Rareza: {Rareza}";
}