using Domain.Enums;
using Domain.Models.Data.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Domain.Models.Game
{
    public class MoveModel
    {
        private readonly ILogger _logger;

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

        public string LogMessage
        {
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

        public static bool operator ==(MoveModel lhs, MoveModel rhs)
        {
            var sameOrigin = lhs.OriginTile == rhs.OriginTile;
            var sameDestination = lhs.DestinationTile == rhs.DestinationTile;
            var samePiece = lhs.IdOfPieceBeingMoved == rhs.IdOfPieceBeingMoved;
            var sameBoard = lhs.Board == rhs.Board;
            var sameExecutionState = lhs.MoveExecuted == rhs.MoveExecuted;


            return sameOrigin && sameDestination && samePiece && sameBoard && sameExecutionState;
        }

        public static bool operator !=(MoveModel lhs, MoveModel rhs)
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
            if (DestinationTile.OccupyingPiece.MyUnit != PieceModel.UnitType.None)
            {
                ActivePlayer.Score += PieceOriginallyAtDestination.ScoreValue;
            }

            // Finsh up execution
            MoveExecuted = true;
            LogMove();
        }

        public Result<MoveOutcome> DetermineOutcome()
        {
            if (!(Outcome is null)) { return new Success<MoveOutcome>(Outcome.Value); }

            if (PieceOriginallyAtDestination.MyUnit == PieceModel.UnitType.None) { return new Success<MoveOutcome>(MoveOutcome.MoveToEmptyTile); }
            if (PieceOriginallyAtDestination.MyUnit == PieceModel.UnitType.King) { return new Success<MoveOutcome>(MoveOutcome.CaptureKing); }
            if (PieceOriginallyAtDestination.MyUnit != PieceModel.UnitType.None) { return new Success<MoveOutcome>(MoveOutcome.CaptureStandardPiece); }


            return new Failure<MoveOutcome>(new NotImplementedException(nameof(DetermineOutcome) + " " + Outcome.Value.ToString()));
        }

        public void LogMove()
        {
            if (_logger is null) { return; }

            string logMessage = $"{DateTime.Now}: Moved {DestinationTile.OccupyingPiece} from {OriginTile.ClassicCoords} to {DestinationTile.ClassicCoords}";
            _logger.LogInformation(logMessage);
        }

        public override bool Equals(object obj)
        {
            return obj is MoveModel model &&
                   IdOfPieceBeingMoved.Equals(model.IdOfPieceBeingMoved) &&
                   Id.Equals(model.Id) &&
                   EqualityComparer<GameModel>.Default.Equals(Game, model.Game) &&
                   EqualityComparer<PlayerModel>.Default.Equals(ActivePlayer, model.ActivePlayer) &&
                   EqualityComparer<BoardModel>.Default.Equals(Board, model.Board) &&
                   EqualityComparer<TileModel>.Default.Equals(OriginTile, model.OriginTile) &&
                   EqualityComparer<TileModel>.Default.Equals(DestinationTile, model.DestinationTile) &&
                   MoveExecuted == model.MoveExecuted &&
                   EqualityComparer<PieceModel>.Default.Equals(PieceOriginallyAtDestination, model.PieceOriginallyAtDestination) &&
                   PieceColor == model.PieceColor &&
                   Outcome == model.Outcome &&
                   LogMessage == model.LogMessage;
        }

        public override int GetHashCode()
        {
            int hashCode = -1820705567;
            hashCode = hashCode * -1521134295 + IdOfPieceBeingMoved.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<GameModel>.Default.GetHashCode(Game);
            hashCode = hashCode * -1521134295 + EqualityComparer<PlayerModel>.Default.GetHashCode(ActivePlayer);
            hashCode = hashCode * -1521134295 + EqualityComparer<BoardModel>.Default.GetHashCode(Board);
            hashCode = hashCode * -1521134295 + EqualityComparer<TileModel>.Default.GetHashCode(OriginTile);
            hashCode = hashCode * -1521134295 + EqualityComparer<TileModel>.Default.GetHashCode(DestinationTile);
            hashCode = hashCode * -1521134295 + MoveExecuted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PieceModel>.Default.GetHashCode(PieceOriginallyAtDestination);
            hashCode = hashCode * -1521134295 + PieceColor.GetHashCode();
            hashCode = hashCode * -1521134295 + Outcome.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LogMessage);
            return hashCode;
        }
    }
}
