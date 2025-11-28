// ._.
using System.Text;
using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;
using Equipo_Futbol.Utils;

// constantes globales
const int JugadoresIniciales = 5;
const string RegexOpcionMenu = @"^[0-6]$";
const string RegexDni = @"[0-9]{8}[a-zA-Z]{1}$";
const string RegexConfirmacion = @"^[SN]$";

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
    var numJugadores = JugadoresIniciales;

    // gestion menú
    int opcionElegida;
    do {
        Utilidades.ImprimirMenu();
        opcionElegida = int.Parse(Utilidades.ValidarDato("- Opción elegida ->", RegexOpcionMenu));
        AsignarAccion(opcionElegida, plantilla, numJugadores);
    } while (opcionElegida != (int)OpcionMenu.Salir);
    Log.Debug("🔵 Saliendo del Main...");
}

void AsignarAccion(int opcion, Jugador?[] plantilla, int numJugadores ) {
    switch (opcion) {
        case (int)OpcionMenu.Salir:
            Console.WriteLine("🤗 Ha sido un placer!");
            break;
        case (int)OpcionMenu.CrearJugador:
            CrearJugador(ref plantilla, ref numJugadores);
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
            BorrarJugador(ref plantilla, ref numJugadores);
            break;
        default: // no deberia pasar nunca ya que ValidarDato se asegura de que la opcion exista en el menu
            Log.Error("🔴  El programa ha fallado en 'ValidarDato' y no ha podido reconocer la opción introducida");
            Console.WriteLine("❌  Opción no reconocida. Introduzca una opción de las que se muestran en el menú.");
            break;
    }
}



// funciones CRUD

void CrearJugador(ref Jugador?[] plantilla, ref int numJugadores) {
    throw new NotImplementedException();
}

void ImprimirPlantilla(Jugador?[] plantilla) {
    Log.Debug("🔵 Imprimiendo Plantilla...");
    Console.WriteLine("-- PLANTILLA CD LEGANÉS 🥒");
    for (var i = 0; i < plantilla.Length; i++) 
        if (plantilla[i] is { } jugadorValido) Console.WriteLine($"{i+1}.- {jugadorValido}"); // si hay un jugador imprime los datos con ToString
}

void OrdenarPorGoles(Jugador?[] plantilla) {
    throw new NotImplementedException();
}

void ListarPorPosicion(Jugador?[] plantilla) {
    throw new NotImplementedException();
}

void ActualizarJugador(Jugador?[] plantilla) {
    throw new NotImplementedException();
}

void BorrarJugador(ref Jugador?[] plantilla, ref int numJugadores) {
    Log.Debug("🔵 Borrando Jugador...");
    var dni = Utilidades.ValidarDato("- Introduzca el DNI del Jugador a eliminar:", RegexDni);
    for (var i = 0; i < plantilla.Length; i++) {
        if (plantilla[i] is { } jugadorValido) {
            if (jugadorValido.Dni == dni) {
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



