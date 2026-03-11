using System.Globalization;
using Pokemon.Dto;
using Pokemon.Models;

namespace Pokemon.Mapper;

public static class PokemonMapper {
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
    
    // limpieza ya que hay campos numericos con un guion indicando que es nulo o 0
    private static string LimpiarNum(string valor) {
        if (string.IsNullOrWhiteSpace(valor) || valor == "-" || valor == "—") 
            return "0";
        return valor.Trim();
    }

    // lo mismo pero para texto
    private static string? LimpiarTexto(string valor) {
        if (string.IsNullOrWhiteSpace(valor) || valor == "-" || valor == "—") 
            return null;
        return valor.Trim();
    }

    public static Pokemonn ToModel(this PokemonDto dto) {
        return new Pokemonn(
            int.Parse(LimpiarNum(dto.Dexnum)),
            LimpiarTexto(dto.Name) ?? "Desconocido", 
            int.Parse(LimpiarNum(dto.Generation)),
            LimpiarTexto(dto.Species) ?? "",
            LimpiarTexto(dto.Type1) ?? "Normal",
            LimpiarTexto(dto.Type2),
            double.Parse(LimpiarNum(dto.Height), InvariantCulture),
            double.Parse(LimpiarNum(dto.Weight), InvariantCulture),
            LimpiarTexto(dto.Ability1) ?? "",
            LimpiarTexto(dto.Ability2),     
            LimpiarTexto(dto.HiddenAbility),   
            int.Parse(LimpiarNum(dto.Hp)),
            int.Parse(LimpiarNum(dto.Attack)),
            int.Parse(LimpiarNum(dto.Defense)),
            int.Parse(LimpiarNum(dto.SpAtk)),
            int.Parse(LimpiarNum(dto.SpDef)),
            int.Parse(LimpiarNum(dto.Speed)),
            int.Parse(LimpiarNum(dto.Total)),
            LimpiarTexto(dto.EvYield) ?? "",
            int.Parse(LimpiarNum(dto.CatchRate)),
            int.Parse(LimpiarNum(dto.BaseFriendship)),
            int.Parse(LimpiarNum(dto.BaseExp)),
            LimpiarTexto(dto.GrowthRate) ?? "",
            LimpiarTexto(dto.EggGroup1) ?? "",
            LimpiarTexto(dto.EggGroup2),
            double.TryParse(dto.PercentMale, NumberStyles.Any, InvariantCulture, out var m) ? m : null,
            double.TryParse(dto.PercentFemale, NumberStyles.Any, InvariantCulture, out var h) ? h : null,
            int.Parse(LimpiarNum(dto.EggCycles)),
            LimpiarTexto(dto.SpecialGroup) ?? "Ordinary"
        );
    }
}