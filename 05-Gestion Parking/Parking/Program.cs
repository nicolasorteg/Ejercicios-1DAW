using System.ComponentModel.Design;
using System.Text;
using Parking.Enums;
using Serilog;
using Parking.Structs;

// zona de constantes

const int OcupacionInicial = 3;
const int Size = 10;

var random = new Random();


// config. del logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();

// titulo de la terminal 
Console.Title = "Gestión Parking IES Luis Vives";
// emojis en terminal
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(args);

// para no salir directamente
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;




void Main(string[] args) {
    Log.Debug("➡️ Iniciando el Main...");

    int numVehiculos = 0;
    // creación del parking
    Vehiculo?[,] parking = new Vehiculo?[2, 5];
    // rellenamos para comenzar con algun dato ejemplificativo
    RellenarParking(parking, ref numVehiculos);
    int opcionElegida = 0;
    do {
        ImprimirMenu();
        opcionElegida = ValidarOpcion("Introduzca una opción:");

        Log.Debug($"🔵 Asignando acción para la opción {opcionElegida}");
        switch (opcionElegida) {
            case (int)MenuOpcion.EntrarParking: // 1
                //SimularBarreraEntrada(parking, numVehiculos);
                break;
            case (int)MenuOpcion.AñadirVehiculo: // 2
                //AñadirVehiculo(parking, numVehiculos);
                break;
            case (int)MenuOpcion.VerParking: // 3
                //MostrarParking(parking);
                break;
            case (int)MenuOpcion.InfPlaza: // 4
                //LeerInformacionPlaza(parking);
                break;
            case (int)MenuOpcion.BusquedaNip: // 5
                //BuscarPorNip(parking);
                break;
            case (int)MenuOpcion.BusquedaMatricula: // 6
                //BuscarPorMatricula(parking);
                break;
            case (int)MenuOpcion.ListaMatricula: // 7
                //MostrarPorMatriculaAsc(parking);
                break;
            case (int)MenuOpcion.ListaProfesoresConVehiculo: // 8
                //MostrarProfesores(parking);
                break;
            case (int)MenuOpcion.ActualizarVehiculo: // 9
                //ActualizarVehiculo(parking);
                break;
            case (int)MenuOpcion.ActualizarProfesor: // 10
                //ActualizarProfesor(parking);
                break;
            case (int)MenuOpcion.BorrarProfesor: // 11
                //BorrarProfesor(parking);
                break;
            case (int)MenuOpcion.BorrarVehiculo: // 12
                //BorrarVehiculo(parking);
                break;
            case (int)MenuOpcion.Salir: // 13
                Console.WriteLine("😊 Ha sido un placer...");
                break;
            default: // no deberia poder llegar aqui ya que 'ValidarOpcion' solo puede devolver un numero valido
                Console.WriteLine("🔴 Error en la validación de datos.");
                break;
        }
    } while (opcionElegida != (int)MenuOpcion.Salir);
    
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

void ImprimirMenu() {
    Console.WriteLine("----------- Menú Parking -----------");
    Console.WriteLine($"{(int)MenuOpcion.EntrarParking}.- Entrar al Parking.");
    Console.WriteLine($"{(int)MenuOpcion.AñadirVehiculo}.- Añadir vehiculo manualmente.");
    Console.WriteLine($"{(int)MenuOpcion.VerParking}.- Ver el Parking.");
    Console.WriteLine($"{(int)MenuOpcion.InfPlaza}.- Ver información de una plaza.");
    Console.WriteLine($"{(int)MenuOpcion.BusquedaNip}.- Buscar por NIP");
    Console.WriteLine($"{(int)MenuOpcion.BusquedaMatricula}.- Buscar por matrícula.");
    Console.WriteLine($"{(int)MenuOpcion.ListaMatricula}.- Listado de coches por matrícula.");
    Console.WriteLine($"{(int)MenuOpcion.ListaProfesoresConVehiculo}.- Listado profesores y sus coches.");
    Console.WriteLine($"{(int)MenuOpcion.ActualizarVehiculo}.- Actualizar datos de un vehículo.");
    Console.WriteLine($"{(int)MenuOpcion.ActualizarProfesor}.- Actualizar datos de un profesor.");
    Console.WriteLine($"{(int)MenuOpcion.BorrarProfesor}.- Borrar profesor.");
    Console.WriteLine($"{(int)MenuOpcion.BorrarVehiculo}.- Borrar vehículo.");
    Console.WriteLine($"{(int)MenuOpcion.Salir}.- Salir.");
    Console.WriteLine("------------------------------------");
}

void RellenarParking(Vehiculo?[,] parking, ref int numVehiculos) {
    Log.Debug("🔵 Creando vehículos ejemplficativos...");
    // creo 3 vehiculos con sus profesores inventados
    Vehiculo v1 = new Vehiculo {matricula = "1234CBC", marca = "Seat", modelo = "Ibiza", profesor = {nip = "AB1", nombre = "JoseLuis", email = "joseluisgs@gmail.com"}};
    Vehiculo v2 = new Vehiculo {matricula = "6382JFF", marca = "Skoda", modelo = "Octavia", profesor = {nip = "ZJ7", nombre = "Carmen", email = "carme123@gmail.com"}};
    Vehiculo v3 = new Vehiculo {matricula = "3729FPL", marca = "Citroen", modelo = "C5", profesor = {nip = "HF7", nombre = "Pepe", email = "pepecito69@gmail.com"}};

    // array de vehiculos para asignarlos a una pos
    var coches = new Vehiculo[] { v1, v2, v3 };
    
    Log.Debug("🔵 Generando posiciones aleatorias..");
    // mientras que el aforo sea inferior al aforo inicial que se nos pide mete vehiculos (solo tenemos 3)
    while (numVehiculos < OcupacionInicial) {
        // generamos posiciones aleatorias
        // getlength mostrará la medida, por ejemplo 5 en el caso de las columnas, pero el 5 no entra asi que 
        // generara de 0-4, lo mismo con las filas
        int filaRandom = random.Next(parking.GetLength(0));
        int columnaRandom = random.Next(parking.GetLength(1));
        // si esta vacia se mete el coche extraido del array de coches y se incrementa el aforo actual
        if (parking[filaRandom, columnaRandom] is null) {
            parking[filaRandom, columnaRandom] = coches[numVehiculos];
            Log.Information($"✅  Coche {coches[numVehiculos].matricula} asignado a la posición {filaRandom}:{columnaRandom} correctamente.");
            numVehiculos++;
        }
    }
}

int ValidarOpcion(string msg) {

    int opcionElegida = 0;
    bool isOpcionOk = false;
    do {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim() ?? "-1";
        Log.Debug("🔵 Validando opción...");
        
        if (int.TryParse(input, out opcionElegida)) {
            if (opcionElegida >= (int)MenuOpcion.EntrarParking && opcionElegida <= (int)MenuOpcion.Salir) {
                Log.Information($"✅  Opción {opcionElegida} leída correctamente.");
                isOpcionOk = true;
            } else {
                Console.WriteLine($"🔴 Opción introducida innexistente. Introduzca una de las {MenuOpcion.Salir} posibles."); 
                Log.Information($"🔴  Opción {opcionElegida} no reconocida.");
            }
        } else {
            Console.WriteLine($"🔴 Opción introducida no reconocida. Introduzca una de las {MenuOpcion.Salir} posibles.");
            Log.Information($"🔴  Dato introducido no válido.");
        }
    } while (!isOpcionOk);
    return opcionElegida;
}