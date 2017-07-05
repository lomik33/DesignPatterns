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
        }

        /// <summary>
        /// Conjunto de objetos PersonaPocoSample
        /// </summary>
        public DbSet<PersonaPocoSample> Personas { get; set; }
    }
}
