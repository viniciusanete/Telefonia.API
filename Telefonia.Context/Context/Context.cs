using Dapper;
using Dommel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Telefonia.Context.Common;
using Telefonia.Context.Model;

namespace Telefonia.Context.Context
{
    public class Context : IContext, IDisposable
    {
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        public Context(IDbSettings dbSettings)
        {
            _connection = new Microsoft.Data.Sqlite.SqliteConnection(dbSettings.ConnectionString);
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new Exception("Existe uma transação que foi criada anteriormente");

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new Exception("Nenhuma transação foi criada anteriormente");

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }

        public void RollBack()
        {
            if (_transaction == null)
                throw new Exception("Nenhuma transação foi criada anteriormente");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
            _connection.Close();
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object parameters)
            where TEntity : class
        {
            return _connection.Query<TEntity>(sql, parameters, _transaction);
        }


        public TEntity QueryFirstOrDefault<TEntity>(string sql, object parameters)
            where TEntity : class
        {
            return _connection.QueryFirstOrDefault<TEntity>(sql, parameters, _transaction);
        }

        public void ExecuteNonQuery(string sql, object parameters)
        {
            _connection.Execute(sql, parameters, _transaction);
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            finally
            {
                _connection.Dispose();
            }
        }

        public void Delete<TEntity, TKeyType>(TEntity entity) where TEntity : class
        {

            if (entity == null)
                throw new ArgumentException("A chave não pode ser nula");

            if (entity is IActive && entity is IKey<TKeyType>)
            {

                var key = entity as IKey<TKeyType>;
                var itm = _connection.Get<TEntity>(key.Id);

                foreach (var prop in itm.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(StatusRegister) || prop.PropertyType == typeof(StatusRegister?))
                        prop.SetValue(itm, StatusRegister.Canceled);
                }

                _connection.Update(itm, _transaction);
            }
            else
            {
                _connection.Delete(entity, _transaction);
            }
        }

        public TEntity Get<TEntity, TKeyType>(IKey<TKeyType> key) where TEntity : class
        {
            if (key == null)
                return default;

            return _connection.Get<TEntity>(key.Id);
        }

        public IEnumerable<TEntity> List<TEntity, TFilter>(TFilter filter)
            where TEntity : class
            where TFilter : class
        {
            return _connection.GetAll<TEntity>();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _connection.Update(entity, _transaction);
        }

        public TEntity Insert<TEntity, TKeyType>(TEntity entity)
            where TEntity : class
            where TKeyType : class
        {
            _connection.Insert(entity, _transaction);
            return entity;
        }

        public TResult ExecuteScalar<TResult>(string sql, object parameters)
        {
            return _connection.ExecuteScalar<TResult>(sql, parameters, _transaction);
        }
    }
}
