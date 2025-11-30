using Gestion.Enums;
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
        _clase = Utilidades.EliminarNulos(ref _clase);
        Utilidades.OrdenarClase(_clase, "Nota");
        Console.WriteLine("-- CLASE ORDENADA POR NOTA:");
        foreach (var persona in _clase) 
                if (persona?.Tipo != Persona.TipoPersona.Profesor)
                    Console.WriteLine($"- {persona}");
    }

    public void OrdenarPorEdad() {
        Log.Debug("Ordenando por edad.");
        _clase = Utilidades.EliminarNulos(ref _clase);
        Utilidades.OrdenarClase(_clase, "Edad");
        Console.WriteLine("-- CLASE ORDENADA POR EDAD:");
        foreach (var persona in _clase) 
            if (persona != null) Console.WriteLine($"- {persona}");
    }

    public void OrdenarPorFaltas() {
        Log.Debug("Ordenando por faltas.");
        _clase = Utilidades.EliminarNulos(ref _clase);
        Utilidades.OrdenarClase(_clase, "Faltas");
        Console.WriteLine("-- CLASE ORDENADA POR FALTAS:");
        foreach (var persona in _clase) 
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
        Log.Debug("Actualizando personas...");
        // validación persona a actualizar
        var dni = Utilidades.ValidarDato("- Introduce el DNI de la persona a actualizar:", Utilidades.RegexDni);
        var posicion = -1;
        var isInClase = Utilidades.IsDniInClase(_clase, dni, ref posicion);
        if (!isInClase) {
            Console.WriteLine($"🔴  No se ha encontrado a ninguna persona de DNI {dni}");
            return;  
        }
        var personaActualizacion = _clase[posicion];
        
        Utilidades.ImprimirMenuActualizacion();
        var opcionElegida = int.Parse(Utilidades.ValidarDato("- Introduzca la opción: ", Utilidades.RegexOpcionMenuActualizacion));
        switch (opcionElegida) {
            case (int)OpcionMenuActualizacion.Salir: break;
            case (int)OpcionMenuActualizacion.Nombre:
                var nombre = Utilidades.PedirNombre();
                var confirmacionNombre = Utilidades.ValidarDato($"¿Desea actualizar el nombre a {nombre}? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
                if (confirmacionNombre != "S") return;
                personaActualizacion?.Nombre = nombre;
                Console.WriteLine($"✅ Nombre actualizado correctamente:\n{personaActualizacion}");
                break;
            
            case (int)OpcionMenuActualizacion.Edad:
                var edad = Utilidades.PedirEdad();
                var confirmacionEdad = Utilidades.ValidarDato($"¿Desea actualizar la edad a {edad}? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
                if (confirmacionEdad != "S") return;
                personaActualizacion?.Edad = edad;
                Console.WriteLine($"✅ Edad actualizada correctamente:\n{personaActualizacion}");
                break;
            
            case (int)OpcionMenuActualizacion.Faltas:
                var faltas = Utilidades.PedirFaltas();
                var confirmacionFaltas = Utilidades.ValidarDato($"¿Desea actualizar las faltas a {faltas}? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
                if (confirmacionFaltas != "S") return;
                if (personaActualizacion != null && personaActualizacion.Datos.HasValue) {
                    var datos = personaActualizacion.Datos.Value; 
                    datos.Faltas = faltas; 
                    personaActualizacion.Datos = datos;
                }
                Console.WriteLine($"✅ Faltas actualizadas correctamente:\n{personaActualizacion}");
                break;
            
            case (int)OpcionMenuActualizacion.Retrasos:
                var retrasos = Utilidades.PedirRetrasos();
                var confirmacionRetrasos = Utilidades.ValidarDato($"¿Desea actualizar el nombre a {retrasos}? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
                if (confirmacionRetrasos != "S") return;
                if (personaActualizacion != null && personaActualizacion.Datos.HasValue) {
                    var datos = personaActualizacion.Datos.Value; 
                    datos.Retrasos = retrasos; 
                    personaActualizacion.Datos = datos;
                }
                Console.WriteLine($"✅ Retrasos actualizados correctamente:\n{personaActualizacion}");
                break;
            
            case (int)OpcionMenuActualizacion.NotaProg:
                var nota = Utilidades.PedirNotaProg();
                var confirmacionNota = Utilidades.ValidarDato($"¿Desea actualizar la nota a {nota}? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
                if (confirmacionNota != "S") return;
                if (personaActualizacion != null && personaActualizacion.Datos.HasValue) {
                    var datos = personaActualizacion.Datos.Value; 
                    datos.NotaProgramacion = nota; 
                    personaActualizacion.Datos = datos;
                }
                Console.WriteLine($"✅ Nota actualizada correctamente:\n{personaActualizacion}");
                break;
        }
    }

    public void BorrarPersona() {
        Log.Debug("Borrando persona...");
        var dni = Utilidades.ValidarDato("- DNI de persona a eliminar: ", Utilidades.RegexDni);
        var posicion = -1;
        var isInClase = Utilidades.IsDniInClase(_clase, dni, ref posicion);
        if (isInClase) {
            Console.WriteLine("✅  Persona encontrada.\n");
            
            var confirmacion = Utilidades.ValidarDato("¿Desea eliminarla? (s/n)", Utilidades.RegexConfirmacion).ToUpper();
            if (confirmacion != "S") return;
            _clase[posicion] = null;
            
            _clase = Utilidades.EliminarNulos(ref _clase);
            Console.WriteLine($"✅  Persona de DNI {dni} dada de baja.");
            Log.Information("✅ Persona eliminada.");
            return;
        }
        Console.WriteLine("❌  No existe ninguna persona para el DNI facilitado.");
    }
}