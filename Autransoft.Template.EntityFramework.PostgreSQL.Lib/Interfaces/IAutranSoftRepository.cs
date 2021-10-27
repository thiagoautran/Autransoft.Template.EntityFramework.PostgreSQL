using System.Threading.Tasks;
using Autransoft.Template.EntityFramework.PostgreSQL.Lib.Entities;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Interfaces
{
    public interface IAutranSoftRepository<Entity>
        where Entity : AutranSoftEntity
    {
        Task<Entity> AddAsync(Entity entity);

        Task UpdateAsync(Entity entity);

        Task DeleteTableAsync(string tableName);
    }
}