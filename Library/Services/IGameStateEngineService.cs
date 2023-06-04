using Library.Attributes.ServiceAttributes;
using Library.Models;

namespace Library.Services;


public interface IGameStateEngineService
{
    public BoardModel CurrentBoard { get; }
    public TileModel SelectedTile { get; }

    public BoardModel ClearBoard();
    public BoardModel SetBoardToStartingPositions();

    public void ClickOnTile(int xPos, int yPos);
    public void SelectTile(TileModel tileModel);
}
