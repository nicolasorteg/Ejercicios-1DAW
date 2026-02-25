namespace Gestion.Exceptions;

/// <summary>
/// Clase abstracta para todas las excepciones de la Empresa.
/// Abstracta para funcionar como contrato para cualquier error.
/// </summary>
/// <param name="msg">Mensaje de error</param>
public abstract class DomainException(string msg) : Exception(msg);