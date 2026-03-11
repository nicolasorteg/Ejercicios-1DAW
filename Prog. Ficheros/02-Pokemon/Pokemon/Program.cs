using System.Text;
using Pokemon.Storage;

// daw's template
Title = "Procesamiento CSV Pokemon";
OutputEncoding = Encoding.UTF8;
Clear();
Main();
WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main() {
    var rutaCsv = Path.Combine("Data", "pokemon_data.csv");
    var storage = new PokemonCsvStorage();
    var pokemons = storage.Cargar(rutaCsv).ToList();

    WriteLine($"🔥 TOTAL POKÉMON CARGADOS: {pokemons.Count}\n");

    
    // ---------- PRIMEROS 5 POKEMON
    var primeros5 = 
        pokemons.Take(5);
    
    WriteLine(">>> LOS 5 PRIMEROS");
    foreach (var p in primeros5) WriteLine(p);
    
    // ---------- LOS 5 POKEMON MÁS PESADOS
    var masPesados =
        pokemons
            .OrderByDescending(p => p.Peso)
            .Take(5);
    
    WriteLine("\n>>> LOS 5 MÁS PESADOS");
    foreach (var p in masPesados) WriteLine($"{p} | Peso: {p.Peso} kg");
    
    // ---------- LOS 5 POKEMON MÁS LIGEROS
    var masLigeros =
        pokemons
            .OrderBy(p => p.Peso)
            .Take(5);
    
    WriteLine("\n>>> LOS 5 MÁS LIGEROS");
    foreach (var p in masLigeros) WriteLine($"{p} | Peso: {p.Peso} kg");
    
    // ---------- LOS 5 POKEMON MÁS ALTOS
    var masAltos =
        pokemons
            .OrderByDescending(p => p.Altura)
            .Take(5);
    
    WriteLine("\n>>> LOS 5 MÁS ALTOS");
    foreach (var p in masAltos) WriteLine($"{p} | Peso: {p.Peso} kg");
    
    // ---------- LOS 5 POKEMON MÁS BAJITOS
    var masBajitos = 
        pokemons
            .OrderBy(p => p.Altura)
            .Take(5);

    WriteLine("\n>>> LOS 5 MÁS BAJITOS");
    foreach (var p in masBajitos) WriteLine($"{p} | Altura: {p.Altura} m");
    
    
    // ---------- POKEMON TIPO FUEGO
    var tipoFuego =
        pokemons
            .Where(p => p.Tipo1 == "Fire" || p.Tipo2 == "Fire");
    
    WriteLine("\n>>> TIPO FUEGO");
    foreach (var p in tipoFuego) WriteLine($"{p} | Tipo 1: {p.Tipo1,-9} | Tipo 2: {p.Tipo2} ");
    
    
    // ---------- POKEMON CON HABILIDAD RUN AWAY
    var conRunAway =
        pokemons
            .Where(p => p.Habilidad1 == "Run Away" || p.Habilidad2 == "Run Away");
    
    WriteLine("\n>>> HABILIDAD RUN AWAY");
    foreach (var p in conRunAway) WriteLine($"{p} | Habilidad 1: {p.Habilidad1,-15} | Habilidad 2: {p.Habilidad2}");
    
    // ---------- LOS 10 POKEMON CON MÁS ATAQUE
    var topAtaque = 
        pokemons
        .OrderByDescending(p => p.Ataque)
        .Take(10);
    
    WriteLine("\n>>> TOP 10 ATAQUE");
    foreach (var p in topAtaque) WriteLine($"{p} | Ataque: {p.Ataque}");
    
    // ---------- LOS 10 POKEMON CON MÁS DEFENSA
    var topDefensa = 
        pokemons
        .OrderByDescending(p => p.Defensa)
        .Take(10);
    
    WriteLine("\n>>> TOP 10 DEFENSA");
    foreach (var p in topDefensa) WriteLine($"{p} | Defensa: {p.Defensa}");
    
    // ---------- LOS 10 POKEMON MÁS RAPIDOS
    var topVelocidad = 
        pokemons
            .OrderByDescending(p => p.Velocidad)
            .Take(10);
    
    WriteLine("\n>>> TOP 10 VELOCIDAD");
    foreach (var p in topVelocidad) WriteLine($"{p} | Velocidad: {p.Velocidad}");
}
