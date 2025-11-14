// ._.

using System.Text;
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

    Empleado?[] plantilla= new Empleado?[EmpleadosIniciales];
    //AsignarDatos(plantilla, ref numEmpleados);

}