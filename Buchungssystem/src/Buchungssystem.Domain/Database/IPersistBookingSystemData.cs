using System;
using System.Collections.Generic;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Domain.Database
{
    /// <summary>
    /// Interface für den Datenbankzugriff
    /// </summary>
    public interface IPersistBookingSystemData
    {
        /// <summary>
        /// Gibt alle Räume aus der Datenbank zurück, welche nicht als gelöscht markiert sind
        /// </summary>
        /// <returns>Liste aller existenten Räume</returns>
        List<Room> Rooms();

        /// <summary>
        /// Speichert Raum in der Datenbank
        /// </summary>
        /// <param name="room">Raum, welcher gespeichert werden soll</param>
        /// <returns>Gespiecherter Raum mit Id</returns>
        Room PersistRoom(Room room);

        /// <summary>
        /// Setzt übergebenen in der Datenbank auf gelöscht
        /// </summary>
        /// <param name="room">Raum, welcher gelöscht werden soll</param>
        void DeleteRoom(Room room);

        /// <summary>
        /// Setzt übergebenen Tisch in der Datenbank als gelöscht
        /// </summary>
        /// <param name="table">Tisch, elcher gelöscht werden soll</param>
        void DeleteTable(Table table);

        /// <summary>
        /// Speichert Tisch in der Datenbank
        /// </summary>
        /// <param name="table">Tisch, welcher gespeichert werden soll</param>
        /// <returns>Gespiecherter Tisch mit Id</returns>
        Table PersistTable(Table table);

        /// <summary>
        /// Gibt alle Warengruppen aus der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        List<ProductGroup> ProductGroups();

        /// <summary>
        /// Gibt alle Warengruppen aus der Datenbank zurück, welche keine Kind-Elemente besitzen
        /// </summary>
        /// <returns></returns>
        List<ProductGroup> LeafProductGroups();
        
        /// <summary>
        /// Speichert Warengruppe in der Datenbank
        /// </summary>
        /// <param name="productGroup">Warengruppe, welche gespeichert werden soll</param>
        /// <returns></returns>
        ProductGroup PersistProductGroup(ProductGroup productGroup);

        /// <summary>
        /// Setzt übergebene Warengruppe in der Datenbank als gelöscht
        /// </summary>
        /// <param name="productGroup"></param>
        void DeleteProductGroup(ProductGroup productGroup);

        /// <summary>
        /// Gibt alle Waren aus der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        List<Product> Products();

        /// <summary>
        /// Speichert Ware in der Datenbank
        /// </summary>
        /// <param name="product">Ware, welche gespeichert werden soll</param>
        /// <returns>gespeicherte Ware mit Id</returns>
        Product PersistProduct(Product product);

        /// <summary>
        /// Setzt übergebene Ware in der Datenbank als gelöscht
        /// </summary>
        /// <param name="product"></param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Speichert eine Buchung in der Datenbank
        /// </summary>
        /// <param name="booking">Buchung, welche gespeichert werden soll</param>
        /// <returns>Buchung mit Id</returns>
        Booking Book(Booking booking);


        /// <summary>
        /// Gibt alle Buchungen eines Tages aus
        /// </summary>
        /// <param name="date">Tag der Buchungen</param>
        /// <returns></returns>
        List<Booking> Bookings(DateTime date);

        /// <summary>
        /// Setzt den Status einer Buchung in der Datenbank auf Stoerniert
        /// </summary>
        /// <param name="booking">Stornierte Buchung</param>
        void Cancel(Booking booking);

        /// <summary>
        /// Setzt den Status einer Buchung in der Datenbank auf Bezahlt
        /// </summary>
        /// <param name="booking">Bezahlte Buchung</param>
        void Pay(Booking booking);

        /// <summary>
        /// Setzt den Status eine Tisches in der Datenbank auf Besetzt
        /// </summary>
        /// <param name="table">Besetzter Tisch</param>
        void Occupy(Table table);

        /// <summary>
        /// Setzt den Status eine Tisches in der Datenbank auf Frei
        /// </summary>
        /// <param name="table">Freier Tisch</param>
        void Clear(Table table);
    }
}
