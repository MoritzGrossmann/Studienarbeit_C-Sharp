using System;
using System.Data.SqlServerCe;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Product;
using Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup;
using Buchungssystem.App.ViewModel.BaseDataManagement.Room;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    /// <summary>
    /// ViewModel für die Stammdatenverwaltung
    /// </summary>
    internal class BaseDataManagementViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Standardkonstruktor für die Stammdatenverwaltung
        /// </summary>
        /// <param name="bookingSystemDataPersistence">Repräsentiert den Datenbankkontext</param>
        public BaseDataManagementViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            RoomsViewModel = new ChangeRoomsViewModel(bookingSystemDataPersistence);
            ProductGroupsViewModel = new ChangeProductGroupsViewModel(bookingSystemDataPersistence);
            ProductsViewModel = new ChangeProductsViewModel(bookingSystemDataPersistence);
        }

        #endregion

        #region Properties

        /// <summary>
        /// ViewModel zum Editieren von Räumen und deren Tische
        /// </summary>
        public BaseViewModel RoomsViewModel { get; set; }

        /// <summary>
        /// ViewModel zum Editieren von Waren
        /// </summary>
        public BaseViewModel ProductsViewModel { get; set; }

        /// <summary>
        /// ViewModel zum Editieren von Warengruppen
        /// </summary>
        public BaseViewModel ProductGroupsViewModel { get; set; }

        #endregion
    }
}
