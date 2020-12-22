using System.Collections.Generic;

namespace TREE.WEB.ViewModels
{
    public class NodeViewModel : AbstractViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<NodeViewModel> Nodes { get; set; }
    }
}
