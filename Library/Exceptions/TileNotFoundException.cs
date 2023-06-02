using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Exceptions
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
