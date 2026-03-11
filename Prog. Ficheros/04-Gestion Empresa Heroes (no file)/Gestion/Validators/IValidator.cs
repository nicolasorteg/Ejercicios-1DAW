namespace Gestion.Validators;

public interface IValidator<in T> {
    IEnumerable<string> Validar(T entity);
}