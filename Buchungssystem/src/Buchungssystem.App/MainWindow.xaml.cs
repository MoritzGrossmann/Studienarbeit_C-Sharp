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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Buchungssystem.App.ViewModel;
using Buchungssystem.Repository;

namespace Buchungssystem.App
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //new StammdatenPersistenz().Raeume().ForEach(raum => RaumView.Items.Add(new TabItem() {Header = raum.Name, Tag = raum}));
        }
    }
}
