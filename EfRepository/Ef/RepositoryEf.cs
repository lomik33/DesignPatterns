
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EfRepository.Ef
{
    /// <summary>
    /// Clase que implementa el patrón Repository
    /// </summary>
    public class RepositoryEf<TEntity> : IRepositoryEf<TEntity> where TEntity : class
    {
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Save(IEnumerable<TEntity> elements, int saveSkip = 200)
        {
            throw new NotImplementedException();
        }

        public bool Save(TEntity entity)
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

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TEntity> IRepositoryEf<TEntity>.Filter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}