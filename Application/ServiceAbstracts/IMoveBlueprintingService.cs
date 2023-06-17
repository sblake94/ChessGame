using Domain.Models.Game;
using System.Collections.Generic;

namespace Application.ServiceAbstracts
{
    public interface IMoveBlueprintingService : IServiceBase
    {
        List<MoveModel> GetAllPossibleMoves(TileModel originTile, GameModel game);
        bool IsValidMove(MoveModel move);
    }
}