namespace Dir.Configuration;

/// <summary>
/// Clase estática donde se almacena:
/// - La ruta donde se ejecutará el comando.
/// - La ruta del archivo donde se almacenará la información de la salida del comando.
/// </summary>
public static class DirConfiguration {
    public static string DirectoryPath { get; set; } = string.Empty;
    public static string FileOutputPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Método para almacenar los datos pasados por argumento en la configuración.
    /// </summary>
    /// <param name="strings"></param>
    public static void MapearArgumentosAConfig(string[] strings) {
        DirectoryPath = Path.GetFullPath(strings[0]);
        if (strings.Length > 1) {
            FileOutputPath = strings[1];
        }
    }
}