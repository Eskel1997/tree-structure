using Microsoft.EntityFrameworkCore;
using TREE.DB.Entities;

namespace TREE.DB.DAL
{
    public class TreeContext: DbContext
    {
        public TreeContext(DbContextOptions<TreeContext> options) : base(options)
        {
            
        }

        public DbSet<Node> Nodes { get; set; }
    }
}
