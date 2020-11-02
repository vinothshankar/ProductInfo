using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInfo.Utils.Interfaces.Data
{

    public interface IRepository<Entity> where Entity : class
    {
        void BulkInsert(IEnumerable<Entity> entities);
        void BulkUpdate(IEnumerable<Entity> entities);
        void Delete(Entity entity);
        void DeleteById(object id);
        Entity GetById(object id);
        void Insert(Entity entity);
        IQueryable<Entity> Querable();
        void Update(Entity entity);
    }
}
