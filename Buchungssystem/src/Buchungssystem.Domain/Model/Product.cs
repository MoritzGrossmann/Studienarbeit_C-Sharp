using System;
using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    public class Product : BookingSystemModel, IComparable<Product>, IProductNode
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        private IProductNode _parent;

        public Product Persist()
        {
            return Persistence.PersistProduct(this);
        }

        public int CompareTo(Product other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        public ICollection<IProductNode> ChildNodes()
        {
            throw new NotSupportedException("Products has no child's");
        }

        public void AddNode(IProductNode node)
        {
            throw new NotSupportedException("Products has no child's");
        }

        public void SetNodes(ICollection<IProductNode> nodes)
        {
            throw new NotSupportedException("Products has no child's");
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
            return true;
        }

        public bool IsRoot()
        {
            return _parent == null;
        }
    }
}