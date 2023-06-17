using Application.ServiceAbstracts;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Presentation_WPF.ViewModels;

public partial class GameBoardViewModel
    : ViewModelBase<GameBoardViewModel>
{
    public GameBoardViewModel()
    {
        // TODO: This should be done via constructor injection, but it works for now
        _chessLogicFacadeService = Ioc.Default.GetRequiredService<IChessLogicFacadeService>();
    }
}
