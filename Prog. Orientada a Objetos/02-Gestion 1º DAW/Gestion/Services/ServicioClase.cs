using Gestion.Models;

namespace Gestion.Services;
/*
 * Servicio para las funciones CRUD de la clase.
 */
public class ServicioClase {
    private const int PersonasIniciales = 5;
    private Persona?[] clase = new Persona?[PersonasIniciales];
    
    public ServicioClase() {
        InicializarDatos();
    }

    private static void InicializarDatos() {
        clase[0] = new Persona("12345678A");
        clase[1] = new Persona("Carlos", 19, 7.8, 3, "Alumno");
        clase[2] = new Persona("Marta", 18, 8.5, 0, "Alumno");
        clase[3] = new Persona("Jorge", 20, 6.1, 5, "Alumno");
        clase[4] = new Persona("Sofía", 19, 9.7, 2, "Subdelegada");
    }

    public static void CrearPersona() {
        throw new NotImplementedException();
    }

    public static void VerClase() {
        throw new NotImplementedException();
    }

    public static void OrdenarPorNota() {
        throw new NotImplementedException();
    }

    public static void OrdenarPorEdad() {
        throw new NotImplementedException();
    }

    public static void OrdenarPorFaltas() {
        throw new NotImplementedException();
    }

    public static void ListarPorRol() {
        throw new NotImplementedException();
    }

    public static void ActualizarPersona() {
        throw new NotImplementedException();
    }

    public static void BorrarPersona() {
        throw new NotImplementedException();
    }
}