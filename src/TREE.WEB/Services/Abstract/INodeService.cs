using System.Collections.Generic;
using System.Threading.Tasks;
using TREE.WEB.ViewModels;

namespace TREE.WEB.Services.Abstract
{
    public interface INodeService
    {
        Task<NodeViewModel> GetByIdAsync(long id);
        Task<List<NodeViewModel>> GetAllAsync();
        Task<NodeViewModel> UpdateAsync(NodeEditViewModel request);
        Task<List<LabelValueViewModel>> GetSuggestions();
        Task<NodeViewModel> RemoveAsync(long id);
        Task<NodeViewModel> AddAsync(NodeAddViewModel request);
    }
}
