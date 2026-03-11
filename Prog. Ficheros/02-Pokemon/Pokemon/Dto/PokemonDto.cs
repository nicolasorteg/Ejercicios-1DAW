namespace Pokemon.Dto;

public record PokemonDto(
    string Dexnum,          // nº de pokédex
    string Name,            // nombre
    string Generation,      // generación
    string Type1,           // tipo 1
    string Type2,           // tipo 2
    string Species,         // especie
    string Height,          // altura
    string Weight,          // peso
    string Ability1,        // habilidad 1
    string Ability2,        // habilidad 2
    string HiddenAbility,   // habilidad oculta
    string Hp,              // vida 
    string Attack,          // ataque
    string Defense,         // defensa
    string SpAtk,           // ataque especial
    string SpDef,           // defensa especial
    string Speed,           // velocidad
    string Total,           // suma total de estadísticas
    string EvYield,         // puntos de esfuerzo
    string CatchRate,       // ratio de captura
    string BaseFriendship,  // amistad base
    string BaseExp,         // experiencia base
    string GrowthRate,      // ratio de crecimiento
    string EggGroup1,       // grupo de huevo 1
    string EggGroup2,       // grupo de huevo 2
    string PercentMale,     // % macho
    string PercentFemale,   // % hembra
    string EggCycles,       // ciclos de huevo
    string SpecialGroup     // grupo especial (Legendario, etc.)
);