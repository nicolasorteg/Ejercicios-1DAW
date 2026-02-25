using Dir.Configuration;
using Dir.Validators;

Console.WriteLine("Iniciando comando Dir con Validación...");

// instancia validator
var validator = new ArgsValidator();
var errores = validator.Validar(args);

// comprobacion errores
if (errores.Any()) {
    Console.WriteLine("Se han encontrado errores de validación:");
    foreach (var error in errores) Console.WriteLine($"- {error}");
    return; 
}

// mapea los datos a config
DirConfiguration.MapearArgumentosAConfig(args);

// comando
DirService.Run();
return;