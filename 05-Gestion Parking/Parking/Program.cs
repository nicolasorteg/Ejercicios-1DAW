using System.Text;
using System.Text.RegularExpressions;
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
    Console.WriteLine("😊 Bienvenid@ al programa de gestión del Parking del Luis Vives.");

    int numVehiculos = 0;
    // creación del parking
    Vehiculo?[,] parking = new Vehiculo?[2, 5];
    // rellenamos para comenzar con algun dato ejemplificativo
    RellenarParking(parking, ref numVehiculos);
    int opcionElegida = 0;
    do {
        Console.WriteLine();
        ImprimirMenu();
        opcionElegida = ValidarOpcion("Introduzca una opción:");

        Log.Debug($"🔵 Asignando acción para la opción {opcionElegida}");
        Console.WriteLine();
        switch (opcionElegida) {
            case (int)MenuOpcion.EntrarParking: // 1
                Posicion posicion = SimularBarreraEntrada(parking, ref numVehiculos);
                if (posicion.Fila != -1 && posicion.Columna != -1)
                    Console.WriteLine($"Dirige el coche a la posición {posicion.Fila + 1}:{posicion.Columna + 1}");
                break;
            case (int)MenuOpcion.AñadirVehiculo: // 2
                AñadirVehiculo(parking, ref numVehiculos);
                break;
            case (int)MenuOpcion.VerParking: // 3
                MostrarParking(parking);
                break;
            case (int)MenuOpcion.InfPlaza: // 4
                LeerInformacionPlaza(parking);
                break;
            case (int)MenuOpcion.BusquedaNip: // 5
                BuscarPorNip(parking);
                break;
            case (int)MenuOpcion.BusquedaMatricula: // 6
                BuscarPorMatricula(parking);
                break;
            case (int)MenuOpcion.ListaMatricula: // 7
                MostrarPorMatriculaAsc(parking);
                break;
            case (int)MenuOpcion.ListaProfesoresConVehiculo: // 8
                MostrarProfesores(parking);
                break;
            case (int)MenuOpcion.ActualizarVehiculo: // 9
                //ActualizarVehiculo(parking);
                break;
            case (int)MenuOpcion.ActualizarProfesor: // 10
                //ActualizarProfesor(parking);
                break;
            case (int)MenuOpcion.BorrarVehiculo: // 11
                BorrarVehiculo(parking, ref numVehiculos);
                break;
            case (int)MenuOpcion.Salir: // 12
                Console.WriteLine("😊 Ha sido un placer...");
                break;
            default: // no deberia poder llegar aqui ya que 'ValidarOpcion' solo puede devolver un numero valido
                Console.WriteLine("🔴 Error en la validación de datos.");
                break;
        }
    } while (opcionElegida != (int)MenuOpcion.Salir);
    
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
    Console.WriteLine($"{(int)MenuOpcion.BorrarVehiculo}.- Borrar vehículo.");
    Console.WriteLine($"{(int)MenuOpcion.Salir}.- Salir.");
    Console.WriteLine("------------------------------------");
}

void RellenarParking(Vehiculo?[,] parking, ref int numVehiculos) {
    Log.Debug("🔵 Creando vehículos ejemplficativos...");
    // creo 3 vehiculos con sus profesores inventados
    Vehiculo v1 = new Vehiculo {Matricula = "1234CBC", Marca = "Seat", Modelo = "Ibiza", Profesor = {Nip = "AB1", Nombre = "JoseLuis", Email = "joseluisgs@gmail.com"}};
    Vehiculo v2 = new Vehiculo {Matricula = "6382JFF", Marca = "Skoda", Modelo = "Octavia", Profesor = {Nip = "ZJ7", Nombre = "Carmen", Email = "carme123@gmail.com"}};
    Vehiculo v3 = new Vehiculo {Matricula = "3729FPL", Marca = "Citroen", Modelo = "C5", Profesor = {Nip = "HF7", Nombre = "Pepe", Email = "pepecito69@gmail.com"}};

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
            Log.Information($"✅  Coche {coches[numVehiculos].Matricula} asignado a la posición {filaRandom}:{columnaRandom} correctamente.");
            numVehiculos++;
        }
    }
}

