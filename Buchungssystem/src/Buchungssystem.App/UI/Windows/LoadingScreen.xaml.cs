using System.Collections.Generic;
using Buchungssystem.App.ViewModel.Loading;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Database;

namespace Buchungssystem.App.UI.Windows
{
    /// <summary>
    /// Interaktionslogik für LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen
    {
        public LoadingScreen()
        {
            InitializeComponent();
            DataContext = new LoadingViewModel(new BookingSystemDataPersitence(), OnDataLoaded);
        }

        private void OnDataLoaded(object sender, ICollection<Room> rooms)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
