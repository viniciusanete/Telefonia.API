using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Context;
using Telefonia.Context.Model;

namespace Telefonia.Context.Common
{
    public class RegularRepository<TEntity, TKeyType> : IRegular<TEntity, TKeyType>
        where TEntity : class, IKey<TKeyType>
    {
        public RegularRepository(IContext context)
        {
            Context = context;
        }

        public IContext Context { get; set; }

        public virtual void Delete(TEntity key)
        {
            Context.Delete<TEntity, TKeyType>(key);
        }

        public virtual TEntity Get(IKey<TKeyType> key)
        {
            return Context.Get<TEntity, TKeyType>(key);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Context.Insert<TEntity, IKey<TKeyType>>(entity);
        }

        public virtual IEnumerable<TEntity> List(IDictionary<string, object> filter)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }
    }
}
