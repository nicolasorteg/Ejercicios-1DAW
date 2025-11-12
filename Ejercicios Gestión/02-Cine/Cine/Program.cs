using System.Text;
using System.Text.RegularExpressions;
using Cine.Enums;
using Cine.Structs;

// ---------- constantes globales ----------
// menu
const int OpcionMenuVerSala = 1;
const int OpcionMenuComprarEntrada = 2;
const int OpcionMenuDevolverEntrada = 3;
const int OpcionMenuVerRecaudacion = 4;
const int OpcionMenuVerInforme = 5;
const int OpcionMenuSalir = 6;

// precio por butaca
const double PrecioButaca = 6.50; 

// dimensiones límite de la sala
const int FilasMinimas = 4;
const int FilasMaximas = 7;
const int ColumnasMinimas = 5;
const int ColumnasMaximas = 9;

const int NumeroButacasRotas = 3;


// ---------- inicio del main ----------

Console.OutputEncoding = Encoding.UTF8; // emojis en terminal

DimensionesSala dimensiones = new DimensionesSala();
Console.WriteLine("--- 🎞️ GESTIÓN SALA CINEMAD 🎞️ ---");
Console.WriteLine("Introduzca a continuación las dimensiones de la sala.");
Console.WriteLine($"Fila -> Mín {FilasMinimas} Max {FilasMaximas} || Columna -> Mín {ColumnasMinimas} Max {ColumnasMaximas}");
dimensiones = ValidarArgumentosEntrada(args);

EjecutarMenu(ref dimensiones);

return;
// ---------- fin del main ----------

void EjecutarMenu(ref DimensionesSala dimensiones) {
    var opcionElegida = 0;
    
    // variables para el informe
    double recaudacion = 0.0;
    var numEntradasVendidas = 0;
    var numButacasLibres = dimensiones.Filas * dimensiones.Columnas - NumeroButacasRotas;
    var numButacasOcupadas = 0;
    
    PosicionButaca posicion = new PosicionButaca();
    // creacion de la sala con , ya que puede ser una matriz rectangular
    EstadoButaca[,] sala = new EstadoButaca[dimensiones.Filas, dimensiones.Columnas];
    ConfigurarSala(sala, ref dimensiones);

    do {
        Console.WriteLine("--🍿 Sala 1 de CINEMAD 🍿--");
        Console.WriteLine("---------------------------");
        Console.WriteLine($"{OpcionMenuVerSala}.- 💺 Ver asientos.");
        Console.WriteLine($"{OpcionMenuComprarEntrada}.- 🎟 Comprar entrada.");
        Console.WriteLine($"{OpcionMenuDevolverEntrada}.- 😣 Devolver entrada.");
        Console.WriteLine($"{OpcionMenuVerRecaudacion}.- 💰 Recaudación sala.");
        Console.WriteLine($"{OpcionMenuVerInforme}.- 📊 Informe de sala.");
        Console.WriteLine($"{OpcionMenuSalir}.- 💨 Salir.");
        Console.WriteLine("---------------------------");
        opcionElegida = ValidarOpcion("Opción elegida: ");

        switch (opcionElegida) {
            case OpcionMenuVerSala:
                ImprimirSala(sala, ref dimensiones);
                break;
            case OpcionMenuComprarEntrada:
                ComprarEntrada(sala, ref recaudacion, ref numEntradasVendidas, ref numButacasLibres, ref numButacasOcupadas, ref posicion);
                break;
            case OpcionMenuDevolverEntrada:
                DevolverEntrada(sala, ref recaudacion, ref numEntradasVendidas, ref numButacasLibres, ref numButacasOcupadas, ref posicion);
                break;
            case OpcionMenuVerRecaudacion:
                MostrarRecaudacion(recaudacion);
                break;
            case OpcionMenuVerInforme:
                MostrarInforme(numEntradasVendidas, numButacasLibres, recaudacion, ref dimensiones);
                break;
            case OpcionMenuSalir:
                Console.WriteLine("Ha sido un placer, ¡vuelve pronto! 😋");
                break;
            default:
                Console.WriteLine($"❌ Opción inválida. Introduce una de las {OpcionMenuSalir} posibles.");
                break;
        }
    } while (opcionElegida is not OpcionMenuSalir);
}


