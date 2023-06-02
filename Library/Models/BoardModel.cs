using Library.Common;
using Library.Exceptions;
using System.Collections;
using System.Collections.Immutable;
using System.Security;

namespace Library.Models
{
    public class BoardModel : IEnumerable<TileModel>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private readonly IList<TileModel> _tiles;

        public BoardModel()
        {
            _tiles = new List<TileModel>();

            for(int x =  0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    TileModel tile = new TileModel(x, y, PieceModel.EmptySpace);
                    _tiles.Add(tile);
                }
            }
        }

        public BoardModel(IEnumerable<TileModel> pieces)
        {
            if (pieces is null) { throw new Exception(nameof(pieces)); }
            this._tiles = pieces.ToList();
        }

        public BoardModel TransferPieceToEmptyTile(TileModel startingTile, TileModel destinationTile)
        {
            if(destinationTile.IsNotEmpty) { throw new InvalidMoveException(startingTile, destinationTile); }

            PieceModel piece = startingTile.OccupyingPiece;
            startingTile.OccupyingPiece = PieceModel.EmptySpace;
            destinationTile.OccupyingPiece = piece;

            return new BoardModel(_tiles);
        }

        public IEnumerator<TileModel> GetEnumerator()
        {
            foreach(var tile in _tiles)
            {
                yield return tile;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
