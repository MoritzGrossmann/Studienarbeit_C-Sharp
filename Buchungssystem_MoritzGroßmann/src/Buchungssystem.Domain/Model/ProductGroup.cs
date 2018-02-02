using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert eine Warengruppe
    /// </summary>
    public class ProductGroup : BookingSystemModel, IComparable<ProductGroup>, IProductNode
    {
        /// <summary>
        /// Eindeutige Id der Warengruppe
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name der Warengruppe
        /// </summary>
        public string Name { get; set; }

        private ICollection<IProductNode> _childs;

        private IProductNode _parent;

        /// <summary>
        /// Speichert die Warengruppe in der Datenbank
        /// </summary>
        /// <returns>Gespeicherte Warengruppe mit Id</returns>
        public async Task<ProductGroup> Persist()
        {
            return await Task.Run(() => Persistence.PersistProductGroup(this));
        }

        /// <summary>
        /// Setzt die Wareengruppe in der Datenbank auf gelöscht
        /// </summary>
        public async Task Delete()
        {
            await Task.Run(() => Persistence.DeleteProductGroup(this));
        }

        /// <summary>
        /// Vergleichsfunktion zum Vergleich der Namen zweier Warengruppen
        /// </summary>
        /// <param name="other">Vergleichswarengruppe</param>
        /// <returns>Negativ, wenn Warengruppe nach der Vergleichswarengruppe im Alphabet kommt, Positiv wenn anders herum. 0 Wenn Namen gleich sind</returns>
        public int CompareTo(ProductGroup other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Gibt alle ChildNodes der Warengurppe zurück
        /// Wenn null dann leere Liste
        /// </summary>
        /// <returns></returns>
        public ICollection<IProductNode> ChildNodes()
        {
            return _childs ?? new List<IProductNode>();
        }

        /// <summary>
        /// Fügt den ChildNodes ein neues IProductNode hinzu
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(IProductNode node)
        {
            if (_childs == null)
                _childs = new List<IProductNode>();
            node.SetParent(this);
            _childs.Add(node);
        }

        /// <summary>
        /// Setzt die Kind-Nodes auf die übergebene Liste
        /// </summary>
        /// <param name="nodes"></param>
        public void SetNodes(ICollection<IProductNode> nodes)
        {
            _childs = nodes;
        }

        /// <summary>
        /// Gibt den Parent der Warengruppe zurück
        /// </summary>
        /// <returns></returns>
        public IProductNode Parent()
        {
            return _parent;
        }

        /// <summary>
        /// Setzt den Parent der Warengruppe auf den übergebenen Node
        /// </summary>
        /// <param name="node">neuer Parent der Ware</param>
        public void SetParent(IProductNode node)
        {
            _parent = node;
        }

        /// <summary>
        /// Zeigt an, ob die Warengruppe ein Blatt im Baum ist
        /// </summary>
        /// <returns></returns>
        public bool IsLeaf()
        {
            return !_childs.Any();
        }

        /// <summary>
        /// Zeigt an, ob es keinen Parent gibt
        /// </summary>
        /// <returns></returns>
        public bool IsRoot()
        {
            return Parent() == null;
        }
        
    }
}