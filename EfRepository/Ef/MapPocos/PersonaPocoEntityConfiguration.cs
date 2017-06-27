using System.ComponentModel.DataAnnotations.Schema;
using EfRepository.Models;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace EfRepository.Ef.MapPocos
{
    class PersonaPocoEntityConfiguration : EntityTypeConfiguration<PersonaPoco>
    {
        public PersonaPocoEntityConfiguration()
        {
            ToTable("Persona");
            HasKey(k => k.Uuid);
            Property(p => p.Uuid).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(255);
            Property(p => p.ApellidoPaterno)
                .IsRequired()
                .HasMaxLength(255);
            Property(p => p.ApellidoMaterno)
                .IsOptional()
                .HasMaxLength(255);
            Property(p => p.FechaNacimiento)
                .IsRequired();
            Property(p => p.Sexo)
                .IsRequired()
                .HasMaxLength(1);           
        }
    }
}
