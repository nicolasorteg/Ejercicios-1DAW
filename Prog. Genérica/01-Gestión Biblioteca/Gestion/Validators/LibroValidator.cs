using Gestion.Models;

namespace Gestion.Validators;

public class LibroValidator : ILibroValidator {
    
    public Libro Validate(Libro libro) {
        return libro;
    }
}