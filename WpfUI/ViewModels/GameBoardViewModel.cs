using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public partial class GameBoardViewModel 
    : ViewModelBase<GameBoardViewModel>
{
    public ICommand OnTileClicked { get; }

    public GameBoardViewModel()
    {
        OnTileClicked = new RelayCommand<string>(Handle_OnTileClicked);
    }

    [RelayCommand]
    private void Handle_OnTileClicked(string tileID)
    {
        Debug.WriteLine($"Tile Clicked: {tileID}");
    }
}
