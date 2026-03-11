namespace Pokemon.Models;

public class Pokemonn(
    int numeroPokedex,
    string nombre,
    int generacion,
    string especie,
    string tipo1,
    string? tipo2,
    double altura,
    double peso,
    string habilidad1,
    string? habilidad2,
    string? habilidadOculta,
    int vida,
    int ataque,
    int defensa,
    int ataqueEspecial,
    int defensaEspecial,
    int velocidad,
    int poderTotal,
    string puntosEsfuerzo,
    int ratioCaptura,
    int amistadBase,
    int experienciaBase,
    string crecimiento,
    string grupoHuevo1,
    string? grupoHuevo2,
    double? porcentajeMacho,
    double? porcentajeHembra,
    int ciclosHuevo,
    string grupoEspecial
) {
    public int NumeroPokedex { get; } = numeroPokedex;
    public string Nombre { get; } = nombre;
    public int Generacion { get; } = generacion;
    public string Especie { get; } = especie;
    public string Tipo1 { get; } = tipo1;
    public string? Tipo2 { get; } = tipo2; 
    public double Altura { get; } = altura;
    public double Peso { get; } = peso;
    public string Habilidad1 { get; } = habilidad1;
    public string? Habilidad2 { get; } = habilidad2;
    public string? HabilidadOculta { get; } = habilidadOculta; 
    public int Vida { get; } = vida;
    public int Ataque { get; } = ataque;
    public int Defensa { get; } = defensa;
    public int AtaqueEspecial { get; } = ataqueEspecial;
    public int DefensaEspecial { get; } = defensaEspecial;
    public int Velocidad { get; } = velocidad;
    public int PoderTotal { get; } = poderTotal;
    public string PuntosEsfuerzo { get; } = puntosEsfuerzo;
    public int RatioCaptura { get; } = ratioCaptura;
    public int AmistadBase { get; } = amistadBase;
    public int ExperienciaBase { get; } = experienciaBase;
    public string Crecimiento { get; } = crecimiento;
    public string GrupoHuevo1 { get; } = grupoHuevo1;
    public string? GrupoHuevo2 { get; } = grupoHuevo2; // Puede ser nulo
    public double? PorcentajeMacho { get; } = porcentajeMacho; // Puede ser nulo
    public double? PorcentajeHembra { get; } = porcentajeHembra; // Puede ser nulo
    public int CiclosHuevo { get; } = ciclosHuevo;
    public string GrupoEspecial { get; } = grupoEspecial;


    public override string ToString() => 
        $"ID: {NumeroPokedex:D4} | Nombre: {Nombre,-15} | Tipo: {Tipo1,-9} | Poder Total: {PoderTotal,-7}";
}