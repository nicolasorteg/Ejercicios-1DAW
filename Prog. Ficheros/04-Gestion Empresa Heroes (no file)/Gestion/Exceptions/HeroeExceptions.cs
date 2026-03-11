using Gestion.Config;

namespace Gestion.Exceptions;

/// <summary>
/// Contenedor de excepciones específicas para el programa
/// </summary>
/// <param name="message">Mensaje de error</param>
public class HeroeExceptions(string message) : DomainException(message) {

    /// <summary>
    /// Se lanza cuando no existe el ID ingresado.
    /// </summary>
    /// <param name="id">Identificador único</param>
    public sealed class NotFound(int id) : HeroeExceptions($"❌  No se ha encontrado ningún elemento con ID: {id}");

    /// <summary>
    /// Se lanza cuando existe un error en la validación.
    /// </summary>
    /// <param name="errores">Listado de errores de validación</param>
    public sealed class Validation(IEnumerable<string> errores)
        : HeroeExceptions($"❌ Se han detectado errores de validación en la entidad.") {
        public IEnumerable<string> Errores { get; init; } = errores;
    }

    /// <summary>
    /// Se lanza cuando un ID ya está registrado. No se admiten duplicados de ID.
    /// </summary>
    /// <param name="id">Identificador único</param>
    public sealed class AlreadyExists(int id) : HeroeExceptions($"El ID {id} ya está registrado en el sistema.");

    /// <summary>
    /// Se lanza cuando un héroe supera la Vida Máxima.
    /// </summary>
    public sealed class VidaExcesiva() : HeroeExceptions($"El héroe tiene demasiada energía. El máximo es {HeroesConfig.EnergiaMaxima}");
}