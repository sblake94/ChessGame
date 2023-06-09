using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for GameWindowView.xaml
    /// </summary>
    public partial class GameWindowView : Window
    {
        GameWindowViewModel ViewModel { get; set; }

        public GameWindowView()
        {
            InitializeComponent();

            DataContext = ViewModel = new GameWindowViewModel();
        }
    }
}
