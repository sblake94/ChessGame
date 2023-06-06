﻿using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace Library.Models
{
    public class MoveModel
    {
        private readonly ILogger? _logger;

        public Guid IdOfPieceBeingMoved { get; private set; }
        public Guid Id { get; } = Guid.NewGuid();

        public BoardModel Board { get; }
        public TileModel OriginTile { get; }
        public TileModel DestinationTile { get; }
        public bool MoveExecuted { get; set; } = false;


        public string LogMessage { 
            get
            {
                if (MoveExecuted)
                    return $"Moved {DestinationTile.OccupyingPiece.ToString} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
                else
                    return $"Intention to move {OriginTile.OccupyingPiece.ToString} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
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

        public MoveModel(BoardModel board, TileModel origin, TileModel destination)
        {
            Board = board;
            OriginTile = origin;
            DestinationTile = destination;

            IdOfPieceBeingMoved = OriginTile.OccupyingPiece.Id;
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
