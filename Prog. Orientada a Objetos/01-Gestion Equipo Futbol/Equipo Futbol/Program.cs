// ._.

using System.Runtime.CompilerServices;
using System.Text;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;
using Equipo_Futbol.Utils;

// constantes globales
const int JugadoresIniciales = 5;
const int EdadMinima = 12;

const string RegexOpcionMenu = @"^[0-6]$";
const string RegexOpcionPosicion = @"^[1-4]$";
const string RegexDni = @"[0-9]{8}[a-zA-Z]{1}$";
const string RegexConfirmacion = @"^[SN]$";
const string RegexNombre = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ]{3,}$";
const string RegexEdad = @"^(0|[1-9]\d?)$";
const string RegexDorsal = @"^([1-9]\d?)$";

// config inicial
Log.Logger = new LoggerConfiguration().WriteTo.File("Logs/logs.txt").MinimumLevel.Debug().CreateLogger();
Console.Title = "Gestor Plantilla";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(); // daw's template sin args ya que en este programa no se requieren

Console.WriteLine("🖖 Pulse cualquier tecla para salir.");
Console.ReadKey();
return;

void Main() {
    Log.Debug("🔵 Iniciando Main...");

    // creacion array contenedor de jugadores
    var plantilla = new Jugador?[JugadoresIniciales]; // ahora mismo lleno de nulos
    Utilidades.InicializarJugadoresIniciales(plantilla);

    // gestion menú
    int opcionElegida;
    do {
        Utilidades.ImprimirMenu();
        opcionElegida = int.Parse(Utilidades.ValidarDato("- Opción elegida ->", RegexOpcionMenu));
        AsignarAccion(opcionElegida, ref plantilla);
    } while (opcionElegida != (int)OpcionMenu.Salir);
    Log.Debug("🔵 Saliendo del Main...");
}

void AsignarAccion(int opcion, ref Jugador?[] plantilla) {
    switch (opcion) {
        case (int)OpcionMenu.Salir:
            Console.WriteLine("🤗 Ha sido un placer!");
            break;
        case (int)OpcionMenu.CrearJugador:
            CrearJugador(ref plantilla);
            break;
        case (int)OpcionMenu.VerPlantilla:
            ImprimirPlantilla(plantilla);
            break;
        case (int)OpcionMenu.OrdenarPorGoles:
            OrdenarPorGoles(plantilla);
            break;
        case (int)OpcionMenu.ListarPorPosicion:
            ListarPorPosicion(plantilla);
            break;
        case (int)OpcionMenu.ActualizarJugador:
            ActualizarJugador(plantilla);
            break;
        case (int)OpcionMenu.BorrarJugador:
            BorrarJugador(ref plantilla);
            break;
        default: // no deberia pasar nunca ya que ValidarDato se asegura de que la opcion exista en el menu
            Log.Error("🔴  El programa ha fallado en 'ValidarDato' y no ha podido reconocer la opción introducida");
            Console.WriteLine("❌  Opción no reconocida. Introduzca una opción de las que se muestran en el menú.");
            break;
    }
}



// funciones CRUD