void ImprimirSala(EstadoButaca[,] sala, ref DimensionesSala dimensiones) {
    
    Console.WriteLine("---------------------------");
    Console.WriteLine("");
    // 3 espacios necesarios para compensar las letras
    Console.Write("   "); 

    // escritura de los numeros de columna
    for (int j = 0; j < dimensiones.Columnas; j += 1) {
        Console.Write(" " + (j + 1) + "   ");
    }
    Console.WriteLine("");

    string letras = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

    // recorremos la sala
    for (int i = 0; i < sala.GetLength(0); i++) {
        for (int j = 0; j < sala.GetLength(1); j++) {
            // si estas al inicio de una fila lo primero la letra de esa fila
            if (j == 0) {
                Console.Write(letras[i] + " ");
            }
            if (sala[i,j] == EstadoButaca.Disponible) {
                Console.Write("[🟢] ");
            } else if (sala[i,j] == EstadoButaca.Ocupada) {
                Console.Write("[🔴] ");
            } else if (sala[i,j] == EstadoButaca.FueraDeServicio) {
                Console.Write("[🚫] ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


void ComprarEntrada(EstadoButaca[,] sala, ref double recaudacion, ref int numEntradasVendidas, ref int numButacasLibres, ref int numButacasOcupadas, ref PosicionButaca posicion) {

    bool isButadaValid = false;
    
    Console.WriteLine("---------------------------");
    Console.WriteLine($"El precio por entrada es de {PrecioButaca}€");

    do {
        ValidarPosicionEntrada("Introduzca la cordenada de la butaca que desee (Ej: B:3): ", ref dimensiones, ref posicion);

        if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.Disponible) {
            sala[posicion.Fila, posicion.Columna] = EstadoButaca.Ocupada; // marcamos como ocupada
            isButadaValid = true;
            recaudacion += PrecioButaca;
            numEntradasVendidas++;
            numButacasOcupadas++;
            numButacasLibres--;
            Console.WriteLine("✅ Butaca comprada con éxito. Ir a ver sala para ver los cambios.");
            
        } else if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.Ocupada) {
            Console.WriteLine("⚠️ La butaca que ha seleccionado está ocupada.");
            
        } else if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.FueraDeServicio) {
            Console.WriteLine("⚠️ La butaca que ha seleccionado está fuera de servicio.");
        }
    } while (!isButadaValid);
}

void DevolverEntrada(EstadoButaca[,] sala, ref double recaudacion, ref int numEntradasVendidas, ref int numButacasLibres, ref int numButacasOcupadas, ref PosicionButaca posicion) {

    bool isButadaValid = false;

    Console.WriteLine("---------------------------");
    do {
        ValidarPosicionEntrada("Introduzca la cordenada de la butaca que desee (Ej: B:3): ", ref dimensiones, ref posicion);

        if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.Ocupada) {
            sala[posicion.Fila, posicion.Columna] = EstadoButaca.Disponible; // marcamos como libre
            isButadaValid = true;
            recaudacion -= PrecioButaca;
            numEntradasVendidas--;
            numButacasOcupadas--;
            numButacasLibres++;
            Console.WriteLine("✅ Butaca vendida con éxito. Ir a ver sala para ver los cambios.");
            
        } else if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.Disponible) {
            Console.WriteLine("⚠️ La butaca que ha seleccionado está libre.");
            
        } else if (sala[posicion.Fila, posicion.Columna] == EstadoButaca.FueraDeServicio) {
            Console.WriteLine("⚠️ La butaca que ha seleccionado está fuera de servicio.");
        } 
    } while (!isButadaValid);
}


void MostrarRecaudacion(double recaudacion) {
    
    double numEntradas = recaudacion / PrecioButaca;
    Console.WriteLine("---------------------------");
    Console.WriteLine($"Entradas vendidas: {Convert.ToInt32(numEntradas)}🎟 || Total recaudado: {recaudacion}€");
    Console.WriteLine();
}


void MostrarInforme(int numEntradasVendidas, int numButacasLibres, double recaudacion, ref DimensionesSala dimensiones) {
    
    int numButacas = dimensiones.Filas * dimensiones.Columnas;

    double ocupacion = Math.Round(Convert.ToDouble(numEntradasVendidas) / (numButacas - NumeroButacasRotas) * 100, 2) ;

    Console.WriteLine("---🎬 INFORME CINEMAD 🎬---");
    Console.WriteLine($"Entradas vendidas: {numEntradasVendidas}🎟");
    Console.WriteLine($"Butacas libres: {numButacasLibres}🟢");
    Console.WriteLine($"Butacas no disponibles: {NumeroButacasRotas}🚫");
    Console.WriteLine($"Ocupación: {ocupacion}%");
    Console.WriteLine($"Recaudación total: {recaudacion}€");
    Console.WriteLine("---------------------------");
}


