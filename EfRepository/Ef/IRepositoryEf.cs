using EfRepository.Ef;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// Interfaz que agrega definición para especializado para la plataforma .net
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryEf<TEntity> : IRepository<TEntity> where TEntity:class
{
    /// <summary>
    /// Operacion de guardado por lotes.
    /// </summary>
    /// <param name="elements">Enumeracion de objetos</param>
    /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
    /// <returns>Total instancias almacenadas</returns>
    int Save(IEnumerable<TEntity> elements, int saveSkip = 200);

    /// <summary>
    /// Proyección de un conjunto de entidades en base a un predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
    IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);


    /// <summary>
    /// Devuelve el total de coincidencias en base a un predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    int Count(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Operación de seleccion en base a un predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    TEntity ToSelect(Expression<Func<TEntity, bool>> predicate);


}