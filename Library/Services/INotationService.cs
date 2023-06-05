using Library.Models;

namespace Library.Services;

public interface INotationService
{
    public BoardModel GetStartingBoard();
    string ToFen(BoardModel board);
}
