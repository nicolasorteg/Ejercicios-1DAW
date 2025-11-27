using System.Text.RegularExpressions;
using Equipo_Futbol.Enums;
namespace Equipo_Futbol.Models;

public class Jugador {
    public string Dni {
        get;
        set => field = !IsDniValido(value) ? throw new ArgumentException("El DNI introducido es inválido.") : value;
    }
    public string Nombre {
        get;
        set => field = !IsNombreValido(value) ? throw new ArgumentException("El Nombre introducido es inválido.") : value;
    }
    public int Edad {
        get;
        set => field = value < 12 ? throw new ArgumentOutOfRangeException(nameof(value), "La Edad introducida es inválida.") : value;
    }
    public int Dorsal {
        get;
        set => field = value < 1 || value > 99? throw new ArgumentOutOfRangeException(nameof(value), "El Dorsal introducido es inválido.") : value;
    }
    public PosicionJugador Posicion { get; set; }
    public int Goles {
        get;
        set => field = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "El N.º de Goles introducido es inválido.") : value;
    }
    public int Asistencias {
        get;
        set => field = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "El N.º de Asistencias introducido es inválido.") : value;
    }
    
    // constructores
    public Jugador(string dni, string nombre, int edad, int dorsal, PosicionJugador posicion, int goles, int asistencias) {
        this.Dni = dni;
        this.Nombre = nombre;
        this.Edad = edad;
        this.Dorsal = dorsal;
        this.Posicion = posicion;
        this.Goles = goles;
        this.Asistencias = asistencias;
    }
    public Jugador() {
        this.Dni = string.Empty;
        this.Nombre = string.Empty; 
    }
    
    // métodos de validación 
    private static bool IsDniValido(string dni) {
        var regex = new Regex("^[0-9]{8}[a-zA-Z]{1}$"); // FUTURA MEJORA: no verifica que la letra sea correcta (%23)
        return regex.IsMatch(dni.ToUpper());
    }
    private static bool IsNombreValido(string nombre) {
        var regex = new Regex("^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ]{3,}$"); // al menos 3 letras
        return regex.IsMatch(nombre);
    }
    
    // imprime la información del jugador
    public override string ToString() {
        return $"DNI: {Dni}, Nombre: {Nombre}, Edad: {Edad}, Dorsal: {Dorsal}, Posición: {Posicion}, Goles: {Goles}, Asistencias {Asistencias}";
    }
}