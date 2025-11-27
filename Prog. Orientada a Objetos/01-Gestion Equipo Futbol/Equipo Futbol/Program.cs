// ._.
using System.Text;
using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;

// constantes globales
const int JugadoresIniciales = 5;
const string RegexOpcionMenu = @"^[0-6]$";

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
    InicializarJugadoresIniciales(plantilla);
    var numJugadores = JugadoresIniciales;

    // gestion menú
    int opcionElegida;
    do {
        ImprimirMenu();
        opcionElegida = int.Parse(ValidarDato("- Opción elegida ->", RegexOpcionMenu));
        AsignarAccion(opcionElegida, plantilla, numJugadores);
    } while (opcionElegida != (int)OpcionMenu.Salir);
    Log.Debug("🔵 Saliendo del Main...");
}

// funciones CRUD

void CrearJugador(ref Jugador?[] plantilla, ref int numJugadores) {
    throw new NotImplementedException();
}

void ImprimirPlantilla(Jugador?[] plantilla, int numJugadores) {
    Log.Debug("🔵 Imprimiendo Plantilla...");
    Console.WriteLine("-- PLANTILLA CD LEGANÉS 🥒");
    for (var i = 0; i < plantilla.Length; i++) 
        if (plantilla[i] is { } jugadorValido) Console.WriteLine($"{i+1}.- {jugadorValido.ToString()}"); 
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
    throw new NotImplementedException();
}





// funciones auxiliares
void InicializarJugadoresIniciales(Jugador?[] jugadores) {
    Log.Debug("🔵 Creando jugadores ejemplificativos...");
    var j1 = new Jugador("12345678A", "Iker", 38, 1, PosicionJugador.Portero, 0, 1);
    jugadores[0] = j1;
    var j2 = new Jugador("23456789B", "Sergio", 35, 3, PosicionJugador.Defensa, 20, 44);
    jugadores[1] = j2;
    var j3 = new Jugador("34567890C", "Luka", 19, 10, PosicionJugador.Mediocentro, 60, 130);
    jugadores[2] = j3;
    var j4 = new Jugador("45678901D", "Karim", 36, 9, PosicionJugador.Delantero, 300, 100);
    jugadores[3] = j4;
    var j5 = new Jugador("56789012E", "Cristiano", 37, 7, PosicionJugador.Delantero, 900, 150);
    jugadores[4] = j5;
    Log.Information($"✅ {JugadoresIniciales} jugadores iniciales creados y asignados a la plantilla.");
}

void ImprimirMenu() {
    Console.WriteLine();
    Console.WriteLine("---------- ⚽ GESTÓN CD LEGANÉS ⚽ ----------");
    Console.WriteLine($"{(int)OpcionMenu.CrearJugador}.- Fichar Jugador.");
    Console.WriteLine($"{(int)OpcionMenu.VerPlantilla}.- Ver plantilla.");
    Console.WriteLine($"{(int)OpcionMenu.OrdenarPorGoles}.- Ordenar por Goles.");
    Console.WriteLine($"{(int)OpcionMenu.ListarPorPosicion}.- Listar por Posición.");
    Console.WriteLine($"{(int)OpcionMenu.ActualizarJugador}.- Actualizar Jugador.");
    Console.WriteLine($"{(int)OpcionMenu.BorrarJugador}.- Despedir Jugador.");
    Console.WriteLine($"{(int)OpcionMenu.Salir}.- Salir.");
    Console.WriteLine("-------------------------------------------");
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
            ImprimirPlantilla(plantilla, numJugadores);
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


string ValidarDato(string msg, string rgx) {
    string input;
    var isDatoOk = false;
    var regex = new Regex(rgx);
    do {
        Console.WriteLine(msg);
        input = Console.ReadLine()?.Trim() ?? "-1";
        if (regex.IsMatch(input)) {
            Log.Information($"✅ Dato {input} leído correctamente.");
            isDatoOk = true;
        } else {
            Log.Warning($"⚠️ {input} no es un dato válido para este campo.");
            Console.WriteLine("🔴  Dato introducido inválido.");
        }
    } while (!isDatoOk);
    return input;
}
