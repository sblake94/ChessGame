using Library.Models.Game;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace Library.Models
{
    public class MoveModel
    {
        private readonly ILogger? _logger;

        public Guid IdOfPieceBeingMoved { get; private set; }
        public Guid Id { get; } = Guid.NewGuid();

        public GameModel Game { get; }
        public PlayerModel ActivePlayer { get; }
        public BoardModel Board { get; }
        public TileModel OriginTile { get; }
        public TileModel DestinationTile { get; }
        public bool MoveExecuted { get; set; } = false;
        public PieceModel PieceOriginallyAtDestination { get; set; }
        public TeamColor PieceColor { get; }
        public MoveOutcome? Outcome { get; set; } = null;

        public string LogMessage { 
            get
            {
                if (MoveExecuted)
                    return 
                        $"Moved {DestinationTile.OccupyingPiece.MyUnit.ToString()} " +
                        $"from {OriginTile.ClassicCoords} " +
                        $"to {DestinationTile.ClassicCoords}" +
                        (PieceOriginallyAtDestination == PieceModel.None ? $"" : $", to take {PieceOriginallyAtDestination.ToString()}");
                else
                    return $"Intention to move {OriginTile.OccupyingPiece.ToString()} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
            }
        }

        public static bool operator==(MoveModel lhs, MoveModel rhs)
        {
            var sameOrigin = lhs.OriginTile == rhs.OriginTile;
            var sameDestination = lhs.DestinationTile == rhs.DestinationTile;
            var samePiece = lhs.IdOfPieceBeingMoved == rhs.IdOfPieceBeingMoved;
            var sameBoard = lhs.Board == rhs.Board;
            var sameExecutionState = lhs.MoveExecuted == rhs.MoveExecuted;
            

            return sameOrigin && sameDestination && samePiece && sameBoard && sameExecutionState;
        }

        public static bool operator!=(MoveModel lhs, MoveModel rhs)
        {
            return !(lhs == rhs);
        }

        public MoveModel(GameModel game, PlayerModel activePlayer, TileModel origin, TileModel destination)
        {
            Game = game;
            Board = game.Board;
            OriginTile = origin;
            DestinationTile = destination;
            ActivePlayer = activePlayer;

            IdOfPieceBeingMoved = OriginTile.OccupyingPiece.Id;
            PieceColor = OriginTile.OccupyingPiece.MyTeam;
            PieceOriginallyAtDestination = DestinationTile.OccupyingPiece;
        }

        public void ExecuteMove()
        {
            // Update the Piece's location
            Board[DestinationTile.ClassicCoords].OccupyingPiece = OriginTile.OccupyingPiece;
            OriginTile.OccupyingPiece = PieceModel.None;

            // Increment the player's score if they captured a piece
            if(DestinationTile.OccupyingPiece.MyUnit != PieceModel.UnitType.None)
            {
                ActivePlayer.Score += PieceOriginallyAtDestination.ScoreValue;
            }

            // Finsh up execution
            MoveExecuted = true;
            LogMove();
        }

        public MoveOutcome DetermineOutcome()
        {
            if(Outcome is not null) { return Outcome.Value; }

            if (PieceOriginallyAtDestination.MyUnit == PieceModel.UnitType.None) { return MoveOutcome.MoveToEmptyTile; }
            if (PieceOriginallyAtDestination.MyUnit == PieceModel.UnitType.King) { return MoveOutcome.CaptureKing; }
            if (PieceOriginallyAtDestination.MyUnit != PieceModel.UnitType.None) { return MoveOutcome.CaptureStandardPiece; }


            throw new NotImplementedException(nameof(DetermineOutcome) + " " + Outcome.Value.ToString());
        }

        public void LogMove()
        {
            if (_logger is null) { return; }

            string logMessage = $"{DateTime.Now}: Moved {DestinationTile.OccupyingPiece} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
            _logger.LogInformation(logMessage);
        }
    }
}
