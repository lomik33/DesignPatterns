using System;

namespace EfRepository.Ef
{
    /// <summary>
    /// Interfaz que define el comportamiento del patrón repository...
    /// Define los metódos para principales para crear un CRUD por entidad.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Operacion de guardado.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        bool Save(TEntity entity);
        /// <summary>
        /// Operacion de actualizacion
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        bool Update(TEntity entity);


        /// <summary>
        /// Operacion de eliminacion
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete<TKey>(TKey key);

        /// <summary>
        /// Operacion de seleccion
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Select<TKey>(TKey key);


    }

}