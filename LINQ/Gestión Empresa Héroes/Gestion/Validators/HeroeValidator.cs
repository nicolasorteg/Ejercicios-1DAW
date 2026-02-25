using Gestion.Config;
using Gestion.Models;

namespace Gestion.Validators;

/// <summary>
/// Validador especializado para la entidad Heroe.
/// Acumula los errores encontrados.
/// </summary>
public class HeroeValidator: IValidator<Heroe> {
    public IEnumerable<string> Validar(Heroe h) {
        var errores = new List<string>();

        // si no es un héroe
        if (h is not { } heroe) {
            errores.Add("La entidad proporcionada no es un Héroe.");
            return errores;
        }
        
        // si el nombre es nulo, es un espacio en blanco o tiene una longitud menor o igual a 1 caracter
        if (string.IsNullOrWhiteSpace(h.Nombre) || h.Nombre.Length < 2) 
            errores.Add("El nombre es obligatorio (mín. 2 car.).");

        // si el nivel es negativo o mayor a 10
        if (int.IsNegative(h.Nivel) || h.Nivel > HeroesConfig.NivelMaximo )
            errores.Add($"El nivel es obligatorio y no puede ser mayor de {HeroesConfig.NivelMaximo}.");
        
        // si la energia es negativa o mayor a 200
        if (int.IsNegative(h.Energia) || h.Energia > HeroesConfig.EnergiaMaxima )
            errores.Add($"La energía es obligatoria y no puede ser mayor de {HeroesConfig.EnergiaMaxima}.");
        
        // si la experiencia es negativa o no corresponde al umbral de subida correspondiente al nivel
        if (int.IsNegative(h.Experiencia)) {
            errores.Add("La experiencia no puede ser un valor negativo.");
        }
        else if (h.Nivel < HeroesConfig.UmbralesNivel.Length - 1) {
            // si la experiencia es mayor o igual al umbral
            if (h.Experiencia >= HeroesConfig.UmbralesNivel[h.Nivel]) {
                errores.Add($"La experiencia para el nivel {h.Nivel} debe ser menor a {HeroesConfig.UmbralesNivel[h.Nivel]} (umbral de subida).");
            }
        }

        // si la rareza no corresponde con una existente
        if (!Enum.IsDefined(h.Rareza)) {
            errores.Add("La rareza debe ser una de las 5 posibles. (Común, Especial, Raro, Épico y Legendario)");
        }

        // validacion de cada uno de los campos de cada tipo de Heroe
        switch (h) {
            case HeroeAgil agil:
                if (agil.Agilidad is < HeroesConfig.AgilidadMinima or > HeroesConfig.AgilidadMaxima)
                    errores.Add($"La Agilidad debe estar entre {HeroesConfig.AgilidadMinima} y {HeroesConfig.AgilidadMaxima}.");
                break;
            
            case HeroeFuerte fuerte:
                if (fuerte.Fuerza is < HeroesConfig.FuerzaMinima or > HeroesConfig.FuerzaMaxima)
                    errores.Add($"La Fuerza debe estar entre {HeroesConfig.FuerzaMinima} y {HeroesConfig.FuerzaMaxima}.");
                break;
            
            case HeroeInteligente inteligente:
                if (!Enum.IsDefined(inteligente.Inteligencia))
                    errores.Add("El nivel de inteligencia no es válido.");
                break;
        }
        return errores;
    }
}