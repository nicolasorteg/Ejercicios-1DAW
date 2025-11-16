// ._.
using System.Text;
using System.Text.RegularExpressions;
using Empleados.Enums;
using Serilog;
using Empleados.Structs;

// zona de constantes
const int EmpleadosIniciales = 5;
const int TamañoInicial = 10;
//const int EmpleadosMaximos = 25;

var random = Random.Shared;

// config. del logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

// título de la terminal 
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
    Log.Debug("- 🔵 Iniciando el Main...");
    Console.WriteLine("👷 Bienvenido al Sistema de Gestión de Empleados");

    int numEmpleados = 0;

    // creación vector que almacena plantilla + asignacion de datos
    var plantilla= new Empleado?[TamañoInicial];
    AsignarDatos(plantilla, ref numEmpleados);

    int opcionElegida = 0; // inicializada para garantizar una entrada segura
    do {
        ImprimirMenu();
        opcionElegida = ValidarOpcion("- Opción elegida: ");
        Console.WriteLine(); // salto de línea
        
        // asignacion de la opcion elegida
        switch (opcionElegida) {
            case (int)MenuPrincipal.CrearEmpleado: // 1
                CrearEmpleado(ref plantilla, ref numEmpleados);
                break;
            case (int)MenuPrincipal.VerEmpleados: // 2
                VerEmpleados(plantilla);
                break;
            case (int)MenuPrincipal.ListarEmpleadosNip: // 3
                ListarEmpleadosNip(plantilla);
                break;
            case (int)MenuPrincipal.MostrarPorCargo: // 4
                MostrarPorCargo(plantilla);
                break;
            case (int)MenuPrincipal.ActualizarEmpleado: // 5
                ActualizarEmpleado(plantilla);
                break;
            case (int)MenuPrincipal.BorrarEmpleado: // 6
                BorrarEmpleado(ref plantilla, ref numEmpleados);
                break;
            case (int)MenuPrincipal.Salir: // 0
                Console.WriteLine("Saliendo del programa...");
                break;
            default: // no deberia pasar nunca ya que ValidarOpcion se asegura de quela opcion exista en el menu
                Log.Error("🔴  El programa ha fallado en 'ValidarOpcion' y no ha podido reconocer la opción introducida");
                Console.WriteLine("❌  Opción no reconocida. Introduzca una opción de las que se muestran en el menú.");
                break;
        }
    } while (opcionElegida != (int)MenuPrincipal.Salir); // mientras la opcion no sea salir se seguirá repitiendo el menú
    Log.Debug("- 🔵 Fin del programa");
}

// --------------------------- FUNCIONES CRUD

void CrearEmpleado(ref Empleado?[] empleado, ref int i) {
    throw new NotImplementedException();
}

void VerEmpleados(Empleado?[] plantilla) {
    Log.Debug("🔵 Iniciando la vista de la plantilla...");
    Console.WriteLine("---- 👷 LISTADO EMPLEADOS 👷 ----");
    for (var i = 0; i < plantilla.Length; i++) { // recorremos y vamos imprimiendo uno a uno
        if (plantilla[i] is { } empleadoValido) { // si es un empleado imprime sus datos
            Console.WriteLine($"👷 NIP: {empleadoValido.Nip} | Nombre: {empleadoValido.Nombre} | Edad: {empleadoValido.Edad} | Email: {empleadoValido.Email} | Cargo: {empleadoValido.Cargo}");
        }
    }
}

void ListarEmpleadosNip(Empleado?[] empleado) {
    throw new NotImplementedException();
}

void MostrarPorCargo(Empleado?[] empleado) {
    throw new NotImplementedException();
}

void ActualizarEmpleado(Empleado?[] empleado) {
    throw new NotImplementedException();
}

void BorrarEmpleado(ref Empleado?[] empleado, ref int i) {
    throw new NotImplementedException();
}



// --------------------------- FUNCIONES AUXILIARES

/*
 * Se encarga de asignar X valores iniciales a la plantilla para así
 * iniciar la gestión con datos ejemplificativos
 */
void AsignarDatos(Empleado?[] plantilla, ref int empleados) {
    
    Log.Debug("🔵 Empezando la asignación de datos...");
    // creamos 5 empleados con datos inventados
    var e1 = new Empleado { Nip = "AA1", Nombre = "José Luis", Edad = 18, Email = "joseluisgs@gmail.com", Cargo = Cargo.Director };
    var e2 = new Empleado { Nip = "BB2", Nombre = "Rafa", Edad = 20, Email = "rafa123@gmail.com", Cargo = Cargo.Operario };
    var e3 = new Empleado { Nip = "CC3", Nombre = "Aitor", Edad = 40, Email = "aitorcrack69@gmail.com", Cargo = Cargo.Operario };
    var e4 = new Empleado { Nip = "DD4", Nombre = "Nicolás", Edad = 20, Email = "nicolas33@gmail.com", Cargo = Cargo.Supervisor };
    var e5 = new Empleado { Nip = "EE5", Nombre = "Carlos", Edad = 25, Email = "carlitosalcaraz@gmail.com", Cargo = Cargo.Tecnico };
    var trabajadores = new Empleado[] { e1, e2, e3, e4, e5 };
    
    // sorteamos su posición en la plantilla
    int posicion = 0;

    Log.Debug("🔵 Comenzando el sorteo de posición...");
    while (empleados < EmpleadosIniciales) {
        posicion = random.Next(plantilla.Length); // de 0-9 en este caso
        if (plantilla[posicion] == null) {
            plantilla[posicion] = trabajadores[empleados];
            Log.Information($"✅  Empleado {trabajadores[empleados].Nip} asignado a la posición {posicion} correctamente.");
            empleados++;
        }
    }
}

/*
 * Procedimiento encargado de imprimir el Menú Principal del programa
 * haciendo uso del enum MenuPrincipal para mostras las posibles opciones
 */
void ImprimirMenu() {
    Console.WriteLine();
    Console.WriteLine("----👷‍♂️MENÚ PRINCIPAL👷‍♂️----");
    Console.WriteLine($"{(int)MenuPrincipal.CrearEmpleado}.- Crear Empleado.");
    Console.WriteLine($"{(int)MenuPrincipal.VerEmpleados}.- Ver listado Empleados.");
    Console.WriteLine($"{(int)MenuPrincipal.ListarEmpleadosNip}.- Listar Empleados por Nip.");
    Console.WriteLine($"{(int)MenuPrincipal.MostrarPorCargo}.- Listar por Cargo.");
    Console.WriteLine($"{(int)MenuPrincipal.ActualizarEmpleado}.- Actualizar datos Empleado.");
    Console.WriteLine($"{(int)MenuPrincipal.BorrarEmpleado}.- Borrar Empleado.");
    Console.WriteLine($"{(int)MenuPrincipal.Salir}.- Salir.");
    Console.WriteLine("---------------------------------");
}


int ValidarOpcion(string prompt) {
    var isOpcionOk = false;
    string input;
    var regexOpcion = new Regex(@$"^[0-{MenuPrincipal.BorrarEmpleado}]$");

    do {
        Console.WriteLine(prompt);
        input = Console.ReadLine()?.Trim() ?? "-1";
        if (regexOpcion.IsMatch(input)) {
            Log.Information($"✅ Opción {input} leída correctamente.");
            isOpcionOk = true;
        } else {
            Log.Warning($"⚠️ La opción {input}");
            Console.WriteLine("⚠️ Opción introducida no reconocida. Introduzca el número de una opción existente.");
        }
    } while (!isOpcionOk);
    return Convert.ToInt32(input);
}