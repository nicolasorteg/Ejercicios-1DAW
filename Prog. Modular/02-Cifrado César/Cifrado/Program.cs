using System.Text;

const string AlfabetoCesar = " abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.,;:¿?¡!()[]{}@#$%€&/\\\"'çÇáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙâêîôûÂÊÎÔÛäëïöüÄËÏÖÜãõÃÕ";
// ---------- inicio del main ----------

Console.OutputEncoding = Encoding.UTF8; // emojis en terminal

Console.WriteLine("--------------------");
Console.WriteLine("🔁 CIFRADO CÉSAR 🔁");
Console.WriteLine("--------------------");

var rotacion = ValidarDatosArgumento();
Console.WriteLine($"✅ Rotación = {rotacion}");
var rotacionEfectiva = AlfabetoCesar.Length % rotacion;
Console.WriteLine($"✅ Rotación efectiva = {rotacionEfectiva}");

Console.WriteLine("Introduce el mensaje a cifrar: ");
var mensaje = Console.ReadLine() ?? "";
CifrarMensaje(rotacion, mensaje);

return;

// ------------ fin del main -----------

void CifrarMensaje(int rotacion, string mensaje) {
    var sb = new StringBuilder();
    foreach (var letra in mensaje) {
        var posicion = AlfabetoCesar.IndexOf(letra);
        if (posicion == -1) {
            sb.Append(letra);
        } else {
            var nuevaPosicion = posicion + rotacion;
            if (nuevaPosicion < 0) {
                sb.Append(AlfabetoCesar[AlfabetoCesar.Length + nuevaPosicion]);
            } else {
                sb.Append(AlfabetoCesar[nuevaPosicion % AlfabetoCesar.Length]);
            }
        }
    }
    Console.WriteLine(sb);
}

int ValidarDatosArgumento() {
    
    int rotacion;
    if (args.Length != 1) {
        Console.WriteLine("❌ Entrada de datos por argumento fallida.");
    } else {
        var datos = args[0].Split(":");
        if (datos.Length is 2 && int.TryParse(datos[1], out rotacion)) {
            rotacion = rotacion % AlfabetoCesar.Length;
            return rotacion;
        } 
        Console.WriteLine("❌ Entrada de datos por argumento fallida.");
    }
    return PedirRotacion("Nº de rotaciones para el cifrar el mensaje: ");
}

int PedirRotacion(string msg) {
    int rotacion;
    bool isRotacionOk = false;
    do {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim() ?? "";

        if (int.TryParse(input, out rotacion)) {
            isRotacionOk = true;
        } else {
            Console.WriteLine("❌ Rotacion inválida. Introduzca un valor entero.");
        }
    } while (!isRotacionOk);
    
    return rotacion;
}