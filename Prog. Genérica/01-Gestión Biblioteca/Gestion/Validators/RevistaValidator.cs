using Gestion.Models;

namespace Gestion.Validators;

public class RevistaValidator : IRevistaValidator {
    public Revista Validate(Revista revista) {
        return revista;
    }
}