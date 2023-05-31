using System.Windows;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for GameWindowView.xaml
    /// </summary>
    public partial class GameWindowView : Window
    {
        public GameWindowView()
        {
            InitializeComponent();

            GameBoard = new GameBoardView();
        }
    }
}
