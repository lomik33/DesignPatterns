# DesignPatterns
Portafolio de proyectos aplicando patrones de diseño en C Sharp 


“La información es poder” esta frase trillada pero cierta en todos los ámbitos en que busquemos aplicación; en el desarrollo de software no es la excepción. 
En el día a día para los desarrolladores de software (Back-End Developer’s) está implícito que sus aplicaciones tengan la capacidad de almacenar, consultar, actualizar y borrar información. 
Basándonos en esa necesidad particularmente surgen: rutinas, middleware’s, frameworks de diferentes proveedores encargados de persistir, recuperar información sea en bases de datos, archivos u otros repositorios de información. El componente “EFRepository” tiene el propósito de ayudar a los programadores .Net con esta necesidad mediante la implementación del patrón de diseño “Repository” haciendo uso de Entity Framework.

Patrón de Diseño Repository Overview

Un “Repository” es una clase servirá como intermediario entre nuestra capa de lógica de negocios y la capa de datos, es una clase que nos ofrecerá una interfaz CRUD (Crear, Obtener, Actualizar, Borrar -Create, Retrieve, Update, Delete en inglés-). Este patrón define una interfaz para cada una de las operaciones, encapsulando el comportamiento para cada proveedor en especial (Oracle, Microsoft Azure BD, Sql Server, Mysql, Csv…)  una ventaja para los desarrolladores es usar una misma api para todos sus objetos de negocio y siendo la implementación especifica de Repository la responsable de procesar las operaciones CRUD.
EfRepository desacopla este comportamiento del modelo y de la lógica de negocios de la aplicación de tal manera que sólo es necesario instanciar un objeto repository con la clase POCO que persistirá y el objeto de contexto que administra Entity Framework para el acceso a datos.
Interfaz IRepositoryEf con la definición de métodos que actualmente están implementados (se considera las firmas de comportamiento asíncrono)
/// Operacion de registro.
bool Create(TEntity entity);
/// Operacion de actualizacion
bool Update(TEntity entity);
/// Operacion de eliminacion
bool Delete<TKey>(TKey key);
/// Operacion de seleccion
TEntity RetrieveFirstOrDefault<TKey>(TKey key);

/// Operación de selección del primer objeto coincidente en base a un predicado
TEntity RetrieveFirstOrDefault(Expression<Func<TEntity, bool>> predicate);    
/// Operación que devuelve una colección coincidente en base a un predicado
IEnumerable<TEntity> Retrieve(Expression<Func<TEntity, bool>> predicate=null);
/// Operación que devuelve una colección coincidente en base a un predicado paginado.
IEnumerable<TEntity> RetrievePagging<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200);
/// Operación Count en base a un predicado
int Count(Expression<Func<TEntity, bool>> predicate=null);   
/// Operación binaria que retorna la existencia de regustros en base a un predicado
bool Exists(Expression<Func<TEntity, bool>> predicate=null);
/// Operación de selección del primer objeto coincidente en base a un predicado
int Create(IEnumerable<TEntity> elements, int saveSkip = 200);
/// Método que liberar recursos del contexto.
void Dispose();

Ejemplo de Implementación llamada a operación Create haciendo uso EfRepository
//Instancia de contexto previamente configurado con sus clases que mapea.
using (var context = new EntityContextSample())
            {
                var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(context);
                PersonaPocoSample poco = new PersonaPocoSample();
                poco.Nombre = "Person";
                poco.ApellidoPaterno = "Sample";
                poco.ApellidoMaterno = "Sample";
                poco.FechaNacimiento = new DateTime(1987, 02, 12);
                poco.Sexo = "M";
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

Donde:
Ejemplo de creación de Instancia Repository:
var repository = new RepositoryEf<EntityContextSample, PersonaPocoSample>(context);

EntityContextSample: Clase que hereda de System.Data.Entity.DBContext y que mapea las clases de dominio (Por lo general POCO’s “PLAIN OLD CLR OBJECT”).
PersonaPocoSample: Clase POCO de modelo de dominio que se mapea a modelo relacional haciendo uso de Entity Framework.
La implementación desacopla las clases de contexto y de dominio con la intención que cada programador configure Entity Fremework y mapee su modelo propio aplicación.

Dependencias
•	Versión de Framework CLR .NET 4.6.1
•	Entity Framework 6.1.3

Bonus:
En escenarios de n-capas se requiere de una implementación que añada lógica de negocio de la aplicación: encapsulando, validando, ejecutando las operaciones transaccionales que los objetos repository poseen. El hacer esta separación permite a los programadores sólo hacer un método o conjunto de ellos con un comportamiento en común p. e.j el registro de un usuario y que dicho comportamiento sea invocado independientemente de la interfaz de usuario sea una terminal de consola, web-app, desktop-app, mobile-app con la principal ventaja de reutilizar código.

Ejemplo

//Objeto manager encapsula el comportamiento 
//Repository Inyección de Dependencias
var manager = new PersonaManager(new RepositoryEf<EntityContextSample,PersonaPocoSample>(new EntityContextSample()));

//Manager encapsula el acceso a las operaciones repository

Para más detalle ver: 
EfRepository.Manager.Manager
EfRepository.Manager.Manager.IManagerResponse<TObject>
EfRepository.Manager.Manager.IManagerResponse<TObject>

Tienes más dudas ¿?
Se anexa el proyecto de test
Pruebas de Repository
	EfRepositoryTest.RepositoryEfTest
Pruebas de Manager
	EfRepositoryTest.ManagerTest

	