// ---------- funciones auxiliares ----------
DimensionesSala ValidarDimensiones(string prompt, ref DimensionesSala dimensiones) {
    
    bool isDimensionOk = false;
    var regexDimension = new Regex(@"^[" + FilasMinimas + "-" + FilasMaximas + "]:[" + ColumnasMinimas + "-" + ColumnasMaximas + "]$");

    do {
        Console.WriteLine(prompt);
        var input = Console.ReadLine()?.Trim() ?? "";

        if (regexDimension.IsMatch(input)) {
            ExtraerDatos(input, ref dimensiones);
            Console.WriteLine($"✅ Dimensiones aceptadas. Filas: {dimensiones.Filas} || Columnas: {dimensiones.Columnas}");
            isDimensionOk = true;
        } else {
            Console.WriteLine("❌ Formato inválido. Use F:C (Ej 6:9).");
        }
    } while (!isDimensionOk);

    return dimensiones;
}
void ExtraerDatos(string input, ref DimensionesSala dimensiones) {
    string[] dimensionesSeleccionadas = input.Split(":");
    dimensiones.Filas = int.Parse(dimensionesSeleccionadas[0]);
    dimensiones.Columnas = int.Parse(dimensionesSeleccionadas[1]);
}

int ValidarOpcion(string prompt) {
    bool isOpcionOk = false;
    var regexOpcion = new Regex(@"^[1-" + OpcionMenuSalir + "]$");
    string input;

    do {
        Console.WriteLine(prompt);
        input = Console.ReadLine()?.Trim() ?? "";

        if (regexOpcion.IsMatch(input)) {
            isOpcionOk = true;
            Console.WriteLine($"✅ Opción elegida: {input}");
        } else {
            Console.WriteLine($"❌ Opción no reconocida. Introduzca una de las {OpcionMenuSalir} posibles.");
        }
    } while (!isOpcionOk);
    
    return int.Parse(input);
}

void ValidarPosicionEntrada(string prompt, ref DimensionesSala dimensiones, ref PosicionButaca posicion) {

    bool isPosicionOk = false;
    string letras = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
    char letraMaxima = letras[dimensiones.Filas - 1];

    var regexPosicionEntrada = new Regex(@$"^[A-{letraMaxima}]:[1-{dimensiones.Columnas}]");

    do {
        Console.WriteLine(prompt);
        var input = Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (regexPosicionEntrada.IsMatch(input)) {
            // leemos y asignamos los datos
            string[] partes = input.Split(":");
            
            posicion.Columna = int.Parse(partes[1]) - 1;
            posicion.Fila = letras.IndexOf(partes[0]);
            isPosicionOk = true;
            Console.WriteLine($"✅ Posición {posicion.Fila}:{posicion.Columna} leída correctamente.");
        } else {
            Console.WriteLine("❌ Error en la entrada de datos. Por favor, introduza la letra y la columna correspondiente (Fila:Columna)");
        }
    } while (!isPosicionOk);
}

void ConfigurarSala(EstadoButaca[,] sala, ref DimensionesSala dimensiones) {
    var random = new Random();
    int butacasRotas = NumeroButacasRotas;
    
    for (int i = 0; i < dimensiones.Filas; i++) {
        for (int j = 0; j < dimensiones.Columnas; j++) {
            sala[i, j] = EstadoButaca.Disponible;
        }
    }


    // bucle para cambiar los 0 por 2 donde la butaca este fuera de servicio
    while (butacasRotas > 0) {

        int filaButacaRota = random.Next(dimensiones.Filas);
        int columnaButacaRota = random.Next(dimensiones.Columnas);

        if (sala[filaButacaRota, columnaButacaRota] == EstadoButaca.Disponible) {
            sala[filaButacaRota, columnaButacaRota] = EstadoButaca.FueraDeServicio;
            butacasRotas -= 1;
        }
    }
}

DimensionesSala ValidarArgumentosEntrada(string[] args) {
    int numFilas;
    int numColumnas;
    
    if (args.Length != 2) {
        Console.WriteLine("❌ Argumentos inválidos. Debe itnroducir -> filas:X columnas:Y");
        return ValidarDimensiones("Introduce las dimensiones (F:C): ", ref dimensiones);

    } else {
        var filas = args[0].Split(":");
        if (filas.Length is not 2 || !int.TryParse(filas[1], out numFilas) || numFilas <= FilasMinimas || numFilas > FilasMaximas) {
            Console.WriteLine($"❌ Argumentos inválidos. El argumento {args[0]} no es válido. (Ej: filas:5)");
            return ValidarDimensiones("Introduce las dimensiones (F:C): ", ref dimensiones);
        }
        var columnas = args[1].Split(":");
        if (columnas.Length is not 2 || !int.TryParse(columnas[1], out numColumnas) || numColumnas <= ColumnasMinimas || numColumnas > ColumnasMaximas) {
            Console.WriteLine($"❌ Argumentos inválidos. El argumento {args[1]} no es válido. (Ej: columnas:5)");
            return ValidarDimensiones("Introduce las dimensiones (F:C): ", ref dimensiones);
        }
    }
    return new DimensionesSala {
        Filas = numFilas,
        Columnas = numColumnas
    };
}