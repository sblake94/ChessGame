using Domain.Models.Game;
using System.Collections.ObjectModel;

namespace Application.ServiceAbstracts
{
    public interface IMoveHistoryService : IServiceBase
    {
        ObservableCollection<MoveModel> MoveHistory { get; }
        void LogMove(MoveModel move);

        void ClearHistory();
    }
}