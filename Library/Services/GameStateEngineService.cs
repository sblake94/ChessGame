using Library.Attributes.ServiceAttributes;
using Library.Exceptions;
using Library.Logging;
using Library.Models;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace Library.Services;

[SingletonService]
public class GameStateEngineService
    : ServiceBase<GameStateEngineService>
    , IGameStateEngineService
{
    private readonly INotationService _notationService;
    private readonly IMoveHistoryService _moveHistoryService;

    public BoardModel CurrentBoard { get; set; }
    public TileModel? SelectedTile { get; set; }


    public GameStateEngineService(
        INotationService notationService,
        IMoveHistoryService moveHistoryService,
        ILoggerFactoryService loggerFactoryService)
        : base(loggerFactoryService)
    {
        _notationService = notationService;
        _moveHistoryService = moveHistoryService;

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
            _moveHistoryService.LogMove(move);
        }

        SelectedTile = null;
        return;
    }
}
