using Domain.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TileNotFoundException : Exception
    {
        public TileNotFoundException(TileModel tile)
            : base(
                  $"Tile at {tile.ClassicCoords} does not exist"
                  )
        {
        }

    }
}
