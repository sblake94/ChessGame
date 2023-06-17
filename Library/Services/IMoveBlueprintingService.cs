using Library.Models.Game;

namespace Library.Services;

public interface IMoveBlueprintingService
{
    public List<MoveModel> GetAllPossibleMoves(TileModel originTile, GameModel game);
    public bool IsValidMove(MoveModel move);
}
