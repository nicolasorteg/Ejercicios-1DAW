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

const int Size = 10;

const int Filas = 2;
const int Columnas = 5;


Main(args);

// para no salir directamente
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main(string[] args) {
    Log.Information("➡️ Iniciando el Main...");

    // creación del parking
    Vehiculo[,] parking = new Vehiculo[2, 5];
    
    Console.WriteLine("--- GESTIÓN PARKING IES LUIS VIVES ---");
    
}
