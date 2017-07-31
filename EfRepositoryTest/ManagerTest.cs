
using EfRepository.Ef;
using EfRepository.Manager;
using EfRepository.Models;
using EfRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepositoryTest
{
    [TestClass]
  public  class ManagerTest
    {

        /// <summary>
        /// Uso de Comportamiento de Logica de Negocios --Save--
        /// </summary>
        [TestMethod]
        public void TestSave()
        {
            //Repository y Contexto por Inyeccion de Dependencias
            var manager = new PersonaManager(new RepositoryEf<EntityContextSample,PersonaPocoSample>(new EntityContextSample()));

            PersonaPocoSample poco = new PersonaPocoSample();
            poco.Nombre = "Gamaliel";
            poco.ApellidoPaterno = "Romero";
            poco.ApellidoMaterno = "Andalón";
            poco.FechaNacimiento = new DateTime(1987, 02, 12);
            poco.Sexo = "M";
            try
            {
                if (manager.Save(poco).IsSucess)
                    Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} ha sigo registrado satisfactoriamente con uuid: {poco.Uuid}");
                else
                    Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} no ha podido ser registrado.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} no ha podido ser registrado detalle: {ex}");
            }
        }


        /// <summary>
        /// Se consulta entidad persona con sus relaciones en base a lazy
        /// </summary>
        [TestMethod]
        public void TestSelect()
        {
            //Repository y Contexto por Inyeccion de Dependencias
            using (var context = new EntityContextSample())
            {
                //LoadLazy=true implicito
                var manager = new PersonaManager(new RepositoryEf<EntityContextSample, PersonaPocoSample>(context));
                var response=manager.Filter();
                foreach (var persona in response?.ObjectResponse)
                    Debug.WriteLine(persona + " dirección: " + persona.Direccion);
            }

        }


        /// <summary>
        /// Se consulta entidad persona con sus relaciones haciendo uso de includes
        /// </summary>
        [TestMethod]
        public void TestSelectInclude()
        {
            //Repository y Contexto por Inyeccion de Dependencias
            using (var context = new EntityContextSample())
            {
                //LoadLazy=false 
                context.Configuration.LazyLoadingEnabled = false;                
                var manager = new PersonaManager(new RepositoryEf<EntityContextSample, PersonaPocoSample>(context));
                //Expresion de inclusión
                var response = manager.Filter(includeExpressions:e=>e.Direccion);
                foreach (var persona in response?.ObjectResponse)
                    Debug.WriteLine(persona + " dirección: " + persona.Direccion);
            }

        }
    }
}