using System.Data.Entity;
using EfRepository.Ef.MapPocos;
using EfRepository.Models;

namespace EfRepository.Ef
{
    public partial class EntityContextSample
    {
        /// <summary>
        /// Mapea por Fluent Api PersonaPoco
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void MapEntitiesEssentials(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonaPocoEntityConfiguration());
            modelBuilder.Configurations.Add(new DireccionPocoEntityConfiguration());
        }

        /// <summary>
        /// Conjunto de objetos PersonaPocoSample
        /// </summary>
        public DbSet<PersonaPocoSample> Personas { get; set; }
        
        /// <summary>
        /// Se agrega para mostrar relacion se asociacion
        /// </summary>
        public DbSet<DireccionPocoSample> Direcciones { get; set; }
    }
}
