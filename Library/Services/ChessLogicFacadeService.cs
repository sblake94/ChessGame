using Library.Attributes.ServiceAttributes;
using Library.Models;
using System.Collections.ObjectModel;

namespace Library.Services;

[SingletonService]
public class ChessLogicFacadeService
    : ServiceBase<ChessLogicFacadeService>
    , IChessLogicFacadeService
{
    private readonly IGameStateEngineService _gameStateEngineService;
    private readonly IMoveBlueprintingService _moveBlueprintingService;
    private readonly IMoveHistoryService _moveHistoryService;
    private readonly INotationService _notationService;

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
    }

    public TileModel? SelectedTile => _gameStateEngineService.SelectedTile;

    public BoardModel? CurrentBoard => _gameStateEngineService.CurrentBoard;

    public ObservableCollection<MoveModel> MoveHistory => _moveHistoryService.MoveHistory;

    public void ClickOnTile(int x, int y)
    {
        _gameStateEngineService.ClickOnTile(x, y);
    }

    public List<MoveModel> GetAllPossibleMoves(TileModel tile, BoardModel board)
    {
        return _moveBlueprintingService.GetAllPossibleMoves(tile, board);
    }
}
