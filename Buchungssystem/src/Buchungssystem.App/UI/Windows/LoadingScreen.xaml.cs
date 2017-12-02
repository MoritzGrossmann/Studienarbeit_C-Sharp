using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Database;
using MahApps.Metro.Controls;

namespace Buchungssystem.App.UI.Windows
{
    /// <summary>
    /// Interaktionslogik für LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : MetroWindow
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        public async Task<ICollection<Room>> GetRoomsAsync()
        {
            return await Task.Run(() => new BookingSystemDataPersitence().Rooms());
        }
    }
}
