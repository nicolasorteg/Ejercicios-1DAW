using System.Text;
using System.Text.RegularExpressions;

// --- constantes para gstionar el menú principal ---
const int OpcionMenuJugar = 1;
const int OpcionMenuGestionarSaldo = 2;
const int OpcionMenuVerPremios = 3;
const int OpcionMenuSalir = 4;
// --- constantes para gestionar saldo ---
const int OpcionGestionSumar = 1;
const int OpcionGestionRetirar = 2;
const int OpcionGestionVerSaldo = 3;
const int OpcionGestionSalir = 4;
// --- constantes para la tirada ---
const int MultiplicadorJackpot = 10;
const int MultiplicadorTresIguales = 3;
const double MultiplicadorDosIguales = 1.5;
const int NumeroJackpot = 7;


// ------ inicio del main ------
Console.OutputEncoding = Encoding.UTF8; // para ver los emojis en terminal

double saldo;

ValidarDecimal("Saldo inicial: ", out saldo);

Console.WriteLine("------- 🎰 TRAGAPERRAS 🎰 -------");
EjecutarMenu(ref saldo);

return;
// ------ fin del main ------


void EjecutarMenu(ref double saldo) {

    int opcionElegida = 0;
    do {
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"{OpcionMenuJugar}.- 📍 Tirar de la Tragaperras.");
        Console.WriteLine($"{OpcionMenuGestionarSaldo}.- 💸 Gestionar saldo.");
        Console.WriteLine($"{OpcionMenuVerPremios}.- 🤑 Ver premios.");
        Console.WriteLine($"{OpcionMenuSalir}.- 💨 Salir.");
        Console.WriteLine("---------------------------------");
        
        opcionElegida = ValidarEntero("Introduce la opción: ",ref opcionElegida);

        switch (opcionElegida) {
            
            case OpcionMenuJugar:
                SimularTirada(ref saldo);
                break;
            case OpcionMenuGestionarSaldo:
                GestionarSaldo(ref saldo);
                break;
            case OpcionMenuVerPremios:
                VerPremios();
                break;
            case OpcionMenuSalir:
                Console.WriteLine("😔 Saliendo del programa...");
                break;
            default:
                Console.WriteLine("⚠️ Opción no reconocida.");
                break;
        }
    } while (opcionElegida != OpcionMenuSalir);
}


// -------------------- SIMULACION TRAGAPERRAS --------------------
void SimularTirada(ref double saldo) {

    double saldoApostado;
    var rand = new Random();

    do {
        Console.WriteLine("Recuerde apostar menos de lo que tiene 😉");
        ValidarDecimal("❔ Dinero a apostar: ", out saldoApostado);
    } while (saldoApostado > saldo);

    saldo -= saldoApostado;

    // almacenamos los numeros de la tirada en un vector
    var numeros = new int[3];
    var num1 = rand.Next(0,10);
    numeros[0] = num1;
    var num2 = rand.Next(0,10);
    numeros[1] = num2;
    var num3 = rand.Next(0,10);
    numeros[2] = num3;

    MostrarTirada(numeros);
    Console.WriteLine("---------------------------------");
    saldo = CalcularSaldo(numeros, ref saldo, saldoApostado);
    Console.WriteLine($"Saldo tras la tirada = {saldo}€");
    

    if (saldo <= 0) Console.WriteLine("😫 Saldo nulo, debe añadir para poder continuar."); GestionarSaldo(ref saldo);
}

void MostrarTirada(int[] numeros) {
    
    Console.WriteLine("Tiramos de la ruleta y...");
    Thread.Sleep(1000);
    Console.WriteLine(numeros[0]);
    Thread.Sleep(1000);
    Console.WriteLine(numeros[1]);
    Thread.Sleep(1000);
    Console.WriteLine("🥁🥁🥁");
    Thread.Sleep(2000);
    Console.WriteLine(numeros[2]);
}

double CalcularSaldo(int[] numeros, ref double saldo, double saldoApostado) {
    
    // condición jackpot
    if (numeros[0] == NumeroJackpot && numeros[1] == NumeroJackpot && numeros[2] == NumeroJackpot) return saldo += saldoApostado * MultiplicadorJackpot;
    // condición 3 iguales pero no jackpot
    if (numeros[0] == numeros[1] && numeros[1] == numeros[2]) return saldo += saldoApostado * MultiplicadorTresIguales;
    // condición solo 2 iguales
    if (numeros[0] == numeros[1] || numeros[1] == numeros[2] || numeros[0] == numeros[2]) return saldo += saldoApostado * MultiplicadorDosIguales;
    
    // llega aquí implica que ningún número coincidia, por lo que ha perdido
    return saldo;
}


