
using EfRepository.Bc;
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
    public class RepositoryTest
    {

        [TestMethod]
        public void TestSave()
        {
            PersonaBc bc = new PersonaBc();
            PersonaPoco poco = new PersonaPoco();
            poco.Nombre = "Gamaliel";
            poco.ApellidoPaterno = "Romero";
            poco.ApellidoMaterno = "Andalón";            
            poco.FechaNacimiento = new DateTime(1987,02,12);
            poco.Sexo = "M";
            try
            {
                if (bc.Save(poco))
                    Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} ha sigo registrado satisfactoriamente con uuid: {poco.Uuid}");
                else
                    Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} no ha podido ser registrado.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{poco.Nombre} {poco.ApellidoPaterno} {poco.ApellidoMaterno} no ha podido ser registrado detalle: {ex}");
            }
        }

        [TestMethod]
        public void TestFilter()
        {
            PersonaBc bc = new PersonaBc();
            foreach (var persona in bc.Filter(e=>e.Nombre.Contains("Ism")))
                Debug.WriteLine(persona);
        }

        [TestMethod]
        public void TestCount()
        {
            PersonaBc bc = new PersonaBc();
            Debug.WriteLine($"Total Rows: {bc.Count()}");
        }

        [TestMethod]
        public void TestSelect()
        {
            PersonaBc bc = new PersonaBc();
            /*Búsqueda por Guid*/
            var person = bc.Select<Guid>(Guid.Parse("2a5a55df-f35c-e711-9eb9-ecb1d73edabf"));
            Debug.WriteLine($"{person}");

        }

        [TestMethod]
        public void TestFilterPagging()
        {
            PersonaBc bc = new PersonaBc();
            foreach (var persona in bc.FilterPagging(e => e.Nombre))
                Debug.WriteLine(persona);
        }

        [TestMethod]
        public void TestUpdate()
        {
            PersonaBc bc = new PersonaBc();
            /*Búsqueda por Guid*/
            var person = bc.Select<Guid>(Guid.Parse("2a5a55df-f35c-e711-9eb9-ecb1d73edabf"));
            person.Nombre = "Salvador";
            bc.Update(person);
            Debug.WriteLine($"{person}");
        }

    }
}