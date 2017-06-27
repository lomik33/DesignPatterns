using System;
using System.Linq;
using EfRepository.Ef;
using EfRepository.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EfRepositoryTest
{
    [TestClass]
    public class EfTest
    {
        [TestMethod]
        public void TestContextSave()
        {
             EntityContext ec=new EntityContext();            
            var persona = new PersonaPoco()
            {
                Uuid = Guid.NewGuid(),
                Nombre = "Juan",
                ApellidoPaterno = "Perez",
                ApellidoMaterno = "Hernandez",
                Sexo = "M",
                FechaNacimiento = new DateTime(1980, 01, 01)
            };            
            ec.Personas.Add(persona);
            var resultado=ec.SaveChanges();
            
        }
    }
}
