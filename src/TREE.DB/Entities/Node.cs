using System;
using Castle.Core.Internal;
using System.Collections.Generic;
using TREE.DB.Exceptions;

namespace TREE.DB.Entities
{
    public class Node
    {
        public long Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime AddedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
        public virtual List<Node> Nodes { get; protected set; }
        public long? ParentId { get; protected set; }
        public virtual Node Parent { get; protected set; }
        public Node(string name, long? parentId)
        {
            AddedAt = DateTime.Now;
            ChangeName(name);
            ChangeParentId(parentId);
        }

        public Node()
        {

        }

        public void Update(string name, long? parentId)
        {
            this.ModifiedAt = DateTime.Now;
            ChangeName(name);
            ChangeParentId(parentId);
        }
        public void AddNodes(List<Node> nodes)
        {
            Nodes.AddRange(nodes);
        }
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
        private void ChangeName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new TreeException(ErrorCode.BadRequest);
            }

            this.Name = name;
        }
        private void ChangeParentId(long? parentId)
        {
            this.ParentId = parentId;
        }
    }
}
