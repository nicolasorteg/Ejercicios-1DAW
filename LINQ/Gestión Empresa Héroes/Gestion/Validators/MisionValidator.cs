using Gestion.Config;
using Gestion.Models;

namespace Gestion.Validators;

/// <summary>
/// Validador especializado para la entidad Misión.
/// Acumula los errores encontrados.
/// </summary>
public class MisionValidator: IValidator<Mision> {
    public IEnumerable<string> Validar(Mision m) {
        var errores = new List<string>();

        // si no es un héroe
        if (m is not { } heroe) {
            errores.Add("La entidad proporcionada no es una Misión.");
            return errores;
        }
        
        // si el nombre es nulo, es un espacio en blanco o tiene una longitud menor o igual a 1 caracter
        if (string.IsNullOrWhiteSpace(m.Nombre) || m.Nombre.Length < 2) 
            errores.Add("El nombre es obligatorio (mín. 2 car.).");
        
        // si la dificultad no está entre 1-10
        if (m.Dificultad is < HeroesConfig.DificultadMinima or > HeroesConfig.DificultadMaxima)
            errores.Add($"La dificultad debe estar entre {HeroesConfig.DificultadMinima} y {HeroesConfig.DificultadMaxima}");
        
        // si es estado no corresponde con una existente
        if (!Enum.IsDefined(m.Estado)) 
            errores.Add("El estado debe ser Pendiente o Completado");
        
        // verificacion de heroes duplicados en la mision
        var tieneDuplicados = m.Lineas
            .Select(l => l.Heroe.Id)
            .Distinct()
            .Count() != m.Lineas.Count;
        
        if (tieneDuplicados) 
            errores.Add("No puede haber héroes duplicados en la misma misión.");

        // si una mision está completada pero no hay heroes
        if (m.Estado == Mision.Estados.Completada && !m.Lineas.Any()) 
            errores.Add("Una misión completada debe haber tenido al menos un héroe asignado.");
        
        return errores;
    }
}