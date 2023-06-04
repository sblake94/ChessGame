using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views;

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
}