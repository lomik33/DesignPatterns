
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
    /// Clase que implementa el patrón Repository
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

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public TEntity Select<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public TEntity ToSelect(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void ToSelectAll(Expression<Func<TEntity, long>> orderByExpression, Expression<Func<TEntity, bool>> predicate, bool isOrderByDesc = false, int pageSize = 200, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}