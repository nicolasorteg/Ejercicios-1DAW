using Funko.Repositories;
using Funko.Validators;

namespace Funko.Services;

public class FunkoService(FunkoRepository repository, FunkoValidator validator) {
    public FunkoRepository Repository { get; } = repository;
    public FunkoValidator Validator { get; } = validator;
}