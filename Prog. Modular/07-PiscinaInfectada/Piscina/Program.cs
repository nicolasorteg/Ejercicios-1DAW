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
    Simular(piscinaFront, piscinaBack, ref informe, configuracion);
    
    // imprime el informe final
    ImprimirInformeFinal(informe, configuracion);
}

void Simular(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack, ref Informe informe, Configuracion configuracion) {
    Log.Debug("🔵 Comenzando simulación...");
    var isSimulacionActiva = true;

    do {
        WriteLine();
        WriteLine($"---------- 🦠 Tiempo: {informe.TiempoTranscurrido}s 🦠 ----------");
        WriteLine();

        // muestra el tablero
        ImprimirPiscina(piscinaFront);
        
        // se copia la matriz del front en la del back para coherencia T+1
        Array.Copy(piscinaFront, piscinaBack, piscinaFront.Length);
        
        // se lee del front y se escribe en el back. Doble Buffer
        SimularSegundo(piscinaFront, piscinaBack, ref informe, configuracion);

        (piscinaFront, piscinaBack) = (piscinaBack, piscinaFront);
        
        informe.TiempoTranscurrido++;
        if (informe.TiempoTranscurrido >= configuracion.Tiempo || informe.NumBebes <= 0) {
            Log.Information("🔴 Fin de la simulación por tiempo o falta de bebés.");
            isSimulacionActiva = false; // sale del bucle
        }
        // pausa de medio segundo para visualizar
        Thread.Sleep(500);
    } while (isSimulacionActiva);
    
    WriteLine();
    WriteLine($"---------- 🦠 Tiempo: {informe.TiempoTranscurrido}s (FINAL) 🦠 ----------");
    WriteLine();
    ImprimirPiscina(piscinaFront);
    
    Log.Information("🔴 Fin de la simulación.");
}

void SimularSegundo(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack, ref Informe informe, Configuracion configuracion) {
    
    Log.Debug($"🔵 Simulano segundo {informe.TiempoTranscurrido}");
    
    // primero se mueve a nizar y luego el resto de celdas
    MoverNizar(piscinaFront, piscinaBack, ref informe, configuracion);
    
    for (int i = 0; i < piscinaFront.GetLength(0); i++) {
        for (int j = 0; j < piscinaFront.GetLength(1); j++) {
            if (piscinaFront[i, j] == TipoCelda.Bebé) {
                MoverBebe(piscinaFront, piscinaBack, i, j, informe, configuracion);
            } else if (piscinaFront[i, j] == TipoCelda.Bacteria) {
                MoverBacteria(piscinaFront, piscinaBack, i, j, ref informe, configuracion);
            } else if (piscinaFront[i, j] == TipoCelda.Nizar) continue; 
        }
    }
}

void MoverBebe(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack, int fila, int columna, Informe informe, Configuracion configuracion) {
    
    // comprueba que no haya sido resctado por nizar
    if (piscinaBack[fila, columna] != TipoCelda.Bebé) {
        return;
    }

    var nuevaPosicion = ObtenerNuevaPosicion(piscinaFront, fila, columna);
    int nuevaFila = nuevaPosicion.Fila;
    int nuevaColumna = nuevaPosicion.Columna;
    
    var celdaDestino = piscinaFront[nuevaFila, nuevaColumna];

    if (celdaDestino == TipoCelda.Bacteria) {
        
        int tirada = random.Next(1, 101);
        // 50/50, si es por debajo de 50 mata a la bacteria y se posiciona ahi, si es por encima se convierte en bacteria
        if (tirada <= configuracion.ProbMatar) {
            informe.NumBacterias--;
            piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Bebé;
            piscinaBack[fila, columna] = TipoCelda.Agua;
            WriteLine($"👼 Beb mata a Bacteria en {nuevaFila},{nuevaColumna}");
            Log.Information($"✅ Bebé mata Bacteria en {nuevaFila}:{nuevaFila}");
        } else {
            informe.BebesInfectados++;
            informe.NumBebes--;
            informe.NumBacterias++;
            piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Bacteria;
            piscinaBack[fila, columna] = TipoCelda.Agua;
            WriteLine($"🦠  Bacteria infecta al bebé {fila}:{columna}");
            Log.Information($"✅ Bacteria infecta bebé en {nuevaFila}:{nuevaColumna}");
        }
    } else if (celdaDestino == TipoCelda.Agua) {
        piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Bebé;
        piscinaBack[fila, columna] = TipoCelda.Agua;
        Log.Information($"✅ Bebé se mueve a {nuevaFila}:{nuevaColumna}");
        
    } else if (celdaDestino == TipoCelda.Bebé || celdaDestino == TipoCelda.Nizar){
        piscinaBack[fila, columna] = TipoCelda.Bebé;    
        Log.Information($"✅ Bebé se queda en {fila}:{columna} debido a colisión.");
    } 
}

