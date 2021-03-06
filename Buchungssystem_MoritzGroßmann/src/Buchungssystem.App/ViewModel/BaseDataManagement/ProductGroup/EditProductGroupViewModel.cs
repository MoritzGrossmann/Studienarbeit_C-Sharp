﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup
{
    /// <summary>
    /// ViewModel zum Erstellen oder Bearbeiten von Warengruppen
    /// </summary>
    internal class EditProductGroupViewModel : EditViewModel
    { 
        /// <summary>
        /// Repräsentiert die Id der zu bearbeiteten Warengruppe
        /// </summary>
        public int Id;

        private readonly Domain.Model.ProductGroup _productGroup;

        #region Constructor

        /// <summary>
        /// Konstruktor zum Editieren einer vorhandenen Warengruppe
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern der Warengruppe aufgerufen wird</param>
        /// <param name="onDelete">Methode, die beim Löschen einer Warengruppe aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        /// <param name="productGroup">Warengruppe, welche bearbeitet werden soll</param>
        public EditProductGroupViewModel(Action<Domain.Model.ProductGroup> onSave,
            Action<Domain.Model.ProductGroup> onDelete, IPersistBookingSystemData bookingSystemPersistence,
            Domain.Model.ProductGroup productGroup)
        {
            HeaderText = $"{productGroup.Name} bearbeiten";
            BookingSystemPersistence = bookingSystemPersistence;

            _onSave = onSave;
            _onDelete = onDelete;

            _productGroup = productGroup;

            Id = productGroup.Id;
            Name = productGroup.Name;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Where(pg => pg.ChildNodes().All(c => c.GetType() != typeof(Domain.Model.Product))).Select(p => new ProductGroupViewModel(p,null)));

            var parent = (Domain.Model.ProductGroup) productGroup.Parent();

            NoParent = parent.Id == productGroup.Id;

            SelectedProductGroupViewModel =
                ProductGroupViewModels.FirstOrDefault(pwm => pwm.ProductGroup.Id == parent.Id);

            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        /// <summary>
        /// Konstruktor zum Erstellen einer neuen Warengruppe
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern der Warengruppe aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public EditProductGroupViewModel(Action<Domain.Model.ProductGroup> onSave,
            IPersistBookingSystemData bookingSystemPersistence)
        {
            HeaderText = "Neue Warengruppe anlegen";
            BookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;
            
            Name = String.Empty;
            Edit = true;

            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Where(pg => pg.ChildNodes().All(c => c.GetType() != typeof(Domain.Model.Product))).Select(p => new ProductGroupViewModel(p, null)));

            if (ProductGroupViewModels.Any())
                SelectedProductGroupViewModel = ProductGroupViewModels.FirstOrDefault();

            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        #endregion

        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        /// <summary>
        /// Repräsentiert alle Produktgruppen, welche keine Waren als Kind haben
        /// </summary>
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels
        {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        private ProductGroupViewModel _selectedProductGroupViewModel;

        /// <summary>
        /// Repräsentiert die Ausgewählte Warengruppe, die Elterngruppe der zu bearbeiteten Warengruppe ist
        /// </summary>
        public ProductGroupViewModel SelectedProductGroupViewModel
        {
            get => _selectedProductGroupViewModel;
            set => SetProperty(ref _selectedProductGroupViewModel, value, nameof(SelectedProductGroupViewModel));
        }

        private bool _noParent;

        /// <summary>
        /// Zeigt an, ob die Warengruppe keine Elterngruppe hat
        /// </summary>
        public bool NoParent
        {
            get => _noParent;
            set => SetProperty(ref _noParent, value, nameof(NoParent));
        }

        #endregion

        #region Actions

        /// <summary>
        /// Aktion zum Speichern einer Warengruppe
        /// Ruft die im Kontrukor übergebene Methode onSave auf
        /// </summary>
        private async void Save()
        {
            ShowProgressbar = true;

            try
            {
                var productGroup =
                    new Domain.Model.ProductGroup() {Id = Id, Name = Name, Persistence = BookingSystemPersistence};
                productGroup.SetParent(SelectedProductGroupViewModel.ProductGroup ?? productGroup);

                var p = await productGroup.Persist();

                if (NoParent)
                {
                    p.SetParent(p);

                    await p.Persist();
                }
                _onSave.Invoke(p);
            }
            catch (ModelExistException ex)
            {
                AddError(nameof(Name), ex.Message);
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Speichern der Warengruppe", $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                ShowProgressbar = false;
            }

        }

        /// <summary>
        /// Aktion zum Löschen einer Warengruppe
        /// Ruft die im Kontruktor übergebene Methode onDelete auf
        /// </summary>
        private async void Delete()
        {
            var result = await DialogCoordinator.Instance.ShowMessageAsync(this, "Achtung",
                $"Wollen sie die Warengruppe \"{_productGroup.Name}\" wirklich löschen?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                try
                {
                    await _productGroup.Delete();
                    _onDelete(_productGroup);
                }
                catch (Exception ex)
                {
                    await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Löschen der Warengruppe", $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private readonly Action<Domain.Model.ProductGroup> _onSave;

        private readonly Action<Domain.Model.ProductGroup> _onDelete;

        #endregion
    }
}
