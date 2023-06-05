using Microsoft.Extensions.Logging;

namespace Library.Models
{
    public class MoveModel
    {
        private readonly ILogger? _logger;

        public Guid Id { get; } = Guid.NewGuid();

        public BoardModel Board { get; }
        public TileModel OriginTile { get; }
        public TileModel DestinationTile { get; }
        public bool MoveExecuted { get; set; }

        public string LogMessage { 
            get
            {
                if (MoveExecuted)
                    return $"Moved {DestinationTile.OccupyingPiece.ToString} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
                else
                    return $"Intention to move {OriginTile.OccupyingPiece.ToString} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
            }
        }

        public bool IsValid
        {
            get
            {
                return DestinationTile.IsEmpty;
            }
        }

        public MoveModel(BoardModel board, TileModel origin, TileModel destination)
        {
            Board = board;
            OriginTile = origin;
            DestinationTile = destination;
        }

        public void ExecuteMove()
        {
            Board[DestinationTile.ClassicCoords].OccupyingPiece = OriginTile.OccupyingPiece;
            OriginTile.OccupyingPiece = PieceModel.None;
            MoveExecuted = true;
            LogMove();
        }

        public void LogMove()
        {
            if (_logger is null) { return; }

            string logMessage = $"{DateTime.Now}: Moved {DestinationTile.OccupyingPiece} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
            _logger.LogInformation(logMessage);
        }
    }
}
