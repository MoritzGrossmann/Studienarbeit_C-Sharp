using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert einen Knoten in einer Warengruppen-Waren-Baumstruktur
    /// </summary>
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
