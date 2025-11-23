// ._.
using System.Globalization;
using System.Text;
using Piscina.Enums;
using Piscina.Structs;
using Serilog;
using static System.Console;

// zona de constantes
const int DimensionDefault = 15;
const int VidasDefault = 2;
const int SanosDefault = 10;
const int BacteriasDefault = 30;
const int TiempoDefault = 60;
const int ProbMatar = 25;

var random = Random.Shared;
Log.Logger = new LoggerConfiguration().WriteTo.File("logs/log.txt").MinimumLevel.Debug().CreateLogger();
var localeEs = new CultureInfo("es-ES");
Title = "Piscina Infectada";
OutputEncoding = Encoding.UTF8;
Clear();

Main(args);

WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main(string[] args) {
    Log.Debug("🔵 Iniciando Main...");
    
    // config simulacion
    WriteLine("---------- ⚙️ CONFIGURANDO SALA ⚙️ ----------");
    var configuracion = ValidarArgs(args);
    ImprimirConfig(configuracion);
    
    // creacion piscina y colocamos al socorrista
    var piscinaFront = new TipoCelda[configuracion.Dimension, configuracion.Dimension];
    var piscinaBack = new TipoCelda[configuracion.Dimension, configuracion.Dimension];
    InicializarDatos(piscinaFront, configuracion);
    
    // coloca a nizar el socorrista
    var posicionInicialSocorrista = new Posicion {
        Fila = random.Next(piscinaFront.GetLength(0)),
        Columna = random.Next(piscinaFront.GetLength(1))
    };
    piscinaFront[posicionInicialSocorrista.Fila, posicionInicialSocorrista.Columna] = TipoCelda.Nizar;
    
    // creamos el informe
    var informe = new Informe {
        TiempoTranscurrido = 0,
        BebesInfectados = 0,
        BebesRescatados = 0,
        NumBacterias = configuracion.NumBacterias,
        NumBebes = configuracion.NumSanos,
        PosicionNizar = posicionInicialSocorrista
    };
    
    // comienza la simulación
    Simular(piscinaFront, piscinaBack, informe, configuracion);
}

void Simular(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack, Informe informe, Configuracion configuracion) {
    Log.Debug("🔵 Comenzando simulación...");
    var isSimulacionActiva = false;

    do {
        Clear();
        WriteLine($"---------- 🦠 Tiempo: {informe.TiempoTranscurrido}s 🦠 ----------");
        WriteLine();

        // muestra el tablero
        ImprimirPiscina(piscinaFront);


        

        isSimulacionActiva = true;
    } while (!isSimulacionActiva);
}

void ImprimirPiscina(TipoCelda[,] piscinaFront) {
    for (int i = 0; i < piscinaFront.GetLength(0); i++) {
        Write("| ");
        for (int j = 0; j < piscinaFront.GetLength(1); j++) {
            if (piscinaFront[i, j] == TipoCelda.Agua)
                Write("💧");
            else if (piscinaFront[i, j] == TipoCelda.Bebé)
                Write("👶");
            else if (piscinaFront[i, j] == TipoCelda.Bacteria)
                Write("🦠");
            else
                Write("🛟");
        }
        Write("   |");
        WriteLine();
    }
}

void InicializarDatos(TipoCelda[,] piscinaFront, Configuracion configuracion) {
    Log.Debug("🔵 Colocando bebés y bacterias...");
    // coloca los bebés y las bacetrias
    int numBebes = configuracion.NumSanos;
    int numBacterias = configuracion.NumBacterias;
    while (numBebes > 0 || numBacterias > 0) {
        // si hay bebés por colocar coloca, sino pasa a colocar las bacterias
        if (numBebes > 0) {
            int filaRandom = random.Next(piscinaFront.GetLength(0));
            int columnaRandom = random.Next(piscinaFront.GetLength(1));
            piscinaFront[filaRandom, columnaRandom] = TipoCelda.Bebé;
            numBebes--;
            Log.Information($"✅ Bebé colocado en {filaRandom}:{columnaRandom}");
        } else {
            int filaRandom = random.Next(piscinaFront.GetLength(0));
            int columnaRandom = random.Next(piscinaFront.GetLength(1));
            piscinaFront[filaRandom, columnaRandom] = TipoCelda.Bacteria;
            numBacterias--;
            Log.Information($"✅ Bactera colocada en {filaRandom}:{columnaRandom}");
        }
    }
}

