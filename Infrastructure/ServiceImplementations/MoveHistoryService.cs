using Application.ServiceAbstracts;
using Infrastructure.Attributes.ServiceAttributes;
using Domain.Models.Game;
using System.Collections.ObjectModel;

namespace Infrastructure.ServiceImplementations
{
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
}