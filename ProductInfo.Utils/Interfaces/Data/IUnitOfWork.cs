using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInfo.Utils.Interfaces.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        IRepository<Entity> Repository<Entity>() where Entity : class;
        void Rollback();
        int SaveChanges(bool recordHistory = true);
    }
}
