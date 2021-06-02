using Atom.Windows.Controls;

using System.Windows;

namespace iPetros
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set the view model
            DataContext = new WindowViewModel(this)
            {
                Title = "iPetros",
                IsMain = true
            };

            Content = new iPetrosApplicationPagesContainer();
        }
    }
}
