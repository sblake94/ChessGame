using Library.Attributes.ServiceAttributes;
using Library.Models.Game;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace Library.Services;

[SingletonService]
public class MoveHistoryService 
    : ServiceBase<MoveHistoryService>
    , IMoveHistoryService
{
    public ObservableCollection<MoveModel> MoveHistory { get; } = new ObservableCollection<MoveModel>();

    public MoveHistoryService(ILoggerFactoryService loggerFactoryService)
        : base(loggerFactoryService)
    {
        
    }

    public void ClearHistory()
    {
        MoveHistory.Clear();
    }

    public void LogMove(MoveModel move)
    {
        MoveHistory.Add(move);
    }
}
