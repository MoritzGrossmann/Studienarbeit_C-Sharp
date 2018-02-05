using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using Buchungssystem.App.ViewModel;
using Buchungssystem.Repository.Database;

namespace Buchungssystem.App.UI.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
       public MainWindow()
        {
            LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            InitializeComponent();
            DataContext = new MainViewModel(new BookingSystemDataPersitence());
        }
    }
}
