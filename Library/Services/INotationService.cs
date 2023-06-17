using Library.Models.Game;

namespace Library.Services;

public interface INotationService
{
    public BoardModel GetStartingBoard();
    string ToFen(BoardModel board);
}
