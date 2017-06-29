
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EfRepository.Ef
{
    /// <summary>
    /// Clase que implementa el patrón Repository con Entity Framework
    /// </summary>
    public abstract class RepositoryEf<TContext, TEntity> : IRepositoryEf<TEntity> where TEntity : class,new() where TContext : DbContext,new()
    {

        public TContext Context { get; set; }

        public RepositoryEf()
        {
            Context = new TContext();
        }

        public RepositoryEf(TContext context)
        {
            this.Context = context;
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate = null)
        {
            if(predicate!=null)
                return Context.Set<TEntity>().Any(predicate);
            else
                return Context.Set<TEntity>().Any();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate=null)
        {
            if (predicate != null)
                return Context.Set<TEntity>().Count(predicate);
            else
                return Context.Set<TEntity>().Count();
        }   

        public bool Save(TEntity entity)
        {
            bool centinela = false;
            Context.Set<TEntity>().Add(entity);
            centinela = Context.SaveChanges()!= 0;
            return centinela;
        }

        public int Save(IEnumerable<TEntity> elements, int saveSkip = 200)
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
        public bool Delete<TKey>(TKey key)
        {
            bool centinela = false;
            TEntity entityToDelete = this.Context.Set<TEntity>().Find(key);
            this.Context.Set<TEntity>().Remove(entityToDelete);
            centinela = Context.SaveChanges() > 0;
            return centinela;
        }

        public bool Update(TEntity entity)
        {
            bool centinela = false;            
            int changes = Context.SaveChanges();
            centinela = changes >= 0;
            return centinela;
        }

        public TEntity Select<TKey>(TKey key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        public TEntity Select(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate=null)
        {
            if(predicate!=null)
                return Context.Set<TEntity>().Where(predicate);
            else
                return Context.Set<TEntity>().AsQueryable();
        }
      

        public IEnumerable<TEntity> FilterPagging<TOrder>(Expression<Func<TEntity, TOrder>> orderByExpression, bool isOrderByDesc = false, Expression < Func<TEntity, bool>> predicate = null, int rowIndex = 0, int pageSize = 200)
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
    }
}