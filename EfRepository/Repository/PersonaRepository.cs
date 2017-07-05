
using EfRepository.Ef;
using EfRepository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Bc
{
    /// <summary>
    /// Clase de logica de negocio que por herencia tiene el repository implementado
    /// </summary>
    public class PersonaRepository : RepositoryEf<EntityContext, PersonaPoco>
    {
        /// <summary>
        /// Implementar constructor heredado
        /// </summary>
        /// <param name="context"></param>
        public PersonaRepository(EntityContext context) : base(context)
        {
        }
    }
}