void MoverBacteria(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack,  int fila, int columna, ref Informe informe, Configuracion configuracion) {
    
    // si ha sido matada por un bebé o por nizar
    if (piscinaBack[fila, columna] != TipoCelda.Bacteria) {
        return;
    }
    
    var nuevaPosicion = ObtenerNuevaPosicion(piscinaFront, fila, columna);
    int nuevaFila = nuevaPosicion.Fila;
    int nuevaColumna = nuevaPosicion.Columna;
    
    if (fila == nuevaFila && columna == nuevaColumna) {
        return;
    }
    
    var celdaDestino = piscinaFront[nuevaFila, nuevaColumna];

    if (celdaDestino == TipoCelda.Bebé) {
        
        int tirada = random.Next(1, 101);
        // 50/50, si es por debajo de 50 mata a la bacteria y se posiciona ahi, si es por encima se convierte en bacteria
        if (tirada <= configuracion.ProbMatar) {
            informe.NumBacterias--;
            piscinaBack[fila, columna] = TipoCelda.Agua;
            WriteLine($"👼  Bebé mata Bacteria en {nuevaFila}:{nuevaFila}");
            Log.Information($"✅ Bebé mata Bacteria en {nuevaFila}:{nuevaFila}");
        } else {
            informe.BebesInfectados++;
            informe.NumBebes--;
            informe.NumBacterias++;
            piscinaBack[fila, columna] = TipoCelda.Agua;
            piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Bacteria;
            WriteLine($"🦠  Bacteria infecta bebé en {nuevaFila}:{nuevaFila}");
            Log.Information($"✅ Bacteria infecta bebé en {nuevaFila}:{nuevaColumna}");
        }
        
    } else if (celdaDestino == TipoCelda.Agua) {
        piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Bacteria;
        piscinaBack[fila, columna] = TipoCelda.Agua;
        Log.Information($"✅ Bacteria se mueve a {nuevaFila}:{nuevaColumna}");
        
    } else if (celdaDestino == TipoCelda.Nizar) {
        informe.NumBacterias--;
        piscinaBack[fila, columna] = TipoCelda.Agua;
        WriteLine("🛟  La bacteria se desplaza a la posición de Nizar y es eliminada.");
        Log.Information($"✅ Bacteria asesinada por Nizar en {nuevaFila}:{nuevaColumna}");
        
    } else if (celdaDestino == TipoCelda.Bacteria) {
        piscinaBack[fila, columna] = TipoCelda.Bacteria;
        Log.Information($"✅ Bacteria se queda en {fila}:{columna} debido a colisión con otra bacteria");
    }
}

void MoverNizar(TipoCelda[,] piscinaFront, TipoCelda[,] piscinaBack, ref Informe informe, Configuracion configuracion) {
    int fila = informe.PosicionNizar.Fila;
    int columna = informe.PosicionNizar.Columna;
    
    var nuevaPosicion = ObtenerNuevaPosicion(piscinaFront, fila, columna);
    int nuevaFila = nuevaPosicion.Fila;
    int nuevaColumna = nuevaPosicion.Columna;
    
    // donde estaba el socorrista se pone agua
    piscinaBack[fila, columna] = TipoCelda.Agua;
    
    var celdaDestino = piscinaFront[nuevaFila, nuevaColumna];

    if (celdaDestino == TipoCelda.Bacteria) {
        informe.NumBacterias--;
        WriteLine("🛟  La bacteria se desplaza a la posición de Nizar y es eliminada.");
        Log.Information($"✅ Nizar elimina Bacteria en {nuevaFila}:{nuevaColumna}.");
        
    } else if (celdaDestino == TipoCelda.Bebé) {
        informe.BebesRescatados++;
        informe.NumBebes--;
        WriteLine($"🛟 Nizar rescata al bebé {nuevaFila},{nuevaColumna})");
        Log.Information($"✅ Nizar rescata al bebé de {nuevaFila}:{nuevaColumna}.");
    } else {
        Log.Information($"✅ Nizar se mueve a {nuevaFila}:{nuevaColumna}.");
    }
    
    piscinaBack[nuevaFila, nuevaColumna] = TipoCelda.Nizar;
    informe.PosicionNizar = nuevaPosicion;
}

