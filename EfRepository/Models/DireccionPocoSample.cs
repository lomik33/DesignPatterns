using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Models
{
   public class DireccionPocoSample
    {
        public Guid Uuid { get; set; }

        public string Calle { get; set; }

        public string Colonia { get; set; }

        public string NumeroExterior { get; set; }

        public DireccionPocoSample()
        {

        }

        public DireccionPocoSample(string calle, string colonia, string numeroExterior)
        {
            this.Calle = calle;
            this.Colonia = colonia;
            this.NumeroExterior = numeroExterior;
        }

        public override string ToString()
        {
            return $"{Colonia} {Calle} {NumeroExterior}".Trim();
        }
    }
}