void CrearJugador(ref Jugador?[] plantilla) {
    Log.Debug("🔵 Creando nuevo Jugador...");
    var nuevoJugador = new Jugador();
    var isDniOk = false;
    var isEdadOk = false;
    
    // 1. dni
    do {
        var dni = Utilidades.ValidarDato("- Introduce el DNI: ", RegexDni);
        isDniOk = Utilidades.ValidarLetraDni(dni);
        if (isDniOk) {
            if (!Utilidades.IsJugadorInPlantilla(plantilla, dni)) nuevoJugador.Dni = dni;
            else {
                var confirmacion = Utilidades.ValidarDato($"🔴 El jugador {dni} ya existe. ¿Desea seguir creando al Jugador? (s/n):", RegexConfirmacion);
                if (confirmacion != "S") return;
                isDniOk = false;
            }
        }
    } while (!isDniOk);
    
    // 2. nombre
    var nombre = Utilidades.ValidarDato("- Introduce el Nombre: ", RegexNombre);
    nuevoJugador.Nombre = nombre;

    // 3. edad
    do {
        var edad = int.Parse(Utilidades.ValidarDato("- Introduce la Edad:", RegexEdad));
        if (edad < EdadMinima) {
            Console.WriteLine($"🔴  La edad mínima es de {EdadMinima} años.");
            continue;
        }
        nuevoJugador.Edad = edad;
        isEdadOk = true;
    } while (!isEdadOk);
    
    // 4. dorsal
    var dorsal = int.Parse(Utilidades.ValidarDato("- Introduce el Dorsal:", RegexDorsal));   
    nuevoJugador.Dorsal = dorsal;
    
    // 5. posicion
    Utilidades.ImprimirMenuPosicion();
    var opcionElegida = int.Parse(Utilidades.ValidarDato("- Introduce el número de la posición: ", RegexOpcionPosicion));
    nuevoJugador.Posicion = (Jugador.Posiciones)(opcionElegida - 1);

    // stats
    nuevoJugador.Goles = 0;
    nuevoJugador.Asistencias = 0;
    
    Console.WriteLine(nuevoJugador);
    var opcion = Utilidades.ValidarDato("- ¿Desea guardarlo? (s/n): ", RegexConfirmacion);
    if (opcion != "S") return;
    
    
    int indiceInsercion = -1;
    for (int i = 0; i < plantilla.Length; i++) {
        if (plantilla[i] == null) {
            indiceInsercion = i;
            break;
        }
    }
    if (indiceInsercion != -1) {
        plantilla[indiceInsercion] = nuevoJugador;
        Log.Information($"✅ Jugador {nuevoJugador.Dni} guardado.");
        Console.WriteLine($"✅ Jugador {nuevoJugador.Dni} guardado correctamente. Es el {indiceInsercion + 1}º Jugador de la plantilla.");
    } else {
        var nuevaPlantilla = new Jugador?[plantilla.Length + 1];
        Array.Copy(plantilla, nuevaPlantilla, plantilla.Length);
        nuevaPlantilla[nuevaPlantilla.Length - 1] = nuevoJugador;
        plantilla = nuevaPlantilla; 
        Log.Information($"✅ Jugador {nuevoJugador.Dni} guardado en la posición {plantilla.Length}");
        Console.WriteLine($"✅  Jugador guardado correctamente. Es el {plantilla.Length}º Jugador de la plantilla.");
    }
}

void ImprimirPlantilla(Jugador?[] plantilla) {
    Log.Debug("🔵 Imprimiendo Plantilla...");
    Console.WriteLine("-- PLANTILLA CD LEGANÉS 🥒");
    for (var i = 0; i < plantilla.Length; i++) 
        if (plantilla[i] is { } jugadorValido) Console.WriteLine($"{i+1}.- {jugadorValido}"); // si hay un jugador imprime los datos con ToString
}

void OrdenarPorGoles(Jugador?[] plantilla) {
    Log.Debug("🔵 Ordenando por Goles...");
    
    // contar jugadores
    var numJugadores = 0;
    foreach (var jugador in plantilla) {
        if (jugador != null) numJugadores++;
    }
    // copiar jugadores
    var plantillaSinNulos = new Jugador[numJugadores];
    for (var i = 0; i < plantilla.Length; i++) {
        if (plantilla[i] is { } jugadorValido) plantillaSinNulos[i] = jugadorValido;
    }
    
    Utilidades.OrdenarPlantilla(plantillaSinNulos);
    
    Console.WriteLine("------- 🎖️ PLANTILLA ORDENADA POR GOLES 🎖️ ------");
    foreach (var jugador in plantillaSinNulos) {
        if (jugador is { } jugadorValido) Console.WriteLine(jugadorValido);
    }
    Console.WriteLine();
}

void ListarPorPosicion(Jugador?[] plantilla) {
    throw new NotImplementedException();
}

void ActualizarJugador(Jugador?[] plantilla) {
    throw new NotImplementedException();
}

void BorrarJugador(ref Jugador?[] plantilla) {
    Log.Debug("🔵 Borrando Jugador...");
    var dni = Utilidades.ValidarDato("- Introduzca el DNI del Jugador a eliminar:", RegexDni);
    for (var i = 0; i < plantilla.Length; i++) {
        if (plantilla[i] is { } jugadorValido) {
            if (Utilidades.IsJugadorInPlantilla(plantilla, dni)) {
                Console.WriteLine("Jugador encontrado:");
                Console.WriteLine(jugadorValido);
                var opcion = Utilidades.ValidarDato("- ¿Desea eliminarlo? (s/n): ", RegexConfirmacion);
                if (opcion == "N") return;
                plantilla[i] = null;
                Log.Information($"✅ Jugador {dni} eliminado.");
                Console.WriteLine($"✅  Jugador {dni} eliminado correctamente.");
                Console.WriteLine();
                return;
            }
        }
    }
    Console.WriteLine($"🔴  El jugador {dni} no existe en la plantilla.");
}