// ------------------------ FUNCIONES CRUD ------------------------



Posicion SimularBarreraEntrada(Vehiculo?[,] parking, ref int numVehiculos) {
    
    Console.WriteLine("🚧 Barrera de entrada.");
    
    string matriculaElegida = ValidarMatricula("Introduce la matrícula (Ej: 1234CBC): ");
    bool encontrado = false;
    
    Log.Debug($"🔵 Buscando vehículo con matrícula {matriculaElegida}");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j]?.Matricula == matriculaElegida) {
                Log.Warning($"⚠️ Vehículo con matrícula {matriculaElegida} encontrado en posición {i + 1}:{j + 1}.");
                encontrado = true;
            }
        }
    }
    
    int filaRandom = -1;
    int columnaRandom = -1;
    
    if (encontrado) {
        Console.WriteLine($"❌  Ya existe un vehículo con matrícula {matriculaElegida}");
    } else {
        var vehiculoIntroducido = new Vehiculo {Matricula = matriculaElegida};
        bool isCocheIntroducido = false;
    
        while (!isCocheIntroducido) {
        
            filaRandom = random.Next(parking.GetLength(0));
            columnaRandom = random.Next(parking.GetLength(1));
            // si esta vacia se mete el coche extraido del array de coches y se incrementa el aforo actual
            if (parking[filaRandom, columnaRandom] is null) {
                parking[filaRandom, columnaRandom] = vehiculoIntroducido;
                isCocheIntroducido = true;
                Log.Information($"✅  Coche {vehiculoIntroducido.Matricula} asignado a la posición {filaRandom}:{columnaRandom} correctamente.");
                numVehiculos++;
            }
        }
    }
    return new Posicion {
        Fila = filaRandom,
        Columna = columnaRandom
    };
}


void AñadirVehiculo(Vehiculo?[,] parking, ref int numVehiculos) {
    Posicion posicion = ValidarPosicion("Posición: ");
    
    if (parking[posicion.Fila, posicion.Columna] != null) {
        Console.WriteLine($"❌ Ya existe un vehículo para la posición {posicion.Fila}:{posicion.Columna}");
        Log.Warning($"⚠️ Vehículo encontrado en {posicion.Fila}:{posicion.Columna}");
    } else {
        string matriculaElegida = ValidarMatricula("Introduce la matrícula (Ej: 1234CBC): ");
    }
}


void MostrarParking(Vehiculo?[,] parking) {

    Log.Debug("🔵 Mostrando el parking...");
    Console.WriteLine("------ PARKING IES LUIS VIVES ------");
    Console.WriteLine();
    
    // cabecera
    Console.Write("   ");
    for (int i = 0; i < parking.GetLength(1); i++) {
        Console.Write((i + 1) + "   ");
    }
    Console.WriteLine();
        
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (j == 0) { // indices filas
                Console.Write((i + 1) + " ");
            }
            if (parking[i, j] is null)
                Console.Write("[🔵]");
            else 
                Console.Write("[🚗]");
        }
        Console.WriteLine();
    }
}


