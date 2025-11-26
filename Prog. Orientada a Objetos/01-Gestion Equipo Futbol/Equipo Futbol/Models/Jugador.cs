using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
using Serilog;
namespace Equipo_Futbol.Models;

public class Jugador {
    private string _dni; // identificador
    private string _nombre;
    private int _edad;
    private int _dorsal;
    private PosicionJugador _posicion;
    private int _goles;
    
    // constructor que obliga a que estén todos los datos
    public Jugador(string dni, string nombre, int edad, int dorsal, PosicionJugador posicion, int goles) {
        var posicionInt = (int)posicion; // para usar el setter
        if (!IsDniValido(dni)) throw new ArgumentException("DNI inválido.");
        _dni = dni;
        if (!IsNombreValido(nombre)) throw new ArgumentException("Nombre inválido.");
        _nombre = nombre;
        SetEdad(edad);
        SetDorsal(dorsal);
        SetPosicion(posicionInt); 
        SetGoles(goles);
    }

    // getters
    public string GetDni() {
        return _dni;
    }
    public string GetNombre() {
        return _nombre;
    }
    public int GetEdad() {
        return _edad;
    }
    public int GetDorsal() {
        return _dorsal;
    }
    public PosicionJugador GetPosicion() {
        return _posicion;
    }
    public int GetGoles() {
        return _goles;
    }
    
    // setters
    public void SetDni(string dni) {
        if (!IsDniValido(dni)) {
            Log.Warning("⚠️ El DNI introducido es inválido.");
            Console.WriteLine("🔴  DNI inválido. Introduzca un DNI válido.");
        } else _dni = dni;
    }

    public void SetNombre(string nombre) {
        if (!IsNombreValido(nombre)) {
            Log.Warning("⚠️ El nombre introducido es inválido.");
            Console.WriteLine("🔴  Nombre inválido. Introduzca un nombre de al menos 3 letras.");
        } else _nombre = nombre;
    }
    public void SetEdad(int edad) {
        if (!IsEdadValida(edad)) {
            Log.Warning("⚠️ La edad introducida es inválida. ");
            Console.WriteLine("🔴  Edad inválida. El jugador debe tener al menos 12 años.");
        } else _edad = edad;
    }
    public void SetDorsal(int dorsal) {
        if (!IsDorsalValido(dorsal)) {
            Log.Warning("⚠️ El dorsal introducido es inválido.");
            Console.WriteLine("🔴  Dorsal inválido. Introduzca un dorsal del 1-99");
        } else _dorsal = dorsal;
    }
    public void SetPosicion(int posicion) {
        if (!IsPosicionValida(posicion)) {
            Log.Warning("⚠️ La posición introducida es inválida.");
            Console.WriteLine("🔴  Posición inválida. Introduzca una de las 4 posiciones disponibles.");
        } else _posicion = (PosicionJugador)posicion;// ...
    }
    public void SetGoles(int goles) {
        if (!IsGolesValidos(goles)) {
            Log.Warning("⚠️ El nº de goles introducido es inválido.");
            Console.WriteLine("🔴  Goles inválidos. Introduzca un número de goles superior o igual a 0.");
        } else _goles = goles;
    }
    
    // métodos de validación 
    private bool IsDniValido(string dni) {
        var regex = new Regex("^[0-9]{8}[a-zA-Z]{1}$"); // FUTURA MEJORA: no verifica que la letra sea correcta (%23)
        return regex.IsMatch(dni.ToUpper());
    }
    private bool IsNombreValido(string nombre) {
        var regex = new Regex("^[A-Za-z]{3,}$"); // al menos 3 letras
        return regex.IsMatch(nombre);
    }
    private bool IsEdadValida(int edad) {
        return edad >= 12; // edad superior 11
    }
    private bool IsDorsalValido(int dorsal) {
        return dorsal >= 1 && dorsal <= 99; // dorsal 1-99
    }
    private bool IsPosicionValida(int posicion) {
        return posicion >= 1 && posicion <= 4; // posicion entre Portero, Defensa, MedioCentro y Delantero
    }
    private bool IsGolesValidos(int goles) {
        return goles >= 0; // goles positivos
    }
    
    // imprime la información del jugador
    public override string ToString() {
        return $"DNI: {_dni}, Nombre: {_nombre}, Edad: {_edad}, Dorsal: {_dorsal}, Posición: {_posicion}, Goles: {_goles}";
    }
}