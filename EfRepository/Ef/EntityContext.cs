using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Ef
{
    /// <summary>
    /// Clase que hereda DbContext responsable del Mapeo de Clases a Entidades al modelo relacional.
    /// </summary>
   public class EntityContext:DbContext
    {
        
        public EntityContext() : base("name=DefaultDbContext")
        {
            ///Estrategia de inicializacion
            ///Para efectos de demostración borra y crea la bd si el modelo cambio            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContext>());
            //Descomentar recomendable para escenarios en producción
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityContext,Configuration>)
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Liberar los recursos del contexto
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
        }
    }
}