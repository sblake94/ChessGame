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

        public TileModel this[int idx]
        {
            get
            {
                Validation.NotNull(idx, nameof(idx));
                Validation.InRange(idx, 0, 63, nameof(idx));

                return _tiles[idx];
            }
            set
            {
                Validation.NotNull(idx, nameof(idx));
                Validation.InRange(idx, 0, 63, nameof(idx));

                _tiles[idx] = value;
            }
        }

        public TileModel this[int x, int y]
        {
            get
            {
                Validation.NotNull(x, nameof(x));
                Validation.NotNull(y, nameof(y));
                Validation.InRange(x, 0, 8, nameof(x));
                Validation.InRange(y, 0, 8, nameof(y));

                return _tiles[x + y * 8];
            }
            set
            {
                Validation.InRange(x, 0, 8, nameof(x));
                Validation.InRange(y, 0, 8, nameof(y));

                _tiles[x + y * 8] = value;
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
                int y = reference[1] - '1';

                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                return _tiles[x + y * 8];
            }
            set
            {
                Validation.NotNull(reference, nameof(reference));
                Validation.Equals(reference.Length, 2);
                Validation.IsInRange(reference[0], 'A', 'H', nameof(reference));
                Validation.IsInRange(reference[1], '1', '8', nameof(reference));

                int x = reference[0] - 'A';
                int y = reference[1] - '1';

                Validation.InRange(x, 0, 7, nameof(x));
                Validation.InRange(y, 0, 7, nameof(y));

                _tiles[x + y * 8] = value;
            }
        }

    }
}
