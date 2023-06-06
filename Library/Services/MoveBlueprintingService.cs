using Library.Attributes.ServiceAttributes;
using Library.Models;

namespace Library.Services;

[SingletonService]
public class MoveBlueprintingService
    : ServiceBase<MoveBlueprintingService>
    , IMoveBlueprintingService
{

    public MoveBlueprintingService(
        ILoggerFactoryService loggerFactoryService) 
        : base(loggerFactoryService)
    {
        
    }

    public List<MoveModel> GetAllPossibleMoves(TileModel originTile, BoardModel board)
    {
        List<MoveModel> result = new List<MoveModel>();

        // We only iterate over tiles that exist on the board
        // So we don't have to check for nulls
        for (int targetY = 0; targetY < 8; targetY++){
        for (int targetX = 0; targetX < 8; targetX++)
        {
            var destinationTile = board[targetY, targetX];
            if (DestinationIsReachableByPiece(originTile, destinationTile, board))
            {
                result.Add(new MoveModel(board, originTile, destinationTile));
            }
        }}

        return result;
    }

    public bool IsValidMove(MoveModel move)
    {
        var allPossibleMoves = GetAllPossibleMoves(move.OriginTile, move.Board);

        bool valid = allPossibleMoves
            .Any(possibleMove => possibleMove.OriginTile == move.OriginTile 
            && possibleMove.DestinationTile == move.DestinationTile);
                
        return valid;
    }

    public bool DestinationIsReachableByPiece(TileModel originTile, TileModel destinationTile, BoardModel board)
    {
        var unit = originTile.OccupyingPiece.MyUnit;
        var team = originTile.OccupyingPiece.MyTeam;
        int direction = team == PieceModel.TeamType.White ? 1 : -1;

        if (originTile.ClassicCoords == destinationTile.ClassicCoords) { return false; }

        switch (unit)
        {
            // Do move logic for each unit type
            case PieceModel.UnitType.Pawn:      return PawnMoveLogic(originTile, destinationTile, board, team, direction);
            case PieceModel.UnitType.Rook:      return RookMoveLogic(originTile, destinationTile, board, team);
            case PieceModel.UnitType.Knight:    return KnightMoveLogic(originTile, destinationTile, board, team);
            case PieceModel.UnitType.Bishop:    return BishopMoveLogic(originTile, destinationTile, board, team);
            case PieceModel.UnitType.Queen:     return QueenMoveLogic(originTile, destinationTile, board, team);
            case PieceModel.UnitType.King:      return KingMoveLogic(originTile, destinationTile, board, team);

            default:
                return false;
        }
    }

    private bool KingMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team)
    {
        if (GetDestinationTileIsOccupiedByAlly(destinationTile, team)) { return false; }
        var distanceVector = GetDistanceVector(originTile, destinationTile);

        // Diagonal move
        if (Math.Abs(distanceVector[0]) == 1 && Math.Abs(distanceVector[1]) == 1) { return true; }

        // Straight Horizontal move
        if (Math.Abs(distanceVector[0]) == 1 && Math.Abs(distanceVector[1]) == 0) { return true; }

        // Straight Vertical move
        if (Math.Abs(distanceVector[0]) == 0 && Math.Abs(distanceVector[1]) == 1) { return true; }

        return false;
    }

    private bool QueenMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team)
    {
        bool isValidDiagonalMove = BishopMoveLogic(originTile, destinationTile, board, team);
        bool isValidStraightMove = RookMoveLogic(originTile, destinationTile, board, team);
        return isValidDiagonalMove || isValidStraightMove;
    }

    private bool PawnMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team, int direction)
    {
        bool canDoFirstMove = 
            GetIsOnStartingTile(originTile) && 
            GetDestinationIsNTilesAhead(originTile, destinationTile, 2, direction) &&
            GetDestinationTileIsClear(destinationTile);

        bool canMoveForward = 
            GetDestinationIsNTilesAhead(originTile, destinationTile, 1, direction) &&
            GetDestinationTileIsClear(destinationTile);

        bool canTakePieceToWest = 
            GetDestinationIsNTilesDiagonalAheadWest(originTile, destinationTile, 1, direction) &&
            GetDestinationTileIsOccupiedByEnemy(destinationTile, team);

        bool canTakePieceToEast =
            GetDestinationIsNTilesDiagonalAheadEast(originTile, destinationTile, 1, direction) && 
            GetDestinationTileIsOccupiedByEnemy(destinationTile, team);

        bool canPerformEnPassantWest =
            GetEnemyUnitNTilesWest(originTile, board, 1, team) &&
            GetDestinationIsNTilesDiagonalAheadWest(originTile, destinationTile, 1, direction) && 
            GetDestinationTileIsClear(destinationTile);

        bool canPerformEnPassantEast =
            GetEnemyUnitNTilesEast(originTile, board, 1, team) &&
            GetDestinationIsNTilesDiagonalAheadEast(originTile, destinationTile, 1, direction) && 
            GetDestinationTileIsClear(destinationTile);

        return
            canDoFirstMove
            || canMoveForward
            || canTakePieceToWest
            || canTakePieceToEast
            || canPerformEnPassantWest
            || canPerformEnPassantEast;
    }

    private bool RookMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team)
    {
        if (GetDestinationTileIsOccupiedByAlly(destinationTile, team)) { return false; };

        bool pathIsClearAndValid = GetIsStraightPathClear(originTile, destinationTile, board);        

        return pathIsClearAndValid;
    }

    private bool KnightMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team)
    {
        // Check if the destination tile is occupied by an ally
        if (GetDestinationTileIsOccupiedByAlly(destinationTile, team)) { return false; }

        // Get the distance vector between the origin and destination tiles
        var distanceVector = GetDistanceVector(originTile, destinationTile);

        // Check if the destination tile is 2 tiles away in one direction and 1 tile away in the other direction
        if (Math.Abs(distanceVector[0]) == 2 && Math.Abs(distanceVector[1]) == 1) { return true; }
        if (Math.Abs(distanceVector[0]) == 1 && Math.Abs(distanceVector[1]) == 2) { return true; }

        return false;
    }

    private bool BishopMoveLogic(TileModel originTile, TileModel destinationTile, BoardModel board, PieceModel.TeamType team)
    {
        // Check if the destination tile is occupied by an ally
        if (GetDestinationTileIsOccupiedByAlly(destinationTile, team)) { return false; }

        // Get the distance vector between the origin and destination tiles
        var distanceVector = GetDistanceVector(originTile, destinationTile);

        // Check if the destination tile is diagonal to the origin tile, store it in a variable
        bool isDiagonal = Math.Abs(distanceVector[0]) == Math.Abs(distanceVector[1]);
        
        // Check if the path is clear
        bool pathIsClearAndValid = GetIsDiagonalPathClear(originTile, destinationTile, board);

        return isDiagonal && pathIsClearAndValid;
    }

    private bool GetIsDiagonalPathClear(TileModel originTile, TileModel destinationTile, BoardModel board)
    {
        // Get the distance vector between the origin and destination tiles
        var distanceVector = GetDistanceVector(originTile, destinationTile);

        // Check if the destination tile is diagonal to the origin tile, store it in a variable
        if (Math.Abs(distanceVector[0]) != Math.Abs(distanceVector[1])) { return false; }
        
       
        // Iterate over the tiles between the origin and destination tiles
        for (int i = 1; i < Math.Abs(distanceVector[0]); i++)
        {
            // Get the tile at the current iteration
            var currentTile = board[originTile.X + i * Math.Sign(distanceVector[0]), originTile.Y + i * Math.Sign(distanceVector[1])];

            // Check if the current tile is occupied
            if (!currentTile.IsEmpty) { return false; }
        }

        return true;
    }    

    private bool GetIsStraightLineToDestination(TileModel originTile, TileModel destinationTile)
    {
        var distanceVector = GetDistanceVector(originTile, destinationTile);
        return distanceVector[0] == 0 || distanceVector[1] == 0;
    }

    private bool GetIsStraightPathClear(TileModel originTile, TileModel destinationTile, BoardModel board)
    {
        // Check if the destination tile is in the same row or column as the origin tile
        if (!GetIsStraightLineToDestination(originTile, destinationTile)) { return false; }

        // Get the distance vector between the origin and destination tiles
        var distanceVector = GetDistanceVector(originTile, destinationTile);

        // Iterate over the tiles between the origin and destination tiles
        for (int i = 1; i < Math.Max(Math.Abs(distanceVector[0]), Math.Abs(distanceVector[1])); i++)
        {
            // Get the tile at the current iteration
            var currentTile = board[originTile.X + i * Math.Sign(distanceVector[0]), originTile.Y + i * Math.Sign(distanceVector[1])];
            
            // Check if the tile is occupied
            if (currentTile.OccupyingPiece != PieceModel.None)
            {
                // If the tile is occupied, return false
                return false;
            }
        }

        return true;
    }

    private int[] GetDistanceVector(TileModel originTile, TileModel destinationTile)
    {
        var result = new int[2];
        result[0] = destinationTile.X - originTile.X;
        result[1] = destinationTile.Y - originTile.Y;
        return result;
    }

    private static bool GetEnemyUnitNTilesEast(TileModel originTile, BoardModel board, int distance, PieceModel.TeamType team)
    {
        // Check that there is a tile to the east of the origin tile
        if (originTile.X + distance > 7) { return false; }

        return board[originTile.X + distance, originTile.Y].OccupyingPiece != PieceModel.None && board[originTile.X + distance, originTile.Y].OccupyingPiece.MyTeam != team;
    }

    private static bool GetEnemyUnitNTilesWest(TileModel originTile, BoardModel board, int distance, PieceModel.TeamType team)
    {
        // Check that there is a tile to the West of the origin tile
        if (originTile.X - distance < 0) { return false; }

        return board[originTile.X - distance, originTile.Y].OccupyingPiece != PieceModel.None && board[originTile.X - distance, originTile.Y].OccupyingPiece.MyTeam != team;
    }

    private static bool GetDestinationIsNTilesDiagonalBehindWest(TileModel originTile, TileModel destinationTile, int distance, int direction)
    {
        return (destinationTile.X == originTile.X - distance && destinationTile.Y == originTile.Y - distance * direction);
    }

    private static bool GetDestinationIsNTilesDiagonalBehindEast(TileModel originTile, TileModel destinationTile, int distance, int direction)
    {
        return (destinationTile.X == originTile.X + distance && destinationTile.Y == originTile.Y - distance * direction);
    }

    private static bool GetDestinationIsNTilesDiagonalAheadEast(TileModel originTile, TileModel destinationTile, int distance, int direction)
    {
        return (destinationTile.X == originTile.X + distance && destinationTile.Y == originTile.Y + distance * direction);
    }

    private static bool GetDestinationIsNTilesDiagonalAheadWest(TileModel originTile, TileModel destinationTile, int distance, int direction)
    {
        return (destinationTile.X == originTile.X - distance && destinationTile.Y == originTile.Y + distance * direction);
    }

    private bool GetDestinationIsNTilesAhead(TileModel originTile, TileModel destinationTile, int distance, int direction)
    {
        return (destinationTile.X == originTile.X && destinationTile.Y == originTile.Y + distance * direction);
    }

    private bool GetDestinationTileIsOccupiedByEnemy(TileModel destinationTile, PieceModel.TeamType team)
    {
        return (destinationTile.OccupyingPiece != PieceModel.None) && (destinationTile.OccupyingPiece.MyTeam != team);
    }
    private bool GetDestinationTileIsOccupiedByAlly(TileModel destinationTile, PieceModel.TeamType team)
    {
        return (destinationTile.OccupyingPiece != PieceModel.None) && (destinationTile.OccupyingPiece.MyTeam == team);
    }

    private bool GetDestinationTileIsClear(TileModel destinationTile)
    {
        return (destinationTile.OccupyingPiece == PieceModel.None);
    }

    private bool GetIsOnStartingTile(TileModel originTile)
    {
        return (PieceModel.registeredPieces[originTile.OccupyingPiece.Id].StartingTile == originTile);
    }
}
