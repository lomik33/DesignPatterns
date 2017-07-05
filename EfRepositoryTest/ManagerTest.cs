
using EfRepository.Ef;
using EfRepository.Manager;
using EfRepository.Models;
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
    }
}