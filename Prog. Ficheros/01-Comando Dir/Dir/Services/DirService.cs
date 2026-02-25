using System.Text;
using Dir.Configuration;

namespace Dir.Services;

public static class DirService {
    public static void Run() {
        // valida por si acaso se llega a este punto sin el directory path
        if (string.IsNullOrEmpty(DirConfiguration.DirectoryPath)) {
            WriteLine("No se ha establecido un directorio.");
            return;
        }

        var sb = new StringBuilder();
        var di = new DirectoryInfo(DirConfiguration.DirectoryPath);

        // cabecera parecida a windows
        sb.AppendLine($" Directorio de {di.FullName}\n");
        sb.AppendLine($"{"Mode",-18} {"LastWriteTime",-10} {"Length",10} Name");
        sb.AppendLine(new string('-', 60));

        try {
            // procesamiento directorio
            foreach (var dir in di.GetDirectories()) {
                var mode = "d-----"; 
                var lastWrite = dir.LastWriteTime.ToString("dd/MM/yyyy  HH:mm");
                sb.AppendLine($"{mode,-18} {lastWrite,-10} {"",10} {dir.Name}");
            }

            // procesamiento archivos
            foreach (var file in di.GetFiles()) {
                var mode = "-a----"; 
                var lastWrite = file.LastWriteTime.ToString("dd/MM/yyyy  HH:mm");
                var length = file.Length.ToString();
                sb.AppendLine($"{mode,-18} {lastWrite,-10} {length,10} {file.Name}");
            }
        }
        catch (Exception ex) {
            WriteLine($"Error accediendo al contenido: {ex.Message}");
            return;
        }

        // si hay fichero se imprime en el fichero, si no en la consola
        if (!string.IsNullOrEmpty(DirConfiguration.FileOutputPath)) {
            try {
                File.WriteAllText(DirConfiguration.FileOutputPath, sb.ToString());
                WriteLine($"Salida guardada en: {DirConfiguration.FileOutputPath}");
            }
            catch (Exception ex) {
                WriteLine($"Error escribiendo fichero: {ex.Message}");
            }
        } else {
            WriteLine(sb.ToString());
        }
    }
}
