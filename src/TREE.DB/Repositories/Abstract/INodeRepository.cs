using System.Collections.Generic;
using System.Threading.Tasks;
using TREE.DB.Entities;

namespace TREE.DB.Repositories.Abstract
{
    public interface INodeRepository
    {
        Task<IList<Node>> GetAllAsync();
        Task<IList<Node>> GetSuggestions();
        Task<Node> GetByIdAsync(long id);
        Task AddAsync(Node node);
        Task RemoveAsync(Node node);
        Task UpdateAsync(Node node);
        Task SaveChangesAsync();
    }
}
