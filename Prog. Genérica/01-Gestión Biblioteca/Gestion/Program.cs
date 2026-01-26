using System.Text;
using Gestion.Config;
using Gestion.Enums;
using Gestion.Models;
using Gestion.Utils;
using Serilog;

// daw's template
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day).CreateLogger();
Title = "Gestión Biblioteca";
OutputEncoding = Encoding.UTF8;
Clear();
Main();
WriteLine("\n👋 Presiona una tecla para salir...");
ReadKey();
return;

void Main() {
   WriteLine("-- Gestión Biblioteca --");
   OpcionMenuPrincipal opcion;
   do {
      Utilities.ImrimirMenuPrincipal();
      opcion = (OpcionMenuPrincipal)int.Parse(Utilities.ValidarDatoRegex("- Opción elegida ->", Configuration.RegexMenuPrincipal));
      switch (opcion) {
         case OpcionMenuPrincipal.Salir: break;
         /*case OpcionMenuPrincipal.VerTodos: VerBiblioteca(service); break;
         case OpcionMenuPrincipal.ListarEstado: ListarEstado(service); break;
         case OpcionMenuPrincipal.BuscarId: BuscarId(service); break;
         case OpcionMenuPrincipal.OrdenarPaginas: OrdenarPorPaginas(service); break;
         case OpcionMenuPrincipal.Crear: Crear(service); break;
         case OpcionMenuPrincipal.Actualizar: Actualizar(service); break;*/
         default: // si se entra aquí ha fallado la validacion de la opcion
            Console.WriteLine($"⚠️ Opción inválida. Introduzca una de las {(int)OpcionMenuPrincipal.Actualizar} opciones posibles.");
            break;
      }
   } while (opcion != OpcionMenuPrincipal.Salir);
}