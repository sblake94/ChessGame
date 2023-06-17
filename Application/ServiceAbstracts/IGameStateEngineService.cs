using Domain.Models.Game;
using System.ComponentModel;

namespace Application.ServiceAbstracts
{
    public interface IGameStateEngineService : IServiceBase, INotifyPropertyChanged
    {
        GameModel CurrentGame { get; set; }
        TileModel SelectedTile { get; }

        void ClearBoard();
        void SetBoardToStartingPositions();

        void ClickOnTile(int xPos, int yPos);
    }
}
