using Library.Attributes.ServiceAttributes;
using Library.Exceptions;
using Library.Models;
using System.Numerics;

namespace Library.Services;

[SingletonService]
public class GameStateEngineService
    : ServiceBase<GameStateEngineService>
    , IGameStateEngineService
{
    private readonly INotationService _notationService;

    public BoardModel CurrentBoard { get; set; }
    public TileModel? SelectedTile { get; set; }


    public GameStateEngineService(INotationService notationService)
    {
        _notationService = notationService;

        ClearBoard();
        SetBoardToStartingPositions();
    }

    public void ClearBoard()
    {
        CurrentBoard = new BoardModel();
    }

    public void SetBoardToStartingPositions()
    {
        CurrentBoard = _notationService.GetStartingBoard();
    }

    public void ClickOnTile(int xPos, int yPos)
    {
        TileModel? clickedTile = CurrentBoard
            .Where(tile => tile.xPos == xPos)
            .Where(tile => tile.yPos == yPos)
            .FirstOrDefault();

        var move = new MoveModel(CurrentBoard, SelectedTile, clickedTile);

        if (clickedTile is null) { throw new TileNotFoundException(new TileModel(xPos, yPos)); }

        if (SelectedTile is null || SelectedTile.IsEmpty) 
        { 
            SelectedTile = clickedTile; 
            return; 
        }

        if(move.IsValid)
        {
            move.ExecuteMove();
        }
        }

        SelectedTile = null;
        return;
    }
}
