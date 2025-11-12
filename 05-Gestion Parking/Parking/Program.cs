using System.Text;
using Serilog;
using Parking.Structs;

// config. del logger
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// emojis en terminal
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

// zona de constantes
const int OpcionMenuEntrarParking = 1;
const int OpcionMenuAñadirVehiculo = 2;
const int OpcionMenuVerParking = 3;
const int OpcionMenuInfPlaza = 4;
const int OpcionMenuBusquedaNip = 5;
const int OpcionMenuBusquedaMatticula = 6;
const int OpcionMenuListaMatricula = 7;
const int OpcionMenuListaProfesoresConVehiculo = 8;
const int OpcionMenuActualizarVehiculo = 9;
const int OpcionMenuActualizarProfesor = 10;
const int OpcionMenuBorrarProfesor = 11;
const int OpcionMenuSalir = 12;

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
    RellenarParking(parking, ref aforo);

    Console.WriteLine("--- GESTIÓN PARKING IES LUIS VIVES ---");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is null)
                Console.Write("[⬛]");
            else 
                Console.Write("[🚗]");
        }
        Console.WriteLine();
    }

    
}


void RellenarParking(Vehiculo?[,] parking, ref int aforo) {
    
    Vehiculo v1 = new Vehiculo {matricula = "1234CBC", marca = "Seat", modelo = "Ibiza", profesor = {nip = "AB1", nombre = "JoseLuis", email = "joseluisgs@gmail.com"}};
    Vehiculo v2 = new Vehiculo {matricula = "6382JFF", marca = "Skoda", modelo = "Octavia", profesor = {nip = "ZJ7", nombre = "Carmen", email = "carme123@gmail.com"}};
    Vehiculo v3 = new Vehiculo {matricula = "3729FPL", marca = "Citroen", modelo = "C5", profesor = {nip = "HF7", nombre = "Pepe", email = "pepecito69@gmail.com"}};

    var coches = new Vehiculo[] { v1, v2, v3 };
    
    while (aforo < OcupacionInicial) {
        int filaRandom = random.Next(parking.GetLength(0));
        int columnaRandom = random.Next(parking.GetLength(1));

        if (parking[filaRandom, columnaRandom] is null) {
            parking[filaRandom, columnaRandom] = coches[aforo];
            Log.Information($"✅ Coche {coches[aforo].matricula} asignado a la posición {filaRandom}:{columnaRandom} correctamente.");
            aforo++;
        }
    }
}