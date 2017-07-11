
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfRepository.Repository
{
    /// <summary>
    /// Clase que implementa el patrón Repository con Entity Framework
    /// </summary>
    public class RepositoryEf<TContext, TEntity> : IRepositoryEf<TEntity>,IRepositoryEfAsync<TEntity> where TEntity : class,new() where TContext : DbContext,new()
    {

        /// <summary>
        /// Contexto EF
        /// </summary>
        public TContext Context { get; set; }     

        /// <summary>
        /// Repository se construye en base a un contexto EF.
        /// </summary>
        public RepositoryEf(TContext context)
        {
            this.Context = context;
        }


        /// <summary>
        /// Operacion para conocer si existen elementos en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate = null)
        {
            if(predicate!=null)
                return Context.Set<TEntity>().Any(predicate);
            else
                return Context.Set<TEntity>().Any();
        }

        /// <summary>
        /// Devuelve el total de coincidencias en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate=null)
        {
            if (predicate != null)
                return Context.Set<TEntity>().Count(predicate);
            else
                return Context.Set<TEntity>().Count();
        }

        /// <summary>
        /// Operacion de guardado.
        /// </summary>
        /// <param name="entity">objeto a persistir</param>
        /// <returns></returns>
        public bool Create(TEntity entity)
        {
            bool centinela = false;
            Context.Set<TEntity>().Add(entity);
            centinela = Context.SaveChanges()!= 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de guardado por lotes.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        public int Create(IEnumerable<TEntity> elements, int saveSkip = 200)
        {
            int totalSave= 0;
            while (elements.Skip(totalSave).Take(saveSkip).Any())
            {
                var subList = elements.Skip(totalSave).Take(saveSkip);
                Context.Set<TEntity>().AddRange(subList);
                Context.SaveChanges();
                totalSave += subList.Count();                
            }
            return totalSave;
        }

        /// <summary>
        /// Operacion de eliminacion
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete<TKey>(TKey key)
        {
            bool centinela = false;
            TEntity entityToDelete = this.Context.Set<TEntity>().Find(key);
            this.Context.Set<TEntity>().Remove(entityToDelete);
            centinela = Context.SaveChanges() > 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de actualizacion
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            bool centinela = false;            
            int changes = Context.SaveChanges();
            centinela = changes >= 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de seleccion
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntity RetrieveFirstOrDefault<TKey>(TKey key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// Operación de seleccion en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity RetrieveFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Proyección de un conjunto de entidades en base a un predicado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        public IEnumerable<TEntity> Retrieve(Expression<Func<TEntity, bool>> predicate=null)
        {
            if(predicate!=null)
                return Context.Set<TEntity>().Where(predicate);
            else
                return Context.Set<TEntity>().AsQueryable();
        }


        /// <summary>
        /// Proyección de un conjunto de entidades en base a un predicado paginado
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <param name="isOrderByDesc"></param>
        /// <param name="predicate"></param>
        /// <param name="rowIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> RetrievePagging<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression < Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200)
        {
            IQueryable<TEntity> _resetSet = null;
            var set = this.Context.Set<TEntity>().AsQueryable<TEntity>();
            if (predicate != null)
            {
                set = set.Where(predicate);
                if (isOrderByDesc)
                    _resetSet = rowIndex == 0 ? set.OrderByDescending(orderByExpression).Take(pageSize) : set.OrderByDescending(orderByExpression).Skip(rowIndex).Take(pageSize);
                else
                    _resetSet = rowIndex == 0 ? set.Take(pageSize) : set.OrderBy(orderByExpression).Skip(rowIndex).Take(pageSize);
            }
            else
            {
                if (isOrderByDesc)
                    _resetSet = rowIndex == 0 ? set.OrderByDescending(orderByExpression).Take(pageSize) : set.OrderByDescending(orderByExpression).Skip(rowIndex).Take(pageSize);
                else
                    _resetSet = rowIndex == 0 ? set.Take(pageSize) : set.OrderBy(orderByExpression).Skip(rowIndex).Take(pageSize);
            }
            return _resetSet;
        }

        #region Async

        /// <summary>
        /// Operacion de guardado asincrono.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(TEntity entity)
        {
            bool centinela = false;
            Context.Set<TEntity>().Add(entity);
            centinela =await Context.SaveChangesAsync() != 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de actualizacion asincrono.
        /// </summary>
        /// <param name="entity">entidad de negocio</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            bool centinela = false;
            int changes = await Context.SaveChangesAsync();
            centinela = changes >= 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de eliminacion asincrono.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<TKey>(TKey key)
        {
            bool centinela = false;
            TEntity entityToDelete = this.Context.Set<TEntity>().Find(key);
            this.Context.Set<TEntity>().Remove(entityToDelete);
            centinela = await Context.SaveChangesAsync() > 0;
            return centinela;
        }

        /// <summary>
        /// Operacion de seleccion asincrono.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TEntity> RetrieveFirstOrDefaultAsync<TKey>(TKey key)
        {
            return await Context.Set<TEntity>().FindAsync(key);
        }


        /// <summary>
        /// Operación de seleccion asincrono en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<TEntity> RetrieveFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }


        /// <summary>
        /// Operación de consulta asincrona de un conjunto de entidades en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Conjunto de objetos que cumplen con el predicado</returns>
        public async Task<IEnumerable<TEntity>> RetrieveAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await Context.Set<TEntity>().Where(predicate).ToListAsync<TEntity>();
            else
                return await Context.Set<TEntity>().AsQueryable().ToListAsync<TEntity>();
        }


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
        public async Task<IEnumerable<TEntity>> RetrievePaggingAsync<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression<Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200)
        {
            IQueryable<TEntity> _resetSet = null;
            var set = this.Context.Set<TEntity>().AsQueryable<TEntity>();
            if (predicate != null)
            {
                set = set.Where(predicate);
                if (isOrderByDesc)
                    _resetSet = rowIndex == 0 ? set.OrderByDescending(orderByExpression).Take(pageSize) : set.OrderByDescending(orderByExpression).Skip(rowIndex).Take(pageSize);
                else
                    _resetSet = rowIndex == 0 ? set.Take(pageSize) : set.OrderBy(orderByExpression).Skip(rowIndex).Take(pageSize);
            }
            else
            {
                if (isOrderByDesc)
                    _resetSet = rowIndex == 0 ? set.OrderByDescending(orderByExpression).Take(pageSize) : set.OrderByDescending(orderByExpression).Skip(rowIndex).Take(pageSize);
                else
                    _resetSet = rowIndex == 0 ? set.Take(pageSize) : set.OrderBy(orderByExpression).Skip(rowIndex).Take(pageSize);
            }
            return _resetSet!=null?await _resetSet.ToListAsync<TEntity>():null;
        }

        /// <summary>
        /// Devuelve el total de coincidencias en base a un predicado asincrono.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await Context.Set<TEntity>().CountAsync(predicate);
            else
                return await Context.Set<TEntity>().CountAsync();
        }


        /// <summary>
        /// Operacion asincrona para conocer si existen elementos en base a un predicado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await Context.Set<TEntity>().AnyAsync(predicate);
            else
                return await Context.Set<TEntity>().AnyAsync();
        }
        
        /// <summary>
        /// Operacion de guardado asincrona por lotes.
        /// </summary>
        /// <param name="elements">Enumeracion de objetos</param>
        /// <param name="saveSkip">Numero de elementos que almacenará en una transacción</param>
        /// <returns>Total instancias almacenadas</returns>
        public async Task<int> CreateAsync(IEnumerable<TEntity> elements, int saveSkip = 200)
        {
            int totalSave = 0;
            while (elements.Skip(totalSave).Take(saveSkip).Any())
            {
                var subList = elements.Skip(totalSave).Take(saveSkip);
                Context.Set<TEntity>().AddRange(subList);
                await Context.SaveChangesAsync();
                totalSave += subList.Count();
            }
            return totalSave;
        }


        /// <summary>
        /// Liberación de Recursos;
        /// </summary>
        public void  Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    #endregion
    }
}