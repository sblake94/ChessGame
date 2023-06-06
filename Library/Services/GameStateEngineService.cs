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
    private readonly IMoveBlueprintingService _moveBlueprintingService;

    public BoardModel CurrentBoard { get; set; }
    public TileModel? SelectedTile { get; set; }
    public List<MoveModel> PossibleMoves 
    { 
        get 
        { 
            return SelectedTile is not null 
                ? _moveBlueprintingService.GetAllPossibleMoves(SelectedTile, CurrentBoard) 
                : new List<MoveModel>(); 
        } 
    } 


    public GameStateEngineService(
        INotationService notationService,
        IMoveHistoryService moveHistoryService,
        IMoveBlueprintingService moveBlueprintingService,
        ILoggerFactoryService loggerFactoryService)
        : base(loggerFactoryService)
    {
        _notationService = notationService;
        _moveHistoryService = moveHistoryService;
        _moveBlueprintingService = moveBlueprintingService;

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
            .Where(tile => tile.X == xPos)
            .Where(tile => tile.Y == yPos)
            .FirstOrDefault();


        if (clickedTile is null) { throw new TileNotFoundException(new TileModel(xPos, yPos)); }

        if (SelectedTile is null || SelectedTile.IsEmpty) 
        { 
            SelectedTile = clickedTile; 
            return; 
        }

        var move = new MoveModel(CurrentBoard, SelectedTile, clickedTile);
        if(_moveBlueprintingService.IsValidMove(move))
        {
            move.ExecuteMove();
            _moveHistoryService.LogMove(move);
        }

        SelectedTile = null;
        return;
    }
}
