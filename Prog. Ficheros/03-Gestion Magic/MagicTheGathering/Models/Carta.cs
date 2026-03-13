namespace MagicTheGathering.Models;

/// <summary>
/// Clase padre que almacena datos genéricos de la Carta
/// </summary>
public abstract record Carta {

    public required string Nombre { get; set; }
    public required List<IdentidadesDeColor> IdentidadColor { get; set; }
    public required Supertipos Supertipo { get; set; }
    public required bool IsProhibida { get; set; }
    
    public enum IdentidadesDeColor { Incoloro, Negro, Blanco, Rojo, Azul, Verde }
    
    public enum Supertipos { Ninguno, Basico, Legendario}
    
    public override string ToString() =>
        $"Nombre: {Nombre,-10} | Tipo: {GetType().Name} | Colores: {string.Join(", ", IdentidadColor),-20} | Supertipo: {Supertipo,-8} | Permitida en Mazos: {(IsProhibida ? "No" : "Sí")}";
}