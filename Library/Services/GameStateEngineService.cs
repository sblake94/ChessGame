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
    public BoardModel CurrentBoard { get; set; }
    public TileModel? SelectedTile { get; set; }


    public GameStateEngineService()
    {
        ClearBoard();
        // SetBoardToStartingPositions();
    }

    public BoardModel ClearBoard()
    {
        CurrentBoard = new BoardModel();
        CurrentBoard = DEBUG_POPULATEBOARDWITHPAWNS();
        return CurrentBoard;
    }

    public BoardModel SetBoardToStartingPositions()
    {
        throw new NotImplementedException(nameof(SetBoardToStartingPositions));
    }

    public BoardModel DEBUG_POPULATEBOARDWITHPAWNS()
    {
        foreach(var tile in CurrentBoard)
        {
            tile.OccupyingPiece = new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Pawn);
        }

        return CurrentBoard;
    }

    public void ClickOnTile(int xPos, int yPos)
    {
        TileModel? clickedTile = CurrentBoard
            .Where(tile => tile.xPos == xPos)
            .Where(tile => tile.yPos == yPos)
            .FirstOrDefault();

        if (clickedTile is null) { throw new TileNotFoundException(new TileModel(xPos, yPos)); }

        if (SelectedTile is null) 
        { 
            SelectedTile = clickedTile; 
            return; 
        }

        if(SelectedTile.IsEmpty)
        {
            SelectedTile = clickedTile;
        }

        if (clickedTile.IsEmpty) 
        { 
            CurrentBoard.TransferPieceToEmptyTile(SelectedTile, clickedTile); 
            return; 
        }

        if (clickedTile.IsNotEmpty) 
        {
            // Check Interactions
            throw new NotImplementedException("No logic for landing on occupied spaces");
            return;
        }
    }

    public void SelectTile(TileModel tileModel)
    {
        TileModel matchingTile = CurrentBoard
            .Where(tile => tile.xPos == tileModel.xPos)
            .Where(tile => tile.yPos == tileModel.yPos)
            .First();

        if (matchingTile is null) { throw new TileNotFoundException(tileModel); }
        if (matchingTile == SelectedTile)
        {
            SelectedTile = null;
            return;
        }

        SelectedTile = matchingTile;
    }
}
