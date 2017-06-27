
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Models
{
    /// <summary>
    /// Clase de dominio que se traduce a tabla por medio del Orm EF.
    /// </summary>
   public class PersonaPoco
    {
        public Guid Uuid { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}