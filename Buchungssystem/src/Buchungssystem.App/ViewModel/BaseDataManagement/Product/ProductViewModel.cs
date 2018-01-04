using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.Domain.Model;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<ProductGroup> ProductGroups { get; }

        public Domain.Model.Product Product { get; set; }

        private bool _edit;

        /// <summary>
        /// Zeigt an, ob der Raumname gerade Editiert wird
        /// </summary>
        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        /// <summary>
        /// Zeigt an, ib der Raumname gerade nicht editiert wird
        /// </summary>
        public bool NoEdit => !Edit;

        /// <summary>
        /// Konstruktor für das Auswählen
        /// </summary>
        /// <param name="product"></param>
        /// <param name="onSelect"></param>
        public ProductViewModel(Domain.Model.Product product, Action<Domain.Model.Product> onSelect)
        {
            Product = product;
            _select = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        /// <summary>
        /// Konstruktor für das Editieren
        /// </summary>
        /// <param name="room"></param>
        /// <param name="onSave"></param>
        public ProductViewModel(Domain.Model.Product product, EventHandler<Domain.Model.Product> onSave, List<ProductGroup> productGroups)
        {
            Product = product;
            _onSave = onSave;

            ProductGroups = new ObservableCollection<ProductGroup>(productGroups);
            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToogleEdit);
        }

        private void ToogleEdit()
        {
            Edit = !Edit;
            RaisePropertyChanged(nameof(NoEdit));
        }

        private readonly EventHandler<Domain.Model.Product> _onSave;

        /// <summary>
        /// Lößt den Eventhandler _onSave aus
        /// Speichert den Raum in der Datenbank
        /// Ändert die Editieransicht auf "Nicht editieren"
        /// </summary>
        private void Save()
        {
            _onSave?.Invoke(this, Product);
            ToogleEdit();
        }

        private void Select()
        {
            _select?.Invoke(Product);
        }

        private readonly Action<Domain.Model.Product> _select;

        #region Actions


        #endregion

        #region Commands

        /// <summary>
        /// Kommando beim Speichern eines Produktes
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Kommando bei Auswahl eines Produktes
        /// </summary>
        public ICommand SelectCommand { get; }

        /// <summary>
        /// erlaubt das Editieren des Produktes
        /// </summary>
        public ICommand EditCommand { get; }

        #endregion

    }
}
