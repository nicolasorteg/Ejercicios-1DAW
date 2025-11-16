// ._.
using System.Text;
using System.Text.RegularExpressions;
using Empleados.Enums;
using Serilog;
using Empleados.Structs;

// zona de constantes
const int EmpleadosIniciales = 5;
const int TamañoInicial = 10;
const int IncrementoPlantilla = 5;

const string RegexNip = @"^[A-Z]{2}\d$";
const string RegexNombre = @"^\w{3,}$";
const string RegexEdad = @"^\d{2,}$";
const string RegexEmail = @"^\w{3,}@\w{3,}.{1,}$";
const string RegexCargo = @"^\w{6,}$";
const string RegexOpcion = @"^[0-6]$";

const int EmpleadosMaximos = 25;

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
        opcionElegida = Convert.ToInt32(ValidarDatos("- Opción elegida: ", RegexOpcion));
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
                ListarEmpleadosNip(plantilla, numEmpleados);
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

void CrearEmpleado(ref Empleado?[] plantilla, ref int numEmpleados) {
    
    if (plantilla.Length >= EmpleadosMaximos) {
        Log.Warning("⚠️ No se puede acceder a la creación de un empleado, plantila llena.");
        Console.WriteLine("🔴  Plantilla llena. Para añadir un empleado deberás despedir a alguien antes. ");
        return;
    }
    Log.Debug("🔵 Empezando la creación de empleado...");
    Empleado newEmpleado;
    
    // si es necesario se añaden plazas
    if (numEmpleados == plantilla.Length) plantilla = RedimensionarPlantilla(plantilla, numEmpleados);
    Console.WriteLine("--- 👷 CREACIÓN EMPLEADO 👷 ---");
    newEmpleado = PreguntarDatos(); // preguntamos los datos
    for (var i = 0; i < plantilla.Length; i++) { // buscamos un hueco vacio y metemos al nuevo empleado
        if (plantilla[i] is null) {
            plantilla[i] = newEmpleado;
            Console.WriteLine($"✅  Empleado {newEmpleado.Nip} introducido en la plantilla.");
            Log.Information($"✅ Empleado {newEmpleado.Nip} introducido en la plantilla.");
            numEmpleados++;
            return;
        }
    }
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

void ListarEmpleadosNip(Empleado?[] plantilla, int numEmpleados) {
    Log.Debug("🔵 Iniciando la vista de la plantilla...");
    var plantillaSinNulos = new Empleado[numEmpleados];
    
    QuitarNulos(plantillaSinNulos, plantilla);
    OrdenarPorNip(plantillaSinNulos);
    
    Console.WriteLine("---- 👷 LISTADO EMPLEADOS 👷 ----");
    for (var i = 0; i < plantillaSinNulos.Length; i++) { // recorremos y vamos imprimiendo uno a uno
        if (plantillaSinNulos[i] is { } empleadoValido) { // si es un empleado imprime sus datos
            Console.WriteLine($"👷 NIP: {empleadoValido.Nip} | Nombre: {empleadoValido.Nombre} | Edad: {empleadoValido.Edad} | Email: {empleadoValido.Email} | Cargo: {empleadoValido.Cargo}");
        }
    }
}

void MostrarPorCargo(Empleado?[] plantilla) {
    throw new NotImplementedException();
}

void ActualizarEmpleado(Empleado?[] plantilla) {
    throw new NotImplementedException();
}

void BorrarEmpleado(ref Empleado?[] plantilla, ref int numEmpleados) {
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

Empleado?[] RedimensionarPlantilla(Empleado?[] plantilla, int numEmpleados) {
    var newPlantilla = new Empleado?[numEmpleados + IncrementoPlantilla];
    int index = 0;
    for (var i = 0; i < plantilla.Length; i++) { // recorremos plantilla para poner los empleados en el nuevo vector
        if (plantilla[i] is { } empleadoValido) { // si es un empleado entra
            newPlantilla[index] = empleadoValido;
        }
    }
    return newPlantilla;
}

Empleado PreguntarDatos() {
    
    var nip = ValidarDatos("NIP = ", RegexNip);
    var nombre = ValidarDatos("Nombre = ", RegexNombre);
    var edad = ValidarDatos("Edad = ", RegexEdad);
    var email = ValidarDatos("Email = ", RegexEmail);
    var cargo = ValidarDatos("Cargo (Director/Operario/Supervisor/Tecnico) = ", RegexCargo);
    
    if (!Enum.TryParse(cargo, out Cargo cargoFinal)) {
        Console.WriteLine("❌ Ese cargo no existe. Se asignará 'Operario' por defecto.");
        cargoFinal = Cargo.Operario;
    }
    return new Empleado {
        Nip = nip,
        Nombre = nombre,
        Edad = int.Parse(edad),
        Email = email,
        Cargo = cargoFinal
    };
}

string ValidarDatos(string msg, string rgx) {
    string input;
    var isDatoOk = false;
    var regex = new Regex(rgx);
    do {
        Console.WriteLine(msg);
        input = Console.ReadLine()?.Trim() ?? "-1";
        if (regex.IsMatch(input)) {
            Log.Information("✅ Dato leído correctamente.");
            isDatoOk = true;
        } else {
            Log.Warning($"⚠️ {input} no es un dato válido para este campo.");
            Console.WriteLine("🔴  Dato introducido inválido.");
        }
    } while (!isDatoOk);
    return input;
}

void QuitarNulos(Empleado[] plantillaSinNulos, Empleado?[] plantilla) {
    var index = 0;
    for (int i = 0; i < plantilla.Length; i++) {
        if (plantilla[i] is { } empleadoValido) {
            plantillaSinNulos[index] = empleadoValido;
            index++;
        }
    }
}

void OrdenarPorNip(Empleado[] plantilla) {
    for (int i = 0; i < plantilla.Length - 1; i++) {
        bool swapped = false;
        for (int j = 0; j < plantilla.Length - i - 1; j++) {
            // datos a comparar
            var nipActual = plantilla[j].Nip;
            char primeraLetraNipActual = Convert.ToChar(nipActual[0]);
            char segundaLetraNipActual = Convert.ToChar(nipActual[1]);
            int numNipActual = Convert.ToInt32((nipActual[2]));
            
            var nipSiguiente = plantilla[j + 1].Nip;
            char primeraLetraNipSiguiente = Convert.ToChar(nipSiguiente[0]);
            char segundaLetraNipSiguiente = Convert.ToChar(nipSiguiente[1]);
            int numNipSiguiente = Convert.ToInt32((nipSiguiente[2]));

            // si el siguiente nip es menor se pone en la posicion actual
            if (primeraLetraNipActual == primeraLetraNipSiguiente) { // si las primeras letras son iguales comprobamos las 2
                if (segundaLetraNipActual == segundaLetraNipSiguiente) { // si tambien son iguales, pasamos a los numeros
                    if (numNipActual > numNipSiguiente) {
                        // swap
                        SwapEmpleados(plantilla, j, j + 1);
                        swapped = true;
                    }
                } else if (segundaLetraNipActual > segundaLetraNipSiguiente) {
                    SwapEmpleados(plantilla, j, j + 1);
                    swapped = true;
                }
            } else if (primeraLetraNipActual > primeraLetraNipSiguiente) {
                // swap
                SwapEmpleados(plantilla, j, j + 1);
                swapped = true;
            }
        }
        // si no hubo intercambio el array está ordenado asc en base a su nip
        if (!swapped) break;
    }
}
void SwapEmpleados(Empleado[] plantilla, int i, int j) {
    Empleado temp = plantilla[i];
    plantilla[i] = plantilla[j];
    plantilla[j] = temp;
}