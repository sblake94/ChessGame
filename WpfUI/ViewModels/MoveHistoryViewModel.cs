using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Models;
using Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfUI.ViewModels
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
