using Library.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Library.Services;

public interface IChessLogicFacadeService : INotifyPropertyChanged
{
    public TileModel? SelectedTile { get; }
    public BoardModel? CurrentBoard { get; }
    public ObservableCollection<MoveModel> MoveHistory { get; }

    void ClickOnTile(int x, int y);
    public List<MoveModel> GetAllPossibleMoves(TileModel tile, BoardModel board);

}
