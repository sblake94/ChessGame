using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Models;
using Library.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfUI.ViewModels
{
    public class MoveHistoryViewModel
    {
        private readonly IMoveHistoryService _moveHistoryService;

        
        public ObservableCollection<MoveModel> MoveHistory
        {
            get
            {
                return _moveHistoryService.MoveHistory;
            }
        }

        public MoveHistoryViewModel()
        {
            _moveHistoryService = Ioc.Default.GetRequiredService<IMoveHistoryService>();
        }
    }
}
