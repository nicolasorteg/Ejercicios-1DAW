using System.Text;
using Serilog;
using Parking.Structs;

// config. del logger
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// emojis en terminal
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

// zona de constantes

const int OcupacionInicial = 3;
const int Size = 10;

var random = new Random();


Main(args);

// para no salir directamente
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main(string[] args) {
    Log.Information("➡️ Iniciando el Main...");

    int aforo = 0;
    // creación del parking
    Vehiculo?[,] parking = new Vehiculo?[2, 5];
    // rellenamos para comenzar con algun dato ejemplificativo
    RellenarParking(parking, ref aforo);

    Console.WriteLine("--- GESTIÓN PARKING IES LUIS VIVES ---");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is null)
                Console.Write("[🔵]");
            else 
                Console.Write("[🚗]");
        }
        Console.WriteLine();
    }

    
}


void RellenarParking(Vehiculo?[,] parking, ref int aforo) {
    
    // creo 3 vehiculos con sus profesores inventados
    Vehiculo v1 = new Vehiculo {matricula = "1234CBC", marca = "Seat", modelo = "Ibiza", profesor = {nip = "AB1", nombre = "JoseLuis", email = "joseluisgs@gmail.com"}};
    Vehiculo v2 = new Vehiculo {matricula = "6382JFF", marca = "Skoda", modelo = "Octavia", profesor = {nip = "ZJ7", nombre = "Carmen", email = "carme123@gmail.com"}};
    Vehiculo v3 = new Vehiculo {matricula = "3729FPL", marca = "Citroen", modelo = "C5", profesor = {nip = "HF7", nombre = "Pepe", email = "pepecito69@gmail.com"}};

    // array de vehiculos para asignarlos a una pos
    var coches = new Vehiculo[] { v1, v2, v3 };
    
    // mientras que el aforo sea inferior al aforo inicial que se nos pide mete vehiculos (solo tenemos 3)
    while (aforo < OcupacionInicial) {
        // generamos posiciones aleatorias
        // getlength mostrará la medida, por ejemplo 5 en el caso de las columnas, pero el 5 no entra asi que 
        // generara de 0-4, lo mismo con las filas
        int filaRandom = random.Next(parking.GetLength(0));
        int columnaRandom = random.Next(parking.GetLength(1));

        // si esta vacia se mete el coche extraido del array de coches y se incrementa el aforo actual
        if (parking[filaRandom, columnaRandom] is null) {
            parking[filaRandom, columnaRandom] = coches[aforo];
            Log.Information($"✅  Coche {coches[aforo].matricula} asignado a la posición {filaRandom}:{columnaRandom} correctamente.");
            aforo++;
        }
    }
}