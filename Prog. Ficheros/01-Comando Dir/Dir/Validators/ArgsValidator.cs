namespace Dir.Validators;

/// <summary>
/// Validador para los datos introducidos por argumento.
/// </summary>
public class ArgsValidator {
    public IEnumerable<string> Validar(string[] args) {
        var errores = new List<string>();

        // valida que al menos exista un argumento
        if (args.Length == 0) {
            errores.Add("No se han proporcionado argumentos. Debe indicar al menos una ruta de directorio.");
            return errores;
        }

        // valida que la ruta de origen sea válida y exista
        if (string.IsNullOrWhiteSpace(args[0])) 
            errores.Add("La ruta del directorio no puede estar vacía.");
        else if (!Directory.Exists(args[0])) 
            errores.Add($"El directorio origen '{args[0]}' no existe o no es accesible.");
        
        // valida la ruta de salida si se pasa
        if (args.Length > 1) {
            if (string.IsNullOrWhiteSpace(args[1])) 
                errores.Add("El nombre del archivo de salida no puede estar vacío si se proporciona el argumento.");
        }
        return errores;
    }
}