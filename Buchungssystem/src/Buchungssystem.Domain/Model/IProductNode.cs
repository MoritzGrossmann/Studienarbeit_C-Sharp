using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    public interface IProductNode
    {
        ICollection<IProductNode> ChildNodes();

        void AddNode(IProductNode node);

        void SetNodes(ICollection<IProductNode> nodes);

        IProductNode Parent();

        void SetParent(IProductNode node);

        bool IsLeaf();

        bool IsRoot();
    }
}
