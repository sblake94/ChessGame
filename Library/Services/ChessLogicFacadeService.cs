using Library.Attributes.ServiceAttributes;
using Library.Models;
using Library.Models.Game;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Library.Services;

[SingletonService]
public class ChessLogicFacadeService
    : ServiceBase<ChessLogicFacadeService>
    , IChessLogicFacadeService
    , INotifyPropertyChanged
{
    private readonly IGameStateEngineService _gameStateEngineService;
    private readonly IMoveBlueprintingService _moveBlueprintingService;
    private readonly IMoveHistoryService _moveHistoryService;
    private readonly INotationService _notationService;

    public event PropertyChangedEventHandler? PropertyChanged;

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

    public ObservableCollection<MoveModel> MoveHistory => _moveHistoryService.MoveHistory;

    private void OnPropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

    public ChessLogicFacadeService(
        IGameStateEngineService gameStateEngineService, 
        IMoveBlueprintingService moveBlueprintingService,
        IMoveHistoryService moveHistoryService,
        INotationService notationService)
        : base()
    {
        _gameStateEngineService = gameStateEngineService;
        _moveBlueprintingService = moveBlueprintingService;
        _moveHistoryService = moveHistoryService;
        _notationService = notationService;

        StartNewGame();

        _gameStateEngineService.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_gameStateEngineService.SelectedTile))
            {
                SelectedTile = _gameStateEngineService.SelectedTile;
                OnPropertyChanged(nameof(SelectedTile));
            }
        };
    }

    public void ClickOnTile(int x, int y)
    {
        _gameStateEngineService.ClickOnTile(x, y);
    }

    public List<MoveModel> GetAllPossibleMoves(TileModel tile, GameModel game)
    {
        return _moveBlueprintingService.GetAllPossibleMoves(tile, game);
    }

    public void StartNewGame()
    {
        CurrentGame
            = _gameStateEngineService.CurrentGame
            = new GameModel(_notationService.GetStartingBoard());
    }
}
