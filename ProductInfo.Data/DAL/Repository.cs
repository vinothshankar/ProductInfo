using ProductInfo.Utils.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ProductInfo.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProductInfo.DAL
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<Entity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<Entity>();
        }
        public void BulkInsert(IEnumerable<Entity> entities)
        {
            _dbSet.AddRange(entities);
        }
        public void BulkUpdate(IEnumerable<Entity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(Entity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            Entity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public Entity GetById(object id)
        {
            var dataToReturn = _dbSet.Find(id);
            return dataToReturn;
        }

        public void Insert(Entity entity)
        {
            _dbSet.Add(entity);
        }

        public IQueryable<Entity> Querable()
        {
            return _dbSet;
        }
        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
