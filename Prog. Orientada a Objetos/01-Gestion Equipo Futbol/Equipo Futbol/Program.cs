// ._.
using System.Text;
using Equipo_Futbol.Enums;
using Equipo_Futbol.Models;
using Serilog;
using Equipo_Futbol.Utils;

// constantes globales
const int JugadoresIniciales = 5;
const int EdadMinima = 12;

const string RegexOpcionMenu = @"^[0-6]$"; // si se creasen más opciones o se ampliasen los campos de jugador habria que crear RegexOpcionMenuActualizar
const string RegexOpcionPosicion = @"^[1-4]$";
const string RegexDni = @"[0-9]{8}[a-zA-Z]{1}$";
const string RegexConfirmacion = @"^[SN]$";
const string RegexNombre = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ]{3,}$";
const string RegexEdad = @"^(0|[1-9]\d?)$";
const string RegexDorsal = @"^([1-9]\d?)$";
const string RegexStats = @"^\d{1,5}$";

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
    bool isDniOk;
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

    Utilidades.GuardarJugadorEnPlantilla(nuevoJugador, ref plantilla);
}

void ImprimirPlantilla(Jugador?[] plantilla) {
    Log.Debug("🔵 Imprimiendo Plantilla...");
    Console.WriteLine("-- PLANTILLA CD LEGANÉS 🥒");
    for (var i = 0; i < plantilla.Length; i++) 
        if (plantilla[i] is { } jugadorValido) Console.WriteLine($"{i+1}.- {jugadorValido}"); // si hay un jugador imprime los datos con ToString
    Console.WriteLine();
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
    Log.Debug("🔵 Imprimiendo Plantilla por posicion...");
    Utilidades.ImprimirJugadorPorPosicion(plantilla, "Portero");
    Utilidades.ImprimirJugadorPorPosicion(plantilla, "Defensa");
    Utilidades.ImprimirJugadorPorPosicion(plantilla, "Mediocentro");
    Utilidades.ImprimirJugadorPorPosicion(plantilla, "Delantero");
}

void ActualizarJugador(Jugador?[] plantilla) {
    Log.Debug("🔵 Actualizando Jugador...");

    var dni = Utilidades.ValidarDato("- Introduzca el DNI del Jugador a actualizar:", RegexDni);
    foreach (var jugador in plantilla) {
        if (jugador is { } jugadorValido) {
            if (jugadorValido.Dni == dni) {
                Console.WriteLine("✅  Jugador encontrado:");
                Console.WriteLine(jugadorValido);
                Console.WriteLine();
                Console.WriteLine("-- Dato a actualizar --");
                Utilidades.ImprimirMenuActualizar();
                var opcionElegida = int.Parse(Utilidades.ValidarDato("- N.º de Dato elegido ->", RegexOpcionMenu));
                switch (opcionElegida) {
                    case (int)OpcionActualizar.Nombre:
                        var nombre = Utilidades.ValidarDato("- Introduce el Nombre: ", RegexNombre);
                        jugadorValido.Nombre = nombre;
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Edad:
                        var edad = int.Parse(Utilidades.ValidarDato("- Introduce la Edad:", RegexEdad));
                        if (edad < EdadMinima) {
                            Console.WriteLine($"🔴  La edad mínima es de {EdadMinima} años.");
                            continue;
                        }
                        jugadorValido.Edad = edad;
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Dorsal:
                        var dorsal = int.Parse(Utilidades.ValidarDato("- Introduce el Dorsal:", RegexDorsal));   
                        jugadorValido.Dorsal = dorsal;
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Posicion:
                        Utilidades.ImprimirMenuPosicion();
                        var opcion = int.Parse(Utilidades.ValidarDato("- Introduce el número de la posición: ", RegexOpcionPosicion));
                        jugadorValido.Posicion = (Jugador.Posiciones)(opcion - 1);
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Goles:
                        var goles = int.Parse(Utilidades.ValidarDato("- Introduce los Goles:", RegexStats));
                        jugadorValido.Goles = goles;
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Asistencias:
                        var asistencias = int.Parse(Utilidades.ValidarDato("- Introduce las Asistencias:", RegexStats));
                        jugadorValido.Goles = asistencias;
                        Utilidades.MostrarConfirmacionActualizacion(jugadorValido);
                        break;
                    
                    case (int)OpcionActualizar.Salir:
                        Log.Debug("🔵 Volviendo al menú...");
                        break;
                    
                    default: // no deberia pasar nunca ya que ValidarDato se asegura de que la opcion exista en el menu
                        Log.Error("🔴  El programa ha fallado en 'ValidarDato' y no ha podido reconocer la opción introducida");
                        Console.WriteLine("❌  Opción no reconocida. Introduzca una opción de las que se muestran en el menú.");
                        break;
                }
                return;
            } 
        }
    }
    Console.WriteLine($"🔴  No existe el Jugador {dni}");
    Console.WriteLine();
    return;
}

void BorrarJugador(ref Jugador?[] plantilla) {
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