void LeerInformacionPlaza(Vehiculo?[,] parking) {
    Posicion posicion = ValidarPosicion("Posición: ");

    if (parking[posicion.Fila, posicion.Columna] == null) {
        Console.WriteLine($"❌ No existe ningún vehículo para la posición {posicion.Fila}:{posicion.Columna}");
        Log.Warning($"⚠️ Vehículo  para la posición {posicion.Fila}:{posicion.Columna} no encontrado.");
    } else {
        Log.Information($"✅  Vehículo en {posicion.Fila}:{posicion.Columna} encontrado.");
        
        Vehiculo? vehiculoElegido = parking[posicion.Fila, posicion.Columna];
        
        Console.WriteLine();
        Console.WriteLine("-- 🚗 Información del vehículo --");
        Console.WriteLine($"- Matrícula: {vehiculoElegido?.Matricula}");
        Console.WriteLine($"- Marca: {vehiculoElegido?.Marca}");
        Console.WriteLine($"- Modelo: {vehiculoElegido?.Modelo}");
        Console.WriteLine("-- 👨‍🏫 Información del propietario --");
        Console.WriteLine($"- NIP: {vehiculoElegido?.Profesor.Nip}");
        Console.WriteLine($"- Nombre: {vehiculoElegido?.Profesor.Nombre}");
        Console.WriteLine($"- Email: {vehiculoElegido?.Profesor.Email}");
    }
}


void BuscarPorNip(Vehiculo?[,] parking) {
    string nipElegido = ValidarNip("Nip: "); // extraemos el nip introducido

    Log.Debug($"🔵 Buscando profesor de NIP {nipElegido}");
    // recorremos el parking en busca del nip
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            
            // si encontramos el nip imprimimos la info
            if (parking[i, j]?.Profesor.Nip == nipElegido) {
                Log.Information($"✅  Profesor de Nip {nipElegido} encontrado.");
                
                Profesor? profesorElegido = parking[i, j]?.Profesor;
                Vehiculo? vehiculoProfesor = parking[i, j];
                
                Console.WriteLine();
                Console.WriteLine("-- 👨‍🏫 Información del propietario --");
                Console.WriteLine($"- NIP: {profesorElegido?.Nip}");
                Console.WriteLine($"- Nombre: {profesorElegido?.Nombre}");
                Console.WriteLine($"- Email: {profesorElegido?.Email}");
                Console.WriteLine("-- 🚗 Información del vehículo --");
                Console.WriteLine($"- Matrícula: {vehiculoProfesor?.Matricula}");
                Console.WriteLine($"- Marca: {vehiculoProfesor?.Marca}");
                Console.WriteLine($"- Modelo: {vehiculoProfesor?.Modelo}");
                Console.WriteLine($"-- 📍 Plaza: {i + 1}:{j + 1} --");
                Console.WriteLine();
                return;
            }
        }
    }
    // si no hemos encontrado el bucle finalizara y mostramos la derrota
    Console.WriteLine($"❌ No se encontró ningún profesor con NIP {nipElegido}.");
    Log.Warning($"⚠️ Profesor con NIP {nipElegido} no encontrado.");
}


void BuscarPorMatricula(Vehiculo?[,] parking) {
    string matriculaElegida = ValidarMatricula("Introduce la matrícula (Ej: 1234CBC): ");

    Log.Debug($"🔵 Buscando vehículo con matrícula {matriculaElegida}");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            
            if (parking[i, j]?.Matricula == matriculaElegida) {
                Log.Information($"✅ Vehículo con matrícula {matriculaElegida} encontrado en posición {i + 1}:{j + 1}.");
                
                var vehiculo = parking[i, j];
                Console.WriteLine();
                Console.WriteLine("-- 🚗 Información del vehículo --");
                Console.WriteLine($"- Matrícula: {vehiculo?.Matricula}");
                Console.WriteLine($"- Marca:     {vehiculo?.Marca}");
                Console.WriteLine($"- Modelo:    {vehiculo?.Modelo}");
                Console.WriteLine("-- 👨‍🏫 Información del propietario --");
                Console.WriteLine($"- NIP:    {vehiculo?.Profesor.Nip}");
                Console.WriteLine($"- Nombre: {vehiculo?.Profesor.Nombre}");
                Console.WriteLine($"- Email:  {vehiculo?.Profesor.Email}");
                Console.WriteLine($"-- 📍 Plaza: {i + 1}:{j + 1} --");
                return;
            }
        }
    }
    
    Console.WriteLine($"❌  No se encontró ningún vehículo con matrícula {matriculaElegida}.");
    Log.Warning($"⚠️  Vehículo con matrícula {matriculaElegida} no encontrado.");

}


