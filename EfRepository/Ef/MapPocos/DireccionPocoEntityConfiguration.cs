
using EfRepository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Ef.MapPocos
{
    class DireccionPocoEntityConfiguration:EntityTypeConfiguration<DireccionPocoSample>
    {
        public DireccionPocoEntityConfiguration()
        {
            ToTable("Direccion");
            HasKey(k => k.Uuid);
            Property(p => p.Uuid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Calle)
                .IsRequired()
                .HasMaxLength(255);
            Property(p => p.Colonia)
                .IsRequired()
                .HasMaxLength(255);
            Property(p => p.NumeroExterior)
                .IsOptional()
                .HasMaxLength(255);     
            
        }
    }
}