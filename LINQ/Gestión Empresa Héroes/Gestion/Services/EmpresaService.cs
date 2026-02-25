using Gestion.Config;
using Gestion.Exceptions;
using Gestion.Models;
using Gestion.Repositories;
using Gestion.Validators;

namespace Gestion.Services;

public class EmpresaService(IHeroeRepository heroeRepository, IMisionRepository misionRepository, IValidator<Heroe> heroeValidator, IValidator<Mision> misionValidator): IEmpresaService {
    
    /// <inheritdoc cref="IEmpresaService.SaveHeroe" />
    public Heroe SaveHeroe(Heroe h) {
        
        var errores = heroeValidator.Validar(h);
        if (errores.Any()) throw new HeroeExceptions.Validation(errores);
        
        return heroeRepository.Create(h) ?? throw new HeroeExceptions.AlreadyExists(h.Id);
    }
    
    /// <inheritdoc cref="IEmpresaService.GetAllHeroes" />
    public IEnumerable<Heroe> GetAllHeroes() => heroeRepository.GetAll();
    
    /// <inheritdoc cref="IEmpresaService.GetHeroeById" />
    public Heroe GetHeroeById(int id) => heroeRepository.GetById(id) ?? throw new HeroeExceptions.NotFound(id);
    
    /// <inheritdoc cref="IEmpresaService.UpdateHeroe" />
    public Heroe UpdateHeroe(int id, Heroe h) {
        
        var errores = heroeValidator.Validar(h);
        if (errores.Any()) throw new HeroeExceptions.Validation(errores);

        return heroeRepository.Update(id, h) ?? throw new HeroeExceptions.NotFound(id);
    }
    
    /// <inheritdoc cref="IEmpresaService.DeleteHeroe" />
    public Heroe DeleteHeroe(int id) => heroeRepository.Delete(id) ?? throw new HeroeExceptions.NotFound(id);
    
    /// <inheritdoc cref="IEmpresaService.GetAllOrderByLevel" />
    public IEnumerable<Heroe> GetAllOrderByLevel() => 
        heroeRepository.GetAll()
            .OrderByDescending(h => h.Nivel)
            .ThenByDescending(h => h.Experiencia);
    
    /// <inheritdoc cref="IEmpresaService.GetAllOrderByPower" />
    public IEnumerable<Heroe> GetAllOrderByPower() =>
        heroeRepository.GetAll()
            .OrderByDescending(h => h.CalcularPoder())
            .ThenByDescending(h => h.Nivel);

    /// <inheritdoc cref="IEmpresaService.DescansarHeroe" />
    public void DescansarHeroe(int id) {
        var h = GetHeroeById(id);
        if (h.Energia >= HeroesConfig.EnergiaMaxima) {
            throw new HeroeExceptions.VidaExcesiva();
        }
        heroeRepository.DescansarHeroe(h);
    }
    
    /// <inheritdoc cref="IEmpresaService.GetPoderHeroe" />
    public int GetPoderHeroe(int id) {
        var heroe = GetHeroeById(id);
        return heroeRepository.CalcularPoder(heroe);
    }
    
    // ------------- misiones

    public Mision SaveMision(Mision m) {
        var errores = misionValidator.Validar(m);
        if (errores.Any()) throw new HeroeExceptions.Validation(errores);
        
        return misionRepository.Create(m) ?? throw new HeroeExceptions.AlreadyExists(m.Id);
    }
    
    public IEnumerable<Mision> GetAllMisiones() => misionRepository.GetAll();
    
    public IEnumerable<Mision> GetAllMisionesOrderByDificultad() => 
        misionRepository.GetAll()
            .OrderByDescending(m => m.Dificultad);
    
    public IEnumerable<Mision> GetMisionesPendientes() => 
        misionRepository.GetAll()
            .Where(m => m.Estado == Mision.Estados.Pendiente);
    
    public Mision GetMisionById(int id) => 
        misionRepository.GetById(id) ?? throw new HeroeExceptions.NotFound(id);
    
    public Mision UpdateMision(int id, Mision m) {
        var errores = misionValidator.Validar(m);
        if (errores.Any()) throw new HeroeExceptions.Validation(errores);
        
        return misionRepository.Update(id, m) ?? throw new HeroeExceptions.NotFound(id);
    }
    
    public Mision DeleteMision(int id) => 
        misionRepository.Delete(id) ?? throw new HeroeExceptions.NotFound(id);
    
    public void AsignarHeroeAMision(int heroeId, int misionId) {
        var heroe = GetHeroeById(heroeId); 
        var mision = GetMisionById(misionId);
        
        mision.AñadirHeroe(heroe);
        
        misionRepository.Update(misionId, mision);
    }
    
    public void SimularMision(int misionId) {
        var mision = GetMisionById(misionId);
        
        // breve validacion
        if (mision.Estado == Mision.Estados.Completada) { WriteLine("⚠️ La misión ya ha sido completada."); return; }
        if (!mision.Lineas.Any()) { WriteLine("❌ No hay héroes asignados a la misión."); return; }
        
        // calculo poder equipo
        var poderEquipo = mision.Lineas.Sum(l => l.Heroe.CalcularPoder());
        
        var umbralExito = mision.Dificultad * 50; 
        
        WriteLine($"⚔️ Simulación: {mision.Nombre}");
        WriteLine($"📊 Poder Equipo: {poderEquipo} | Dificultad Requerida: {umbralExito}");

        if (poderEquipo >= umbralExito) {
            WriteLine("✅ ¡ÉXITO TOTAL!");
            mision.Estado = Mision.Estados.Completada;

            foreach (var h in mision.Lineas.Select(linea => linea.Heroe)) {
                h.SumarExperiencia(HeroesConfig.XpGanadaMision);
                heroeRepository.Update(h.Id, h);
            }
        }
        else {
            WriteLine("❌ ¡FRACASO! Los héroes regresan heridos y cansados.");
            foreach (var h in mision.Lineas.Select(linea => linea.Heroe)) {
                h.Energia -= HeroesConfig.EnergiaPerdidaMision;
                heroeRepository.Update(h.Id, h);
            }
        }
    }
}