void MostrarPorMatriculaAsc(Vehiculo?[,] parking) {
    Log.Debug("🔵 Mostrando vehículos ordenados por matrícula...");
    
    // creamos un array donde se almacenaran los coches que hay actualmente en el parking
    var vehiculosExistentes = CrearArrayVehiculos(parking);

    // ordenamos el array
    Log.Debug($"🔵 Ordenando por matrícula...");
    OrdenarVehiculosBurbuja(vehiculosExistentes);
    
    // mostramos los datos por pantalla
    Log.Debug($"🔵 Mostrando los datos...");
    Console.WriteLine();
    ImprimirDatos(vehiculosExistentes);
}


void MostrarProfesores(Vehiculo?[,] parking) {
    Log.Debug("🔵 Mostrando vehículos ordenados por matrícula...");

    var vehiculosExistentes = CrearArrayVehiculos(parking);
    
    // mostramos los datos por pantalla
    Log.Debug($"🔵 Mostrando los datos...");
    Console.WriteLine();
    ImprimirDatos(vehiculosExistentes);
}


void BorrarVehiculo(Vehiculo?[,] parking, ref int numVehiculos) {
    Log.Debug("🔵 Comenzando el proceso de eliminación...");
    string matriculaElegida = ValidarMatricula("Introduce la matrícula del vehículo a borrar (Ej: 1234CBC): ");
    
    Log.Debug($"🔵 Buscando vehículo con matrícula {matriculaElegida}");
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            
            if (parking[i, j]?.Matricula == matriculaElegida) {
                parking[i, j] = null;
                numVehiculos--;
                Log.Information($"✅ Vehículo con matrícula {matriculaElegida} encontrado y borrado.");
                Console.WriteLine("✅  Vehículo borrado con éxito");
            }
        }
    }
}






// ----------------------- FUNCIONES AUXILIARES -----------------------

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



Posicion ValidarPosicion(string msg) {

    int filaElegida = 0;
    int columnaElegida = 0;
    bool isPosicionOk = false;
    var regexPosicion = new Regex (@"^[1-2]:[1-5]$");
    do {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim() ?? "-1";
        Log.Debug("🔵 Validando posicion...");
        
        if (regexPosicion.IsMatch(input)) {
            var posicion = input.Split(":");
            filaElegida = Convert.ToInt32(posicion[0]);
            Log.Information($"✅  Fila {filaElegida} leída correctamente.");
            columnaElegida = Convert.ToInt32(posicion[1]);
            Log.Information($"✅  Columna {columnaElegida} leída correctamente.");
            isPosicionOk = true;
        } else {
            Console.WriteLine($"🔴 Posición introducida no reconocida. Introduzca una posición existente.");
            Log.Information($"🔴  Posición introducida no válida.");
        }
    } while (!isPosicionOk);

    return new Posicion {
        Fila = filaElegida - 1,
        Columna = columnaElegida - 1
    };
}


string ValidarNip(string msg) {
    string nip = "";
    bool isNipOk = false;
    var regexPosicion = new Regex (@"^[A-Z]{2}\d$");
    do {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim() ?? "-1";
        Log.Debug("🔵 Validando posicion...");
        
        if (regexPosicion.IsMatch(input)) {
            nip = input;
            Log.Information($"✅  Nip {nip} leído correctamente.");
            isNipOk = true;
        } else {
            Console.WriteLine($"🔴 Nip introducido no reconocida. Introduzca un Nip válido (LLN).");
            Log.Information($"🔴  Nip introducido no válido.");
        }
    } while (!isNipOk);

    return nip;
}

