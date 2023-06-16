using Library.Attributes.ServiceAttributes;
using Library.Models;
using Library.Models.Game;
using System.ComponentModel;

namespace Library.Services;


public interface IGameStateEngineService : INotifyPropertyChanged
{
    public GameModel CurrentGame { get; }
    public TileModel SelectedTile { get; }

    public void ClearBoard();
    public void SetBoardToStartingPositions();

    public void ClickOnTile(int xPos, int yPos);
}