Posicion ObtenerNuevaPosicion(TipoCelda[,] piscina, int filaActual, int columnaActual) {
    
    int dimension = piscina.GetLength(0);
    Posicion[] posiblesMovimientos = new Posicion[8];
    int contadorMovimientosValidos = 0; 

    for (var i = -1; i <= 1; i++) {
        for (var j = -1; j <= 1; j++) {

            // se salta la posicion actual
            if (i == 0 && j == 0) continue;

            var nuevaPosicion = new Posicion {
                Fila = filaActual + i,
                Columna = columnaActual + j
            };
            
            if (nuevaPosicion.Fila >= 0 && 
                nuevaPosicion.Fila < dimension && 
                nuevaPosicion.Columna >= 0 && 
                nuevaPosicion.Columna < dimension) {
                
                posiblesMovimientos[contadorMovimientosValidos] = nuevaPosicion;
                contadorMovimientosValidos++;
            }
        }
    }
    
    if (contadorMovimientosValidos == 0) {
        return new Posicion { Fila = filaActual, Columna = columnaActual };
    }
    
    int index = random.Next(contadorMovimientosValidos);
    return posiblesMovimientos[index];
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
        Write("    |");
        WriteLine();
    }
}

void InicializarDatos(TipoCelda[,] piscinaFront, Configuracion configuracion) {
    Log.Debug("🔵 Colocando bebés y bacterias...");
    // coloca los bebés y las bacetrias
    int numBebes = configuracion.NumSanos;
    int numBacterias = configuracion.NumBacterias;
    
    // inicializa todo a agua
    for (int i = 0; i < piscinaFront.GetLength(0); i++) {
        for (int j = 0; j < piscinaFront.GetLength(1); j++) {
            piscinaFront[i, j] = TipoCelda.Agua;
        }
    }
    
    while (numBebes > 0 || numBacterias > 0) {
        int filaRandom = random.Next(piscinaFront.GetLength(0));
        int columnaRandom = random.Next(piscinaFront.GetLength(1));
        // si hay bebés por colocar coloca, sino pasa a colocar las bacterias
        if (piscinaFront[filaRandom, columnaRandom] == TipoCelda.Agua) {
            if (numBebes > 0) {
                piscinaFront[filaRandom, columnaRandom] = TipoCelda.Bebé;
                numBebes--;
                Log.Information($"✅ Bebé colocado en {filaRandom}:{columnaRandom}");
            } else if (numBacterias > 0) {
                piscinaFront[filaRandom, columnaRandom] = TipoCelda.Bacteria;
                numBacterias--;
                Log.Information($"✅ Bacteria colocada en {filaRandom}:{columnaRandom}");
            }
        }
    }
}

void ImprimirConfig(Configuracion config) {
    Log.Debug("🔵 Imprimiendo config...");
    WriteLine($"⬛  Dimensiones -> {config.Dimension}x{config.Dimension}");
    WriteLine($"👶 Bebés Sanos -> {config.NumSanos}");
    WriteLine($"🦠 Bacterias -> {config.NumBacterias}");
    WriteLine($"⌛  Segundos Simulación -> {config.Tiempo}");
    WriteLine($"🗡️Probabilidad Bebé mata Bacteria -> {config.ProbMatar}");
    WriteLine("---------------------------------------------");
}


Configuracion ValidarArgs(string[] args) {

    Log.Information("✅ Config default creada.");
    var configuracion = new Configuracion {
        Dimension = DimensionDefault,
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

void ImprimirInformeFinal(Informe informe, Configuracion configuracion) {
    WriteLine();
    WriteLine("----- INFORME FINAL -----");
    WriteLine();
    WriteLine("--- 👶 ESTADÍSTICAS DE BEBÉS ---");
    WriteLine($"- Bebés Infectados: {informe.BebesInfectados}");
    WriteLine($"- Bebés Supervivientes: {informe.NumBebes}");
    WriteLine();
    WriteLine("--- 🦠 ESTADÍSTICAS DE BACTERIAS ---");
    WriteLine($"- Bacterias creadas por infección: {informe.BebesInfectados}");
    WriteLine($"- Bacterias Supervivientes: {informe.NumBacterias}");
    WriteLine();
    WriteLine("--- 🛟 DESEMPEÑO DEL SOCORRISTA ---");
    WriteLine($"- Posición Final de Nizar: ({informe.PosicionNizar.Fila}, {informe.PosicionNizar.Columna})");
    WriteLine($"- Total de Rescates: {informe.BebesRescatados}");
}