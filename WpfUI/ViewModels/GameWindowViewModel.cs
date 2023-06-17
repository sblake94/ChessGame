using CommunityToolkit.Mvvm.Input;
using System;

namespace Presentation_WPF.ViewModels;

public partial class GameWindowViewModel
    : ViewModelBase<GameWindowViewModel>
{
    public GameWindowViewModel()
    {
        
    }

    [RelayCommand]
    public void CMD_NewGame()
    {
        _chessLogicFacadeService.StartNewGame();
    }

    [RelayCommand]
    public void CMD_Undo()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    public void CMD_Redo()
    {
        throw new NotImplementedException();
    }
}
