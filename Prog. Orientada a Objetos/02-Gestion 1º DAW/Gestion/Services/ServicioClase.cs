using Gestion.Models;
using Serilog;

namespace Gestion.Services;
/*
 * Servicio para las funciones CRUD de la clase.
 */
public class ServicioClase {
    private const int PersonasIniciales = 5;
    private Persona?[] _clase = new Persona?[PersonasIniciales];
    
    public ServicioClase() {
        InicializarDatos();
    }

    private void InicializarDatos() {
        Log.Debug("Inicializando Personas...");
        _clase[0] = new Persona { Dni="12345678A", Nombre="Laura Martínez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=1,Retrasos=0}};
        _clase[1] = new Persona { Dni="98765432B", Nombre="Carlos Pérez", Edad=19, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=3,Retrasos=1}};
        _clase[2] = new Persona { Dni="11223344C", Nombre="Marta Gómez", Edad=18, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=0,Retrasos=2}};
        _clase[3] = new Persona { Dni="22334455D", Nombre="Sergio López", Edad=20, Tipo=Persona.TipoPersona.Alumno, Datos=new DatosAcademicos{Faltas=2,Retrasos=0}};
        _clase[4] = new Persona { Dni="55667788E", Nombre="Jorge Ruiz", Edad=45, Tipo=Persona.TipoPersona.Profesor, Datos=null};
    }


    public void CrearPersona() {
        throw new NotImplementedException();
    }

    public void VerClase() {
        Log.Debug("Imprimiendo clase...");
        Console.WriteLine("--- 💻 INTEGRANTES 1º DAW ");
        foreach (var persona in _clase)
            if (persona != null) Console.WriteLine($"- {persona}");
        Console.WriteLine();
    }

    public void OrdenarPorNota() {
        throw new NotImplementedException();
    }

    public void OrdenarPorEdad() {
        throw new NotImplementedException();
    }

    public void OrdenarPorFaltas() {
        throw new NotImplementedException();
    }

    public void ListarPorRol() {
        throw new NotImplementedException();
    }

    public void ActualizarPersona() {
        throw new NotImplementedException();
    }

    public void BorrarPersona() {
        throw new NotImplementedException();
    }
}