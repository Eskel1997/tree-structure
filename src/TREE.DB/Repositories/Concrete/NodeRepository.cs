using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TREE.DB.DAL;
using TREE.DB.Entities;
using TREE.DB.Repositories.Abstract;

namespace TREE.DB.Repositories.Concrete
{
    internal class NodeRepository : INodeRepository
    {
        protected readonly TreeContext context;
        protected readonly DbSet<Node> dbSet;
        public NodeRepository(TreeContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<Node>();
        }
        public async Task<IList<Node>> GetAllAsync()
        {
            return await this.dbSet
                .Where(x => x.ParentId == null)
                .ToListAsync();
        }

        public async Task<IList<Node>> GetSuggestions()
        {
            return await this.dbSet
                .ToListAsync();
        }

        public async Task<Node> GetByIdAsync(long id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task AddAsync(Node node)
        {
            await Task.FromResult(await this.dbSet.AddAsync(node));
        }

        public async Task RemoveAsync(Node node)
        {
            await Task.FromResult(this.dbSet.Remove(node));
        }

        public async Task UpdateAsync(Node node)
        {
            await Task.FromResult(this.dbSet.Update(node));
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
