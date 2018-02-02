using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert eine Ware
    /// </summary>
    public class Product : BookingSystemModel, IComparable<Product>, IProductNode
    {
        /// <summary>
        /// Eindeutige Id der Ware
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name der Ware
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Preis der Ware
        /// </summary>
        public decimal Price { get; set; }

        private IProductNode _parent;

        /// <summary>
        /// Speichert die Waree in der Datenbank
        /// </summary>
        /// <returns>Gespeicherte ware mit Id</returns>
        public async Task<Product> Persist()
        {
            return await Task.Run(() => Persistence.PersistProduct(this));
        }

        /// <summary>
        /// Setzt die Ware in der Datenbank auf gelöscht
        /// </summary>
        public async Task Delete()
        {
            await Task.Run(() => Persistence.DeleteProduct(this));
        }

        /// <summary>
        /// Vergleichsfunktion zum Vergleich der Namen zweier Waren
        /// </summary>
        /// <param name="other">Vergleichsware</param>
        /// <returns>Negativ, wenn Waren nach der Vergleichsware im Alphabet kommt, Positiv wenn anders herum. 0 Wenn Namen gleich sind</returns>
        public int CompareTo(Product other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Wift /// <summary>
        /// Wift NotSupportedException da Waren keine Kinder haben
        /// </summary>
        /// <returns></returns> da Waren keine Kinder haben
        /// </summary>
        /// <returns></returns>
        public ICollection<IProductNode> ChildNodes()
        {
            throw new NotSupportedException("Products has no child's");
        }

        /// <summary>
        /// Wift NotSupportedException da Waren keine Kinder haben
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(IProductNode node)
        {
            throw new NotSupportedException("Products has no child's");
        }

        /// <summary>
        /// Wift NotSupportedException da Waren keine Kinder haben
        /// </summary>
        /// <param name="nodes"></param>
        public void SetNodes(ICollection<IProductNode> nodes)
        {
            throw new NotSupportedException("Products has no child's");
        }

        /// <summary>
        /// Gibt den Parent der Ware zurück
        /// </summary>
        /// <returns></returns>
        public IProductNode Parent()
        {
            return _parent;
        }

        /// <summary>
        /// Setzt den Parent der Ware auf den übergebenen Node
        /// </summary>
        /// <param name="node">neuer Parent der Ware</param>
        public void SetParent(IProductNode node)
        {
            _parent = node;
        }

        /// <summary>
        /// Immer true, da Waren immer Blätter sind
        /// </summary>
        /// <returns></returns>
        public bool IsLeaf()
        {
            return true;
        }

        /// <summary>
        /// Zeigt an, ob es keinen Parent gibt
        /// </summary>
        /// <returns></returns>
        public bool IsRoot()
        {
            return _parent == null;
        }
    }
}