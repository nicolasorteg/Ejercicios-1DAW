using System.Text;
using System.Text.RegularExpressions;
using Pokemon.Dto;
using Pokemon.Mapper;
using Pokemon.Models;

namespace Pokemon.Storage;

public class PokemonCsvStorage : IPokemonStorage {

    public PokemonCsvStorage() {
        InitStorage();
    }

    public IEnumerable<Pokemonn> Cargar(string path) {
        
        // validación path
        if (!File.Exists(path)) return [];
        
        try {
            return File.ReadLines(path, Encoding.UTF8)
                .Skip(1) // skip cabecera
                .Select(linea => Regex.Split(linea, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                .Select(campos => new PokemonDto(
                    campos[0].Trim('\"'), 
                    campos[1], 
                    campos[2], 
                    campos[3], 
                    campos[4],
                    campos[5], 
                    campos[6], 
                    campos[7], 
                    campos[8], 
                    campos[9],
                    campos[10], 
                    campos[11], 
                    campos[12], 
                    campos[13], 
                    campos[14],
                    campos[15], 
                    campos[16], 
                    campos[17], 
                    campos[18], 
                    campos[19],
                    campos[20], 
                    campos[21], 
                    campos[22], 
                    campos[23], 
                    campos[24],
                    campos[25], 
                    campos[26], 
                    campos[27], 
                    campos[28]
                ).ToModel()).ToList();
        }
        catch (Exception ex) {
            WriteLine($"Error al cargar el CSV: {ex.Message}");
            throw;
        }
    }

    private static void InitStorage() {
        if (!Directory.Exists("Data")) {
            Directory.CreateDirectory("Data");
        }
    }
}