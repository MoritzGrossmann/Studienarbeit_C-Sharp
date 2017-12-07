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

        public ProductGroup ParentProductGroup { get; set; }

        public ICollection<Product> Products { get; set; }

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
            return _childs;
        }

        public void AddNode(IProductNode node)
        {
            _childs.Add(node);
        }

        public void SetNodes(ICollection<IProductNode> nodes)
        {
            _childs = nodes;
        }

        public IProductNode Parent()
        {
            return ParentProductGroup;
        }

        public bool IsLeaf()
        {
            return !_childs.Any();
        }
    }
}