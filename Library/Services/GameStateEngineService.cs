using Library.Attributes.ServiceAttributes;
using Library.Exceptions;
using Library.Logging;
using Library.Models.Data.Result;
using Library.Models.Game;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Numerics;

namespace Library.Services;

[SingletonService]
public class GameStateEngineService
    : ServiceBase<GameStateEngineService>
    , IGameStateEngineService
    , INotifyPropertyChanged
{
    private readonly INotationService _notationService;
    private readonly IMoveHistoryService _moveHistoryService;
    private readonly IMoveBlueprintingService _moveBlueprintingService;

    public event PropertyChangedEventHandler? PropertyChanged;

    private GameModel _currentGame;
    public GameModel CurrentGame 
    {
        get { return _currentGame; }
        set
        {
            if(_currentGame != value)
            {
                _currentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }
    }

    private TileModel? _selectedTile;
    public TileModel? SelectedTile 
    {
        get { return _selectedTile; }
        set 
        { 
            if(_selectedTile != value)
            {
                _selectedTile = value;
                OnPropertyChanged(nameof(SelectedTile));
            }
        }
    }

    public List<MoveModel> PossibleMoves 
    { 
        get 
        { 
            return SelectedTile is not null 
                ? _moveBlueprintingService.GetAllPossibleMoves(SelectedTile, CurrentGame) 
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

        CurrentGame = new GameModel(_notationService.GetStartingBoard());

        CurrentGame.SecondPlayerScoreChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(CurrentGame));
        };

        CurrentGame.FirstPlayerScoreChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(CurrentGame));
        };
    }

    private void OnPropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v)); 
    }

    public void ClearBoard()
    {
        CurrentGame.Board = new BoardModel();
    }

    public void SetBoardToStartingPositions()
    {
        CurrentGame.Board = _notationService.GetStartingBoard();
    }

    public void ClickOnTile(int xPos, int yPos)
    {
        // If the game is already over, don't allow any more moves
        if (CurrentGame.Winner is not null) { return; }

        TileModel? clickedTile = CurrentGame.Board
            .Where(tile => tile.X == xPos)
            .Where(tile => tile.Y == yPos)
            .FirstOrDefault();

        if (clickedTile is null) { throw new TileNotFoundException(new TileModel(xPos, yPos)); }

        if (SelectedTile is null || SelectedTile.IsEmpty) 
        { 
            SelectedTile = clickedTile; 
            return; 
        }

        var move = new MoveModel(CurrentGame, CurrentGame.ActivePlayer, SelectedTile, clickedTile);
        if(_moveBlueprintingService.IsValidMove(move))
        {
            move.ExecuteMove();
            Result<MoveOutcome> outcome = move.DetermineOutcome();
            if (!outcome.IsSuccess) { throw outcome.Exception; }

            switch(outcome.Value)
            {
                case MoveOutcome.CaptureKing:
                case MoveOutcome.Checkmate:
                    {
                        CurrentGame.GameOver(move.ActivePlayer);
                        break;
                    }

                case MoveOutcome.Check:
                    {
                        // Handle check state
                        break;
                    }

                default: break;
            }

            _moveHistoryService.LogMove(move);
            CurrentGame.EndTurn();
        }

        SelectedTile = null;
        return;
    }
}
