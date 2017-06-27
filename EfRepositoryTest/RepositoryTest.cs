
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
            poco.Nombre = "Ismael";
            poco.ApellidoPaterno = "López";
            poco.ApellidoMaterno = "Martínez";            
            poco.FechaNacimiento = new DateTime(1985, 5, 29);
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
    }
}