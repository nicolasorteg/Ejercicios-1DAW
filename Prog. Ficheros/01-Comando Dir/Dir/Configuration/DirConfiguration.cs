namespace Dir.Configuration;

/// <summary>
/// Clase estática donde se almacena:
/// - La ruta donde se ejecutará el comando.
/// - La ruta del archivo donde se almacenará la información de la salida del comando.
/// </summary>
public static class DirConfiguration {
    public static string DirectoryPath { get; set; } = string.Empty;
    public static string FileOutputPath { get; set; } = string.Empty;
}