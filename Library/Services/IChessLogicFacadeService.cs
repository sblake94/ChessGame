using Library.Attributes.ServiceAttributes;
using Library.Models;
using System.Collections.ObjectModel;

namespace Library.Services;

public interface IChessLogicFacadeService
{
    public TileModel? SelectedTile { get; }
    public BoardModel? CurrentBoard { get; }
    public ObservableCollection<MoveModel> MoveHistory { get; }

    void ClickOnTile(int x, int y);
    public List<MoveModel> GetAllPossibleMoves(TileModel tile, BoardModel board);

}
