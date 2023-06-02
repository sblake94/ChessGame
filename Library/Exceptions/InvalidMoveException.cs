using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    public class InvalidMoveException : Exception
    {
        TileModel? _startingTile;
        TileModel? _destinationTile;

        public InvalidMoveException(TileModel? startingTile, TileModel? destinationTile)
            : base($"Invalid Move" + 
                  (startingTile is not null && destinationTile is not null ? 
                  $": {startingTile.OccupyingPiece} tried to move from {startingTile.ClassicCoords} to {destinationTile.ClassicCoords}." 
                  : ""))
        {
            _startingTile = startingTile;
            _destinationTile = destinationTile;
        }
    }
}
