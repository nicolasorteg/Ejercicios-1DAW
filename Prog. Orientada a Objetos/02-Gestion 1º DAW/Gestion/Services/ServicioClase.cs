using Gestion.Models;
using Gestion.Utils;
using Serilog;

namespace Gestion.Services;
/*
 * Servicio para las funciones CRUD de la clase.
 */
public class ServicioClase {
    private const int PersonasIniciales = 5;
    private Persona?[] _clase = new Persona?[PersonasIniciales];
    
    public ServicioClase() {
        Utilidades.InicializarDatos(_clase);
    }


    public void CrearPersona() {
        throw new NotImplementedException();
    }

    public void VerClase() {
        Log.Debug("Imprimiendo clase...");
        Console.WriteLine("--- 💻 INTEGRANTES 1º DAW ");
        foreach (var persona in _clase)
            if (persona != null) Console.WriteLine($"- {persona}");
    }

    public void OrdenarPorNota() {
        Log.Debug("Ordenando por nota.");
        _clase = Utilidades.RedimensionarClase(ref _clase);
        Utilidades.OrdenarClase(_clase, "Nota");
        Console.WriteLine("-- CLASE ORDENADA POR NOTA:");
        foreach (var persona in _clase) 
            if (persona != null) 
                if (persona?.Tipo != Persona.TipoPersona.Profesor)
                    Console.WriteLine($"- {persona}");
    }

    public void OrdenarPorEdad() {
        Log.Debug("Ordenando por edad.");
        _clase = Utilidades.RedimensionarClase(ref _clase);
        Utilidades.OrdenarClase(_clase, "Edad");
        Console.WriteLine("-- CLASE ORDENADA POR EDAD:");
        foreach (var persona in _clase) 
            if (persona != null) Console.WriteLine($"- {persona}");
    }

    public void OrdenarPorFaltas() {
        Log.Debug("Ordenando por faltas.");
        _clase = Utilidades.RedimensionarClase(ref _clase);
        Utilidades.OrdenarClase(_clase, "Faltas");
        Console.WriteLine("-- CLASE ORDENADA POR FALTAS:");
        foreach (var persona in _clase) 
            if (persona != null) 
                if (persona?.Tipo != Persona.TipoPersona.Profesor)
                    Console.WriteLine($"- {persona}");
    }

    public void ListarPorRol() {
        Log.Debug("Listando por rol...");
        Console.WriteLine("-- 🧑‍🏫 PROFESORES");
        Utilidades.ImprimirDatosPorRol(_clase, Persona.TipoPersona.Profesor);
        Console.WriteLine("\n--  🤓   ALUMNOS");
        Utilidades.ImprimirDatosPorRol(_clase, Persona.TipoPersona.Alumno);
    }

    public void ActualizarPersona() {
        throw new NotImplementedException();
    }

    public void BorrarPersona() {
        Log.Debug("Borrando persona...");
        var dni = Utilidades.ValidarDato("- DNI de persona a eliminar: ", Utilidades.RegexId);
        var posicion = -1;
        var isInClase = Utilidades.IsDniInClase(_clase, dni, ref posicion);
        if (isInClase) {
            Console.WriteLine("✅  Persona encontrada.\n");
            
            var confirmacion = Utilidades.ValidarDato("¿Desea eliminarla? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
            if (confirmacion != "S") return;
            _clase[posicion] = null;
            
            _clase = Utilidades.RedimensionarClase(ref _clase);
            Console.WriteLine($"✅  Persona de DNI {dni} dada de baja.");
            Log.Information("✅ Persona eliminada.");
            return;
        }
        Console.WriteLine("❌  No existe ninguna persona para el DNI facilitado.");
    }
}