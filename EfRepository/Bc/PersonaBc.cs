
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
    public class PersonaBc:RepositoryEf<EntityContext, PersonaPoco>
    {
        public PersonaBc() : base()
        {

        }
        public PersonaBc(EntityContext context) : base(context)
        {

        }

    }
}