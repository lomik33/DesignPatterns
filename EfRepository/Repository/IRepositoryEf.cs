﻿using EfRepository.Ef;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace EfRepository.Repository {
    /// <summary>
    /// Interfaz que define el comportamiento del patron Repository para especializado para la plataforma .Net Framework 4.6
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryEf<TEntity> where TEntity : class
    {
        /// <summary>
        /// Operacion de guardado.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        bool Create(TEntity entity);
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
        TEntity RetrieveFirstOrDefault<TKey>(TKey key);

        /// <summary>
        /// Operación de seleccion en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeExpressions">expresiones de entidades que se deben incluir</param>
        /// <returns></returns>
        TEntity RetrieveFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Proyección de un conjunto de entidades en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeExpressions">expresiones lambda de entidades que se deben incluir en la ejecución del query.</param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        IEnumerable<TEntity> Retrieve(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Proyección de un conjunto de entidades en base a un predicado paginado
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <param name="isOrderByDesc"></param>
        /// <param name="predicate"></param>
        /// <param name="rowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeExpressions">expresiones lambda de entidades que se deben incluir en la ejecución del query.</param>
        /// <returns></returns>
        IEnumerable<TEntity> RetrievePagging<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Devuelve el total de coincidencias en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);


        /// <summary>
        /// Operacion para conocer si existen elementos en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate = null);


        #region BatchTransactions
        /*Metodo que definen comportamiento por lotes*/

        /// <summary>
        /// Operacion de guardado por lotes.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        int Create(IEnumerable<TEntity> elements, int saveSkip = 200);

        ///// <summary>
        ///// Operacion que obtiene entidades en base a una carga paginada.
        ///// </summary>
        ///// <param name="orderByExpression"></param>
        ///// <param name="predicate"></param>
        ///// <param name="isOrderByDesc"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="includeExpressions"></param>
        //void All(Expression<Func<TEntity, Int64>> orderByExpression, Expression<Func<TEntity, bool>> predicate, bool isOrderByDesc = false, int pageSize = 200, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Liberación de Recursos
        /// </summary>
        void Dispose();


        #endregion

    }

}