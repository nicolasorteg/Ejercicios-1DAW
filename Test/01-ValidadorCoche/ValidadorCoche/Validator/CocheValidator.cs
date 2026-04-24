using ValidadorCoche.Model;

namespace ValidadorCoche.Validator;

public class CocheValidator {

    public List<string> Validar(Coche coche) {
        var errores = new List<string>();
        
        errores.AddRange(ValidarMatricula(coche.Matricula));
        errores.AddRange(ValidarMarca(coche.Matricula));
        errores.AddRange(ValidarFechaMatriculacion(coche.FechaMatriculacion));
        
        return errores;
    }

    
    private IEnumerable<string> ValidarMatricula(string matricula) {
        throw new NotImplementedException();
    }
    
    
    private IEnumerable<string> ValidarMarca(string marca) {
        throw new NotImplementedException();
    }

    
    private IEnumerable<string> ValidarFechaMatriculacion(DateTime fechaMatriculacion) {
        throw new NotImplementedException();
    }
}