
using System;

namespace EfRepository.Models
{
    /// <summary>
    /// Clase de dominio que se traduce a tabla por medio del Orm EF.
    /// </summary>
   public class PersonaPocoSample
    {
        public Guid Uuid { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public override string ToString()
        {
            return $"{Uuid} ({Nombre} {ApellidoPaterno} {ApellidoMaterno} {Sexo}) Fecha N.:{FechaNacimiento}";
        }
    }
}