string ValidarMatricula(string msg)
{
    string matricula = "";
    bool isMatriculaOk = false;
    var regexMatricula = new Regex(@"^\d{4}[B-DF-HJ-NP-TV-Z]{3}$");
    do
    {
        Console.WriteLine(msg);
        var input = Console.ReadLine()?.Trim().ToUpper() ?? "";
        Log.Debug($"🔵  Validando matrícula {input}...");

        if (regexMatricula.IsMatch(input)) {
            matricula = input;
            Log.Information($"✅  Matrícula {matricula} leída correctamente.");
            isMatriculaOk = true;
        } else
        {
            Console.WriteLine("🔴 Matrícula introducida no válida. Formato esperado: 1234ABC");
            Log.Information("🔴  Matrícula introducida no válida.");
        }
    } while (!isMatriculaOk);

    return matricula;
}

void OrdenarVehiculosBurbuja(Vehiculo[] vehiculosExistentes) {
    // ordenamos usando el metodo de la burbuja
    for (int i = 0; i < vehiculosExistentes.Length - 1; i++) {
        bool swapped = false;
        for (int j = 0; j < vehiculosExistentes.Length - i - 1; j++) {
            
            // comparacion de los numeros de la matricla
            int matriculaActual = Convert.ToInt32(vehiculosExistentes[j].Matricula.Substring(0, 4));
            int matriculaSiguiente = Convert.ToInt32(vehiculosExistentes[j + 1].Matricula.Substring(0, 4));

            // si la siguiente matricula es menor se pone en la posicion actual
            if (matriculaActual > matriculaSiguiente) {
                // swap
                SwapVehiculos(vehiculosExistentes, j, j + 1);
                swapped = true;
            }
        }
        // si no hubo intercambio el array está ordenado asc en base a su matricula
        if (!swapped) break;
    }
}

void SwapVehiculos(Vehiculo[] vehiculos, int i, int j) {
    Vehiculo temp = vehiculos[i];
    vehiculos[i] = vehiculos[j];
    vehiculos[j] = temp;
}

void ImprimirDatos(Vehiculo[] vehiculos) {
    
    Console.WriteLine("-- 🚗 Listado de vehículos por matrícula (ascendente) --");
    foreach (Vehiculo vehiculo in vehiculos) {
        Console.WriteLine("-- 🚗 Información del vehículo --");
        Console.WriteLine($"- Matrícula: {vehiculo.Matricula}");
        Console.WriteLine($"- Marca: {vehiculo.Marca}");
        Console.WriteLine($"- Modelo: {vehiculo.Modelo}");
        Console.WriteLine("-- 👨‍🏫 Información del propietario --");
        Console.WriteLine($"- NIP: {vehiculo.Profesor.Nip}");
        Console.WriteLine($"- Nombre: {vehiculo.Profesor.Nombre}");
        Console.WriteLine($"- Email: {vehiculo.Profesor.Email}");
        Console.WriteLine("---------------------------------");
    }
}

Vehiculo[] CrearArrayVehiculos(Vehiculo?[,] parking) {
    // para determinar el tamaño del vector de vehiculos necesitamos saber cuantos hay actualmente en el parking
    int numCoches = 0;
    
    // recorremos el parking para ver cuantos coches hay
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is not null)
                numCoches++;
        }
    }
    Vehiculo[] vehiculosExistentes = new Vehiculo[numCoches];
    if (numCoches == 0) {
        Console.WriteLine("❌ No hay vehículos en el parking.");
        Log.Warning("⚠️ No hay vehículos para mostrar.");
        return vehiculosExistentes;
    }
    
    // ponemos a los vehiculos en el vector de vehiculos
    int indice = 0;
    for (int i = 0; i < parking.GetLength(0); i++) {
        for (int j = 0; j < parking.GetLength(1); j++) {
            if (parking[i, j] is not null) {
                vehiculosExistentes[indice] = parking[i, j].Value;
                indice++;
            }
        }
    }

    return vehiculosExistentes;
}