void ImprimirConfig(Configuracion config) {
    Log.Debug("🔵 Imprimiendo config...");
    WriteLine($"⬛  Dimensiones -> {config.Dimension}x{config.Dimension}");
    WriteLine($"👶 Bebés Sanos -> {config.NumSanos}");
    WriteLine($"🦠 Bacterias -> {config.NumBacterias}");
    WriteLine($"💕 Vidas Bebés -> {config.Vidas}");
    WriteLine($"⌛  Segundos Simulación -> {config.Tiempo}");
    WriteLine($"🗡️Probabilidad Bebé mata Bacteria -> {config.ProbMatar}");
    WriteLine("---------------------------------------------");
}


Configuracion ValidarArgs(string[] args) {

    Log.Information("✅ Config default creada.");
    var configuracion = new Configuracion {
        Dimension = DimensionDefault,
        Vidas = VidasDefault,
        NumSanos = SanosDefault,
        NumBacterias = BacteriasDefault,
        Tiempo = TiempoDefault,
        ProbMatar = ProbMatar
    };

    // validacion posicion
    string claveDimension = "dimension";
    var stringDimension = BuscarValorArgs(args, claveDimension);
    if (stringDimension == null || !int.TryParse(stringDimension, out int dimension) || dimension < 5) {
        WriteLine("🔴  Entrada de datos por argumento errónea. Configurando simulación con dimensiones default.");
        Log.Error("🔴 Dimension introducida incorrectamente. Se simulará con las dimensiones por defecto.");
    } else {
        configuracion.Dimension = dimension;
        Log.Information($"✅ Simulando con una piscina {configuracion.Dimension}x{configuracion.Dimension}");
    }
    
    // validacion vidas
    string claveVidas = "vidas";
    var stringVidas = BuscarValorArgs(args, claveVidas);
    if (stringVidas == null || !int.TryParse(stringVidas, out int vidas) || vidas < 1) {
        WriteLine("🔴  Entrada de datos por argumento errónea. Configurando simulación con vidas default.");
        Log.Error("🔴 Vidas introducidas incorrectamente. Se simulará con las vidas por defecto.");
    } else {
        configuracion.Vidas = vidas;
        Log.Information($"✅ Simulando con un bebé con {configuracion.Vidas} vidas.");
    }
    
    // validacion sanos
    string claveSanos = "sanos";
    var stringSanos = BuscarValorArgs(args, claveSanos);
    if (stringSanos == null || !int.TryParse(stringSanos, out int sanos) || sanos < 1) {
        WriteLine("🔴  Entrada de datos por argumento errónea. Configurando simulación con sanos default.");
        Log.Error("🔴 Sanos introducidos incorrectamente. Se simulará con los sanos por defecto.");
    } else {
        configuracion.NumSanos = sanos;
        Log.Information($"✅ Simulando con {configuracion.NumSanos} infectados.");
    }
    
    // validacion bacterias
    string claveBacterias = "bacterias";
    var stringBacterias = BuscarValorArgs(args, claveBacterias);
    if (stringBacterias == null || !int.TryParse(stringBacterias, out int bacterias) || bacterias < 1) {
        WriteLine("🔴  Entrada de datos por argumento errónea. Configurando simulación con bacterias default.");
        Log.Error("🔴 Infectados introducidos incorrectamente. Se simulará con las bacterias por defecto.");
    } else {
        configuracion.NumBacterias = bacterias;
        Log.Information($"✅ Simulando con {configuracion.NumBacterias} infectados.");
    }
    
    // validacion tiempo
    string claveTiempo = "tiempo";
    var stringTiempo = BuscarValorArgs(args, claveTiempo);
    if (stringTiempo == null || !int.TryParse(stringTiempo, out int tiempo) || tiempo < 1) {
        WriteLine("🔴  Entrada de datos por argumento errónea. Configurando simulación con el tiempo default.");
        Log.Error("🔴 Tiempo introducido incorrectamente. Se simulará con los segundos por defecto.");
    } else {
        configuracion.Tiempo = tiempo;
        Log.Information($"✅ La simulación durará {configuracion.Tiempo}s");
    }

    return configuracion;
}

string? BuscarValorArgs(string[] args, string claveEsperada) {
    foreach (var arg in args) {
        var partes = arg.Split(":");
        var claveActual = partes[0].Trim().ToLower();
        if (claveActual == claveEsperada) return partes[1];
    }
    return null;
}