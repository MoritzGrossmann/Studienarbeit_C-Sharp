using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
