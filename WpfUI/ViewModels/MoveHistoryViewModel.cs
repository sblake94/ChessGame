using Application.ServiceAbstracts;
using CommunityToolkit.Mvvm.DependencyInjection;
using Domain.Models.Game;
using System.Collections.ObjectModel;

namespace Presentation_WPF.ViewModels
{
    public class MoveHistoryViewModel
    {
        private readonly IChessLogicFacadeService _chessLogicFacadeService;

        public ObservableCollection<MoveModel> MoveHistory
        {
            get
            {
                return _chessLogicFacadeService.MoveHistory;
            }
        }

        public MoveHistoryViewModel()
        {
            _chessLogicFacadeService = Ioc.Default.GetRequiredService<IChessLogicFacadeService>();
        }
    }
}
