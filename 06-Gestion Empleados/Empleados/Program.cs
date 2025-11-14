// ._.

using System.Text;
using Empleados.Enums;
using Serilog;
using Empleados.Structs;

// zona de constantes
const int EmpleadosIniciales = 5;
const int TamañoInicial = 10;
const int EmpleadosMaximos = 25;

var random = Random.Shared;

// config. del logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();

// titulo de la terminal 
Console.Title = "Gestión Empleados";
// emojis en terminal
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(args);

// para no salir directamente
Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

// main daws template
void Main(string[] args) {
    Log.Debug("🔵 Iniciando el Main...");
    Console.WriteLine("👷 Bienvenido al Sistema de Gestión de Empleados");

    int numEmpleados = 0;

    Empleado?[] plantilla= new Empleado?[TamañoInicial];
    AsignarDatos(plantilla, ref numEmpleados);

}

// --------------------------- FUNCIONES AUXILIARES

/*
 * Se encarga de asignar X valores iniciales a la plantilla para así
 * iniciar la gestión con datos ejemplificativos
 */
void AsignarDatos(Empleado?[] plantilla, ref int empleados) {

    // creamos 5 empleados con datos inventados
    var e1 = new Empleado { Nip = "AA1", nombre = "José Luis", Edad = 18, Email = "joseluisgs@gmail.com", Cargo = Cargo.Director };
    var e2 = new Empleado { Nip = "BB2", nombre = "Rafa", Edad = 20, Email = "rafa123@gmail.com", Cargo = Cargo.Operario };
    var e3 = new Empleado { Nip = "CC3", nombre = "Aitor", Edad = 40, Email = "aitorcrack69@gmail.com", Cargo = Cargo.Operario };
    var e4 = new Empleado { Nip = "DD4", nombre = "Nicolás", Edad = 20, Email = "nicolas33@gmail.com", Cargo = Cargo.Supervisor };
    var e5 = new Empleado { Nip = "EE5", nombre = "Carlos", Edad = 25, Email = "carlitosalcaraz@gmail.com", Cargo = Cargo.Tecnico };
    
    // sorteamos su posición en la plantilla
    int posicion = 0;

    while (empleados < EmpleadosIniciales) {
        posicion = random.Next(plantilla.Length); // de 0-9 en este caso
    }

}