// -------------------- GESTIÓN SALDO --------------------
void GestionarSaldo(ref double saldo) {

    int opcionElegida = 0;
    do {
        Console.WriteLine("---- GESTIÓN SALDO ----");
        Console.WriteLine($"{OpcionGestionSumar}.- ➕  Añadir saldo.");
        Console.WriteLine($"{OpcionGestionRetirar}.- ➖  Retirar saldo.");
        Console.WriteLine($"{OpcionGestionVerSaldo}.- 👀 Ver saldo.");
        Console.WriteLine($"{OpcionGestionSalir}.- 💨 Volver al menú.");

        opcionElegida = ValidarEntero("Opción elegida", ref opcionElegida);
        
        switch (opcionElegida) {
            case OpcionGestionSumar:
                SumarSaldo(ref saldo);
                break;
            case OpcionGestionRetirar:
                RetirarSaldo(ref saldo);
                break;
            case OpcionGestionVerSaldo:
                Console.WriteLine($"- Saldo actual -> {saldo}€");
                break;
            case OpcionGestionSalir:
                Console.WriteLine("Volviendo al menú...");
                break;
            default:
                Console.WriteLine("⚠️ Opción no reconocida.");
                break;
        }
    } while (opcionElegida != OpcionGestionSalir);
}

void SumarSaldo(ref double saldo) {
    
    double saldoParaAñadir;
    ValidarDecimal("- Saldo a añadir:  ", out saldoParaAñadir);
    saldo += saldoParaAñadir;
    Console.WriteLine($"✅  Saldo tras la suma: {saldo}€");
}

void RetirarSaldo(ref double saldo) {
    
    double saldoParaRetirar;
    bool isSaldoValid = false;
    do {
        ValidarDecimal("- Saldo a retirar:  ", out saldoParaRetirar);

        if (saldoParaRetirar >= saldo) {
            Console.WriteLine("❌  Para poder continuar el saldo debe ser mayor a 0€. Por favor, introduzca otra cantidad a retirar.");
        } else {
            saldo -= saldoParaRetirar;
            Console.WriteLine($"✅  Saldo tras la retirada: {saldo}€");
            isSaldoValid = true;
        }
    } while (!isSaldoValid);
}


// -------------------- VER PREMIOS --------------------
void VerPremios() {
    
    Console.WriteLine("-- PREMIOS --");
    Console.WriteLine("-👎 Si no coincide ningún número pierdes el dinero apostado.");
    Console.WriteLine("-🙄️ Si sólo coinciden 2 números, sean cuales sean recuperas un x1.5 de lo apostado.");
    Console.WriteLine("-😉 Si coinciden los 3 números recuperas un x3 de lo apostado excepto si los números que coinciden son 777.");
    Console.WriteLine("-🤑 Si coinciden  7 7 7 recuperas un x10 de lo apostado."); // jackpot
}


// -------------------- FUNCIONES AUXILIARES --------------------
/*
 * Esta función no hacee uso de expresiones regulares para practicar el TryParse.
 * Además, en vez de devolver el saldo con un return, se pasa por referencia usando el token out
 * que nos obliga a asignarle un valor.
 */
void ValidarDecimal(string message, out double saldo) {
    
    bool isSaldoValid = false;

    do {
        Console.WriteLine(message);
        
        if (double.TryParse(Console.ReadLine(), out saldo) && saldo > 0.0) {
            Console.WriteLine($"✅  Saldo introducido: {saldo}€");
            isSaldoValid = true;
        } else {
            Console.WriteLine("❌  Saldo inválido. Por favor, introduzca un saldo superior a 0€");
        }
    } while (!isSaldoValid);
}

int ValidarEntero(string message, ref int opcion) {

    bool isOpcionValid = false;
    var regexOpcion = new Regex(@"^[1-9]$");

    do {
        Console.WriteLine(message);
        // si no se introduce valor se le asigna un espacio en blanco que dara fallo a intentar castear
        var input = Console.ReadLine()?.Trim() ?? "";

        if (regexOpcion.IsMatch(input)) {
            opcion = int.Parse(input!); // aquí ya es seguro el casting ya que ha pasado por la expresion regular
            isOpcionValid = true;
        } else {
            Console.WriteLine($"❌  Opción no reconocida. Introduzca una de las opciones posibles.");
        }
    } while (!isOpcionValid);
    
    return opcion;
}