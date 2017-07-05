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
   public partial class EntityContextSample:DbContext
    {

        /// <summary>
        /// Creacion del contexto con la cadena de conexion "DefaultDbContext" 
        /// </summary>
        public EntityContextSample() : base("name=DefaultDbContext")
        {
            ///Estrategia de inicializacion
            ///Para efectos de demostración borra y crea la bd si el modelo cambio            
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContextSample>());
            //Descomentar recomendable para escenarios en producción
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityContext,Configuration>)
        }


        /// <summary>
        /// Creacion del contexto en base a una cadena de conexion
        /// </summary>
        public EntityContextSample(string connectionString) : base(connectionString)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContextSample>());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            MapEntitiesEssentials(modelBuilder);
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