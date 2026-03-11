using Gestion.Enums;

namespace Gestion.Config;

public static class HeroesConfig {
    public const int EnergiaRestauraDescansar = 5;
    public const int EnergiaMinima = 10;
    public const int EnergiaMaxima = 200;
    
    public const int MultiplicadorNivelCalcPoder = 100;
    public const int NivelMaximo = 10;
    
    public const int DificultadMinima = 1;
    public const int DificultadMaxima = 10;
    
    public const int FuerzaMinima = 1;
    public const int FuerzaMaxima = 50;

    public const int AgilidadMinima = 1;
    public const int AgilidadMaxima = 50;
    
    public static readonly string RegexOpcionMenuPrincipal = @$"^[{(int)OpcionesMenuPrincipal.Salir}-{(int)OpcionesMenuPrincipal.ActualizarMision}]|1[0-3]$";
    public static readonly string RegexOpcionMenuOrdenacion = @$"^[{(int)OpcionObtenerHeroes.Salir}-{(int)OpcionObtenerHeroes.ObtenerPorPoder}]$";
    public static readonly string RegexOpcionMenuActualizarHeroe = @$"^[{(int)OpcionMenuActualizarHeroe.Salir}-{(int)OpcionMenuActualizarHeroe.Rareza}]$";
    public const string RegexAgilidadFuerza = @"^(?:[1-9]|[1-4][0-9]|50)$";
    public const string RegexInteligencia = @"^[0-2]$";
    public const string RegexNombre = @"^[A-Za-z\s]{2,}$";
    public const string RegexNivel = @"^([0-9]|10)$";
    public const string RegexXp = @"^\d{1,4}$";
    public const string RegexEnergia = @"^(?:[1-9][0-9]?|1[0-9]{2}|200)$";
    public const string RegexRarezas = @"^[0-4]$";
    public const string RegexTipoHeroe = @"^[1-3]$";
    public const string RegexId = @"^\d{1,}$";
    public const string MensajeConfirmacion = "- ¿Confirmar? (s/n)";
    public const string RegexConfirmacion = @"^[sSnN]$";

    public static int[] UmbralesNivel = [10, 25, 50, 100, 150, 225, 325, 450, 600, 800, 0];
    public static int[] ValoresRealesRarezas = [100, 200, 300, 400, 500];
    public static int[] ValoresRealesInteligencias = [10, 30, 50];

    public const int XpGanadaMision = 100;
    public const int EnergiaPerdidaMision = 25;
}