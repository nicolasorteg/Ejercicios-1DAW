namespace Gestion.Models;
/*
 * Almacena los datos de las personas a gestionar.
 * Se usa record, ya que Persona solo almacena los datos
 */
public record Persona {
    public enum TipoPersona {
        Profesor,
        Alumno
    }
    public required string Dni { get; init; }
    public required string Nombre { get; set; }
    public required int Edad { get; set; }
    public required TipoPersona Tipo { get; set; }
    public required DatosAcademicos? Datos { get; set; }

    public override string ToString() {
        var resultado = $"Dni: {this.Dni}  |  Nombre: {this.Nombre}  |  Edad: {this.Edad}  |  Rol:  {this.Tipo}";
        if (this.Tipo == TipoPersona.Alumno) resultado += $"  |  Faltas: {this.Datos?.Faltas}  |  Retrasos: {this.Datos?.Retrasos}";
        return resultado;
    }
}