using Library.Models;

namespace Library.Services;

public interface IMoveBlueprintingService
{
    public List<MoveModel> GetAllPossibleMoves(TileModel originTile, BoardModel board);
    public bool IsValidMove(MoveModel move);
}
