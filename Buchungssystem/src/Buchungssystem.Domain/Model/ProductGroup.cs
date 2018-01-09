using System;
using System.Collections.Generic;
using System.Linq;

namespace Buchungssystem.Domain.Model
{
    public class ProductGroup : BookingSystemModel, IComparable<ProductGroup>, IProductNode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private ICollection<IProductNode> _childs;

        private IProductNode _parent;


        public ProductGroup Persist()
        {
            return Persistence.PersistProductGroup(this);
        }

        public int CompareTo(ProductGroup other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        public ICollection<IProductNode> ChildNodes()
        {
            return _childs ?? new List<IProductNode>();
        }

        public void AddNode(IProductNode node)
        {
            if (_childs == null)
                _childs = new List<IProductNode>();
            node.SetParent(this);
            _childs.Add(node);
        }

        public void SetNodes(ICollection<IProductNode> nodes)
        {
            _childs = nodes;
        }

        public IProductNode Parent()
        {
            return _parent;
        }

        public void SetParent(IProductNode node)
        {
            _parent = node;
        }

        public bool IsLeaf()
        {
            return !_childs.Any();
        }

        public bool IsRoot()
        {
            return Parent() == null;
        }
        
    }
}