using System;
using System.Collections.Generic;
using System.Text;
using Telefonia.Context.Model;

namespace Telefonia.Context.Context
{
    public interface IContext
    {
        void BeginTransaction();
        void RollBack();
        void Commit();
        void ExecuteNonQuery(string sql, object parameters);
        TResult ExecuteScalar<TResult>(string sql, object parameters);
        IEnumerable<TEntity> Query<TEntity>(string sql, object parameters) where TEntity : class;
        TEntity QueryFirstOrDefault<TEntity>(string sql, object parameters)
            where TEntity : class;

        void Delete<TEntity, TKeyType>(TEntity entity) where TEntity : class;

        TEntity Get<TEntity, TKeyType>(IKey<TKeyType> key) where TEntity : class;

        IEnumerable<TEntity> List<TEntity, TFilter>(TFilter filter)
            where TEntity : class
            where TFilter : class;

        void Update<TEntity>(TEntity entity)
            where TEntity : class;

        TEntity Insert<TEntity, TKeyType>(TEntity entity)
            where TEntity : class
            where TKeyType : class;
    }
}
