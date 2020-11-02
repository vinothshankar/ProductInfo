using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ProductInfo.Utils.Interfaces.Data;

namespace ProductInfo.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private Dictionary<string, dynamic> _repositories;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        public void BeginTransaction()
        {
            try
            {
                _dbContext.Database.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Commit()
        {
            try
            {
                _dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }


        public IRepository<Entity> Repository<Entity>() where Entity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(Entity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<Entity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(Entity)), _dbContext));

            return _repositories[type];
        }

        public void Rollback()
        {
            try
            {
                _dbContext.Database.RollbackTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveChanges(bool recordHistory = true)
        {
            var IsSaved = 0;
            try
            {
                _dbContext.SaveChanges();
                IsSaved = 1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                var entityVal = ex.Entries.Single().GetDatabaseValues();
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (IsSaved);
        }
    }
}
