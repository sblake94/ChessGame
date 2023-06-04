using System.Configuration;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for GameBoardView.xaml
    /// </summary>
    public partial class GameBoardView : UserControl
    {
        GameBoardViewModel ViewModel { get; set; }

        public GameBoardView()
        {
            InitializeComponent();

            DataContext = ViewModel = new GameBoardViewModel();
        }

        private void TileView_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}
