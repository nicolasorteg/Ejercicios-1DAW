using System.Text;
using System.Text.RegularExpressions;

// ---------- inicio del main ----------

Console.OutputEncoding = Encoding.UTF8; // emojis por terminal

string palabra;
Console.WriteLine("------ 😵 JUEGO DEL AHORCADO 😵 ------");
Console.WriteLine("Pulse cualquier tecla para comenzar la partida...");
Console.ReadKey();

Console.WriteLine("- Comienzo de partida -");
SeleccionarPalabra(out palabra);
var tableroLetras = new char[palabra.Length];
var tableroAciertos = new bool[tableroLetras.Length];
ConfigurarPartida(tableroLetras, palabra);
ComenzarPartida(palabra, tableroLetras, tableroAciertos);

return;
// ---------- fin del main ----------

void SeleccionarPalabra(out string palabra) {

    // se crean las posibles palabras, modificable en el tiempo
    var palabras = new string[] { "computadora", "programacion", "desarrollo", "teclado", "pantalla", "internet", "javascript", "montaña", "carretera", "bicicleta", "aventura", "planeta", "astronauta", "musica", "pelicula", "cangrejo", "elefante", "biblioteca", "cereza", "fantasma" };

    var random = new Random();
    var indicePalabra = random.Next(palabras.Length); // de 0 a la longitud del vector excluyendo el último numero, en este caso 0-19 incluyendo el 19
    palabra = palabras[indicePalabra]; // se le asigna la palabra cogida con un índice random
}

void ConfigurarPartida(char[] tableroLetras, string palabra) {
    for (int i = 0; i < tableroLetras.Length; i++) {
        tableroLetras[i] = palabra[i];
    }
}

void ComenzarPartida(string palabra, char[] letras, bool[] aciertos) {

    int intentos = letras.Length + 5;
    bool isPalabraAcertada = false;
    char letraElegida;
    
    do {
        Console.WriteLine($"Intentos restantes = {intentos}");
        ImprimirTablero(letras, aciertos);
        letraElegida = LeerLetra("- Letra a comprobar: ");
        bool isLetraContenida = ComprobarLetra(letraElegida, letras, aciertos);
        
        if (isLetraContenida) isPalabraAcertada = ComprobarAciertosPalabra(aciertos);

        intentos--;
    } while (intentos > 0 && !isPalabraAcertada);
    
    Console.WriteLine($"La palabra era {palabra}");
}
void ImprimirTablero(char[] letras, bool[] aciertos) {

    for (int i = 0; i < letras.Length; i++) {
        
        if (aciertos[i]) {
            Console.Write($"{letras[i]} ");
        } else {
            Console.Write("_ ");
        }
    }
    Console.WriteLine();
}

char LeerLetra(string message) {

    var letra = char.MinValue;
    bool isLetraValid = false;
    var regexLetra = new Regex(@"^[a-z]$");

    do {
        Console.WriteLine(message);
        var input = Console.ReadLine()?.Trim() ?? "";

        if (regexLetra.IsMatch(input)) {
            letra = char.Parse(input);
            isLetraValid = true;
        } else {
            Console.WriteLine("❌ Letra no válida.");
        }
    } while (!isLetraValid);

    return letra;
}

bool ComprobarLetra(char letra, char[] letras, bool[] aciertos) {

    bool isLetraContenida = false;
    for (int i = 0; i < letras.Length; i++) {
        if (letras[i] == letra) {
            aciertos[i] = true;
            isLetraContenida = true;
        }
    }

    if (isLetraContenida) {
        Console.WriteLine("✅ Letra contenida en la palabra.");
    } else {
        Console.WriteLine("😿 Letra no contenida en la palabra.");
    }

    return isLetraContenida;
}


bool ComprobarAciertosPalabra(bool[] aciertos) {

    for (int i = 0; i < aciertos.Length; i++) {
        if (aciertos[i] == false) return false;
    }
    
    Console.WriteLine("Enhorabuena has ganado!!");
    return true;
}