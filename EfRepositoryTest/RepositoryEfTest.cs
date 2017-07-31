

using EfRepository.Ef;
using EfRepository.Models;
using EfRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace EfRepositoryTest
{
    [TestClass]
    public class RepositoryEfTest
    {

        [TestMethod]
        public void TestSave()
        {
            using (var context = new EntityContextSample())
            {
                var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(context);
                PersonaPocoSample poco = new PersonaPocoSample();
                poco.Nombre = "Gamaliel";
                poco.ApellidoPaterno = "Romero";
                poco.ApellidoMaterno = "Andalón";
                poco.FechaNacimiento = new DateTime(1987, 02, 12);
                poco.Sexo = "M";
                poco.Direccion = new DireccionPocoSample("Av. Ruiz C.", "Centro", "98");
                try
                {
                    if (repository.Create(poco))
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

        [TestMethod]
        public void TestFilter()
        {
            var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(new EntityContextSample());
            foreach (var persona in repository.Retrieve(e=>e.Nombre.Contains("Ism")))
                Debug.WriteLine(persona);
        }

        [TestMethod]
        public void TestCount()
        {
            var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(new EntityContextSample());
            Debug.WriteLine($"Total Rows: {repository.Count()}");
        }

        [TestMethod]
        public void TestSelectKey()
        {
            using (var context = new EntityContextSample())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(context);
                /*Búsqueda por Guid*/
                var person = repository.RetrieveFirstOrDefault<Guid>(Guid.Parse("74F43D82-2276-E711-9EC1-ECB1D73EDABF"));
                Debug.WriteLine($"{person}");
                Debug.WriteLine(person.Direccion);
            }


        }




        /// <summary>
        /// Test con expresiones include
        /// </summary>
        [TestMethod]
        public void TestSelectInclude()
        {
            using (var context = new EntityContextSample())
            {
                //Desahibilita la carga lazy para que no cargue sus relaciones
                context.Configuration.LazyLoadingEnabled = false;
                var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(context);
                Guid uidd = Guid.Parse("74F43D82-2276-E711-9EC1-ECB1D73EDABF");
                /*Búsqueda por Guid*/
                var person = repository.RetrieveFirstOrDefault(e=>e.Uuid==uidd,e=>e.Direccion);
                Debug.WriteLine($"{person}");
                Debug.WriteLine(person.Direccion);
            }


        }

        [TestMethod]
        public void TestFilterPagging()
        {
            var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(new EntityContextSample());
            foreach (var persona in repository.RetrievePagging(e => e.Nombre))
                Debug.WriteLine(persona);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(new EntityContextSample());
            /*Búsqueda por Guid*/
            var person = repository.RetrieveFirstOrDefault<Guid>(Guid.Parse("2a5a55df-f35c-e711-9eb9-ecb1d73edabf"));
            person.Nombre = "Salvador";
            repository.Update(person);
            Debug.WriteLine($"{person}");
        }

    }
}