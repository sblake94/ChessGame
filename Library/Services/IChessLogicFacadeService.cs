using Library.Models;
using Library.Models.Game;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Library.Services;

public interface IChessLogicFacadeService : INotifyPropertyChanged
{
    public TileModel? SelectedTile { get; }
    public GameModel? CurrentGame { get; }
    public ObservableCollection<MoveModel> MoveHistory { get; }

    void ClickOnTile(int x, int y);
    public List<MoveModel> GetAllPossibleMoves(TileModel tile, GameModel game);

}
