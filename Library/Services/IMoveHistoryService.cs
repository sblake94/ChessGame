using Library.Models.Game;
using System.Collections.ObjectModel;

namespace Library.Services;

public interface IMoveHistoryService
{
    public ObservableCollection<MoveModel> MoveHistory { get; }
    public void LogMove(MoveModel move);

    public void ClearHistory();
}
