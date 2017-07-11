using EfRepository.Models;
using EfRepository.Ef;
using EfRepository.Repository;

namespace EfRepository.Manager
{
    /// <summary>
    /// Clase Manager del POCO Persona
    /// </summary>
    public class PersonaManager : Manager<PersonaPocoSample>
    {
        /// <summary>
        /// Constructor que inyecta el repository para comunicar con capa de datos
        /// </summary>
        /// <param name="repository"></param>
        public PersonaManager(IRepositoryEfAsync<PersonaPocoSample> repository) : base(repository)
        {

        }
        
    }
}