using EfRepository.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfRepository.Manager
{

    /// <summary>
    /// Clase de administración que gestiona el acceso a Bd mediante Repository's
    /// Diseñada para Arquitectura N-Capas
    /// Implementa el Comportamiento de Logica de Negocio
    /// </summary>
    /// <typeparam name="TPoco"></typeparam>
    public class Manager<TPoco> where TPoco:class
    {
        
        /// <summary>
        /// Instancia Repository
        /// </summary>
        public IRepositoryEfAsync<TPoco> Repository { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="repository"></param>
        public Manager(IRepositoryEfAsync<TPoco> repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Metodos de logica de negocio guardado
        /// </summary>
        /// 
        public virtual IManagerResponse<TPoco> Save(TPoco poco)
        {
            IManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            response.ObjectResponse = poco;
            try
            {
                response.IsSucess = Repository.Create(poco);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodos de logica de negocio guardado asincrono
        /// </summary>
        /// 
        public virtual async Task<IManagerResponse<TPoco>> SaveAsync(TPoco poco)
        {
            IManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            response.ObjectResponse = poco;
            try
            {
                response.IsSucess = await Repository.CreateAsync(poco);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        ///  Metodo de logica de negocio para conocer si existen elementos en base a un predicado 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IManagerResponse<bool> Exists(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
               response.ObjectResponse=Repository.Exists(predicate);
               response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        ///  Metodo de logica de negocio para conocer si existen elementos en base a un predicado asincrono
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IManagerResponse<bool>> ExistsAsync(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
                response.ObjectResponse = await Repository.ExistsAsync(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocios que devuelve el total de coincidencias en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IManagerResponse<int> Count(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<int> response = new ManagerResponse<int>();
            try
            {
                response.ObjectResponse = Repository.Count(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocios que devuelve el total de coincidencias en base a un predicado asincrono
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IManagerResponse<int>> CountAsync(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<int> response = new ManagerResponse<int>();
            try
            {
                response.ObjectResponse = await Repository.CountAsync(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Guardado por lotes.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        public IManagerResponse<int> Save(IEnumerable<TPoco> elements, int saveSkip = 200)
        {
            IManagerResponse<int> response = new ManagerResponse<int>();
            try
            {
                response.ObjectResponse = Repository.Create(elements,saveSkip);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;

        }

        /// <summary>
        /// Guardado por lotes asincrono.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        public async Task<IManagerResponse<int>> SaveAsync(IEnumerable<TPoco> elements, int saveSkip = 200)
        {
            IManagerResponse<int> response = new ManagerResponse<int>();
            try
            {
                response.ObjectResponse = await Repository.CreateAsync(elements, saveSkip);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de logica de negocio de eliminacion de un objeto
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public IManagerResponse<bool> Delete<TKey>(TKey key)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
                response.ObjectResponse =  Repository.Delete(key);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de logica de negocio de eliminacion de un objeto asincrono
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IManagerResponse<bool>> DeleteAsync<TKey>(TKey key)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
                response.ObjectResponse = await Repository.DeleteAsync(key);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de Logica de Negocio de actualizacion
        /// </summary>
        /// <param name="poco">entidad de negocio</param>
        /// <returns></returns>
        public IManagerResponse<bool> Update(TPoco poco)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
                response.ObjectResponse = Repository.Update(poco);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocio de actualizacion asincrono
        /// </summary>
        /// <param name="poco">entidad de negocio</param>
        /// <returns></returns>
        public async Task<IManagerResponse<bool>> UpdateAsync(TPoco poco)
        {
            IManagerResponse<bool> response = new ManagerResponse<bool>();
            try
            {
                response.ObjectResponse = await Repository.UpdateAsync(poco);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de Logica de Negocio de seleccion en base a una key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public IManagerResponse<TPoco> Select<TKey>(TKey key)
        {
            ManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            try
            {
                response.ObjectResponse = Repository.RetrieveFirstOrDefault(key);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocio de seleccion en base a una key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IManagerResponse<TPoco>> SelectAsync<TKey>(TKey key)
        {
            ManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            try
            {
                response.ObjectResponse = await Repository.RetrieveFirstOrDefaultAsync(key);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de Logica de Negocios de seleccion en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IManagerResponse<TPoco> Select(Expression<Func<TPoco, bool>> predicate)
        {
            ManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            try
            {
                response.ObjectResponse = Repository.RetrieveFirstOrDefault<Expression<Func<TPoco, bool>>>(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de Logica de Negocios de seleccion en base a un predicado asincrono
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IManagerResponse<TPoco>> SelectAsync(Expression<Func<TPoco, bool>> predicate)
        {
            ManagerResponse<TPoco> response = new ManagerResponse<TPoco>();
            try
            {
                response.ObjectResponse = await Repository.RetrieveFirstOrDefaultAsync(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocios que recupera de una coleccion de objetos en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        public IManagerResponse<IEnumerable<TPoco>> Filter(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<IEnumerable<TPoco>> response = new ManagerResponse<IEnumerable<TPoco>>();
            try
            {
                response.ObjectResponse = Repository.Retrieve(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de Negocios que recupera de una coleccion de objetos en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        public async Task<IManagerResponse<IEnumerable<TPoco>>> FilterAsync(Expression<Func<TPoco, bool>> predicate = null)
        {
            IManagerResponse<IEnumerable<TPoco>> response = new ManagerResponse<IEnumerable<TPoco>>();
            try
            {
                response.ObjectResponse = await Repository.RetrieveAsync(predicate);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }


        /// <summary>
        /// Metodo de Logica de NEgocios que devuelve una coleccion de entidades en base a un predicado paginado.
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <param name="isOrderByDesc"></param>
        /// <param name="predicate"></param>
        /// <param name="rowIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IManagerResponse<IEnumerable<TPoco>> FilterPagging<TOrder>(Expression<Func<TPoco,TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TPoco, bool>> predicate = null, int rowIndex = 0, int pageSize = 200)
        {
            IManagerResponse<IEnumerable<TPoco>> response = new ManagerResponse<IEnumerable<TPoco>>();
            try
            {
                response.ObjectResponse =  Repository.RetrievePagging<TOrder>(orderByExpression,isOrderByDesc,predicate,rowIndex,pageSize);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Metodo de Logica de Negocios que devuelve una coleccion de entidades en base a un predicado paginado asincrono.
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <param name="isOrderByDesc"></param>
        /// <param name="predicate"></param>
        /// <param name="rowIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IManagerResponse<IEnumerable<TPoco>>> FilterPaggingAsync<TOrder>(Expression<Func<TPoco, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TPoco, bool>> predicate = null, int rowIndex = 0, int pageSize = 200)
        {
            IManagerResponse<IEnumerable<TPoco>> response = new ManagerResponse<IEnumerable<TPoco>>();
            try
            {
                response.ObjectResponse = await Repository.RetrievePaggingAsync<TOrder>(orderByExpression, isOrderByDesc, predicate, rowIndex, pageSize);
                response.IsSucess = true;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }
            return response;
        }
    }
}