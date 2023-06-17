using System.Windows.Controls;
using Presentation_WPF.ViewModels;

namespace Presentation_WPF.Views;

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