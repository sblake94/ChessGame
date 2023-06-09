using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for GameDataView.xaml
    /// </summary>
    public partial class GameDataView 
        : UserControl
    {
        GameDataViewModel ViewModel { get; set; }

        public GameDataView()
        {
            InitializeComponent();

            DataContext = ViewModel = new GameDataViewModel();
        }
    }
}
