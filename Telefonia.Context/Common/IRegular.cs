using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Context.Common
{
    public interface IRegular<TEntity, TKeyType> : IBase
        where TEntity : class, IKey<TKeyType>
    {
        TEntity Get(IKey<TKeyType> key);

        IEnumerable<TEntity> List(IDictionary<string, object> filter);

        TEntity Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
