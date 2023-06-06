using Library.Common;
using Library.Exceptions;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Security;

namespace Library.Models
{
    public class BoardModel : IEnumerable<TileModel>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        private readonly TileModel[,] _tiles;

        public BoardModel()
        {
            _tiles = new TileModel[8,8];

            for(int x =  0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    TileModel tile = new TileModel(x, y, PieceModel.None);
                    _tiles[x,y] = tile;
                }
            }
        }

        public BoardModel TransferPieceToEmptyTile(TileModel startingTile, TileModel destinationTile)
        {
            if(destinationTile.IsNotEmpty) { throw new InvalidMoveException(startingTile, destinationTile); }

            PieceModel piece = startingTile.OccupyingPiece;
            startingTile.OccupyingPiece = PieceModel.None;
            destinationTile.OccupyingPiece = piece;

            this._tiles[startingTile.X, startingTile.Y] = startingTile;
            this._tiles[destinationTile.X, destinationTile.Y] = destinationTile;

            return this;
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

        public TileModel this[int x, int y]
        {
            get
            {
                Validation.NotNull(x, nameof(x));
                Validation.NotNull(y, nameof(y));
                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                return _tiles[x, y];
            }
            set
            {
                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                _tiles[x, y] = value;
            }
        }

        public TileModel this[string reference]
        {
            get
            {
                Validation.NotNull(reference, nameof(reference));
                Validation.Equals(reference.Length, 2);
                Validation.IsInRange(reference[0], 'A', 'H', nameof(reference));
                Validation.IsInRange(reference[1], '1', '8', nameof(reference));

                int x = reference[0] - 'A';
                int y = (int)char.GetNumericValue(reference[1]) - 1;

                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                Debug.WriteLine($"Fetched Tile {_tiles[x, y].ClassicCoords} with reference {reference}");

                return _tiles[x, y];
            }
            set
            {
                Validation.NotNull(reference, nameof(reference));
                Validation.Equals(reference.Length, 2);
                Validation.IsInRange(reference[0], 'A', 'H', nameof(reference));
                Validation.IsInRange(reference[1], '1', '8', nameof(reference));

                int x = reference[0] - 'A';
                int y = (int)char.GetNumericValue(reference[1]);

                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                _tiles[x, y] = value;
            }
        }

    }
}
