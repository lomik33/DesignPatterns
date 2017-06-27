using System.Data.Entity;
using EfRepository.Ef.MapPocos;
using EfRepository.Models;

namespace EfRepository.Ef
{
    public partial class EntityContext
    {
        public void MapEntitiesEssentials(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonaPocoEntityConfiguration());
        }

        public DbSet<PersonaPoco> Personas { get; set; }
    }
}
