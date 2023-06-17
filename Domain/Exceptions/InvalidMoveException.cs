using Domain.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidMoveException : Exception
    {
        TileModel _startingTile;
        TileModel _destinationTile;

        // TODO: Add a constructor that takes a MoveModel as an argument
        // TODO: Make the base constructor more readable
        public InvalidMoveException(TileModel startingTile, TileModel destinationTile)
            : base($"Invalid Move" + 
                  (!(startingTile is null) && !(destinationTile is null) ? 
                  $": {startingTile.OccupyingPiece} tried to move from {startingTile.ClassicCoords} to {destinationTile.ClassicCoords}." 
                  : ""))
        {
            _startingTile = startingTile;
            _destinationTile = destinationTile;
        }
    }
}
