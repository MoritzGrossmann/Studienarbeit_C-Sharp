using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.RoomView
{
    internal class TableListViewModel : BaseViewModel
    {
        private ObservableCollection<TableViewModel> _tableViewModels;

        public ObservableCollection<TableViewModel> TableViewModels
        {
            get { return _tableViewModels; }
            set { _tableViewModels = value; }
        }

        public TableListViewModel(ICollection<Table> tables)
        {
            TableViewModels = new ObservableCollection<TableViewModel>(tables.Select(t => new TableViewModel(t)));
        }

        public TableListViewModel()
        {
            TableViewModels = new ObservableCollection<TableViewModel>(new TestPersitence().Tables().Select(t => new TableViewModel(t)));
        }
    }
}
