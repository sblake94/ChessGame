using Domain.Models.Data.ValueTypes;
using Domain.Models.Game;

namespace Application.ServiceAbstracts
{
    public interface INotationService : IServiceBase
    {
        BoardModel GetStartingBoard();
        FENString ToFen(BoardModel board);
    }
}