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
    private int _asistencias;
    
    // constructor que obliga a que estén todos los datos
    public Jugador(string dni, string nombre, int edad, int dorsal, PosicionJugador posicion, int goles, int asistencias) {
        var posicionInt = (int)posicion; // para usar el setter
        if (!IsDniValido(dni)) throw new ArgumentException("DNI inválido.");
        _dni = dni;
        if (!IsNombreValido(nombre)) throw new ArgumentException("Nombre inválido.");
        _nombre = nombre;
        SetEdad(edad);
        SetDorsal(dorsal);
        SetPosicion(posicionInt); 
        SetGoles(goles);
        SetAsistencias(asistencias);
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

    public int GetAsistencias() {
        return _asistencias;
    }
    
    // setters
    public bool SetDni(string dni) {
        if (!IsDniValido(dni)) {
            Log.Warning("⚠️ El DNI introducido es inválido.");
            Console.WriteLine("🔴  DNI inválido. Introduzca un DNI válido.");
            return false;
        }
        _dni = dni;
        return true;
    }
    public bool SetNombre(string nombre) {
        if (!IsNombreValido(nombre)) {
            Log.Warning("⚠️ El nombre introducido es inválido.");
            Console.WriteLine("🔴  Nombre inválido. Introduzca un nombre de al menos 3 letras.");
            return false;
        }
        _nombre = nombre;
        return true;
    }
    public bool SetEdad(int edad) {
        if (!IsEdadValida(edad)) {
            Log.Warning("⚠️ La edad introducida es inválida. ");
            Console.WriteLine("🔴  Edad inválida. El jugador debe tener al menos 12 años.");
            return false;
        } 
        _edad = edad;
        return true;
    }
    public bool SetDorsal(int dorsal) {
        if (!IsDorsalValido(dorsal)) {
            Log.Warning("⚠️ El dorsal introducido es inválido.");
            Console.WriteLine("🔴  Dorsal inválido. Introduzca un dorsal del 1-99");
            return false;
        }
        _dorsal = dorsal;
        return true;
    }
    public bool SetPosicion(int posicion) {
        if (!IsPosicionValida(posicion)) {
            Log.Warning("⚠️ La posición introducida es inválida.");
            Console.WriteLine("🔴  Posición inválida. Introduzca una de las 4 posiciones disponibles.");
            return false;
        }
        _posicion = (PosicionJugador)posicion;
        return true;
    }
    public bool SetGoles(int goles) {
        if (!IsParticipacionesGolValidas(goles)) {
            Log.Warning("⚠️ El nº de goles introducido es inválido.");
            Console.WriteLine("🔴  Goles inválidos. Introduzca un número de goles superior o igual a 0.");
            return false;
        }
        _goles = goles;
        return true;
    }
    public bool SetAsistencias(int asistencias) {
        if (!IsParticipacionesGolValidas(asistencias)) {
            Log.Warning("⚠️ El nº de asistencias introducido es inválido.");
            Console.WriteLine("🔴  Asistencias inválidos. Introduzca un número de asistencias superior o igual a 0.");
            return false;
        }
        _asistencias = asistencias;
        return true;
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
        return posicion >= 0 && posicion <= 3; // posicion entre Portero, Defensa, MedioCentro y Delantero
    }
    private bool IsParticipacionesGolValidas(int goles) {
        return goles >= 0; // goles positivos
    }
    
    // imprime la información del jugador
    public override string ToString() {
        return $"DNI: {_dni}, Nombre: {_nombre}, Edad: {_edad}, Dorsal: {_dorsal}, Posición: {_posicion}, Goles: {_goles}, Asistencias {_asistencias}";
    }
}