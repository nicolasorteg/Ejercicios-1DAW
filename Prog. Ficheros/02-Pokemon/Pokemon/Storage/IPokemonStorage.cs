using Pokemon.Models;
namespace Pokemon.Storage;

public interface IPokemonStorage {
    IEnumerable<Pokemonn> Cargar(string path);
}