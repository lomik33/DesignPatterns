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
             EntityContextSample ec=new EntityContextSample();            
            var persona = new PersonaPocoSample()
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
