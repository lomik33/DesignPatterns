
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfRepository.Ef
{
    /// <summary>
    /// Interfaz que define el comportamiento del patron Repository para especializado para la plataforma .Net Framework 4.6 comportamiento asincrono
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryEfAsync<TEntity>:IRepositoryEf<TEntity> where TEntity : class
    {


        /// <summary>
        /// Operacion de guardado asincrono.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        Task<bool> SaveAsync(TEntity entity);
        /// <summary>
        /// Operacion de actualizacion asincrono.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity);


        /// <summary>
        /// Operacion de eliminacion asincrono.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<TKey>(TKey key);

        /// <summary>
        /// Operacion de seleccion asincrono.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> SelectAsync<TKey>(TKey key);

        /// <summary>
        /// Operación de seleccion asincrono en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Operación de consulta asincrona de un conjunto de entidades en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Operación de consulta asincrona de un conjunto de entidades en base a un predicado paginado.
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <param name="isOrderByDesc"></param>
        /// <param name="predicate"></param>
        /// <param name="rowIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FilterPaggingAsync<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200);

        /// <summary>
        /// Devuelve el total de coincidencias en base a un predicado asincrono.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);


        /// <summary>
        /// Operacion asincrona para conocer si existen elementos en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate = null);


        #region BatchTransactions
        /*Metodo que definen comportamiento por lotes*/

        /// <summary>
        /// Operacion de guardado asincrona por lotes.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        Task<int> SaveAsync(IEnumerable<TEntity> elements, int saveSkip = 200);

        ///// <summary>
        ///// Operacion que obtiene entidades en base a una carga paginada.
        ///// </summary>
        ///// <param name="orderByExpression"></param>
        ///// <param name="predicate"></param>
        ///// <param name="isOrderByDesc"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="includeExpressions"></param>
        //void All(Expression<Func<TEntity, Int64>> orderByExpression, Expression<Func<TEntity, bool>> predicate, bool isOrderByDesc = false, int pageSize = 200, params Expression<Func<TEntity, object>>[] includeExpressions);

        #endregion


    }
}