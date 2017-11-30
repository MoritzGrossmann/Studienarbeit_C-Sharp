using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.SubViewModels.BaseDataManagement
{
    internal class BaseDataManagementViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersistence;

        #region Contructor

        public BaseDataManagementViewModel() : this(new TestPersitence())
        {
            
        }

        public BaseDataManagementViewModel(IPersistBaseData baseDataPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            CurrentViewModel = new ChangeRoomsViewModel(_baseDataPersistence);
        }

        #endregion

        #region Properties

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel == value) return;
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        #endregion

        #region Actions

        private void SetViewModel(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        #endregion
    }
}
