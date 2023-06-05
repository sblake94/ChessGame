using Library.Common;

namespace Library.Models
{
    public class TileModel : IEquatable<TileModel>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public readonly int xPos;
        public readonly int yPos;

        public string ClassicCoords { 
            get 
            {
                string result = $"{(char)(xPos + 'A')}{(char)(yPos + '1')}";
                return result;
            }
        }
        public bool IsEmpty { get { return OccupyingPiece == PieceModel.None; } }
        public bool IsNotEmpty { get { return OccupyingPiece != PieceModel.None; } }
        public bool IsLightTile { get { return (xPos + yPos) % 2 == 0; } }


        private PieceModel _occupyingPiece;

        public PieceModel OccupyingPiece
        {
            get 
            { 
                return _occupyingPiece; 
            }
            set 
            {
                _occupyingPiece = value; 
            }
        }

        public TileModel(int xPos, int yPos, PieceModel? occupyingPiece = null)
        {
            Validation.NotNull(xPos, nameof(xPos));
            Validation.NotNull(yPos, nameof(yPos));

            Validation.InRange(xPos, 0, 7, nameof(xPos));
            Validation.InRange(yPos, 0, 7, nameof(yPos));

            this.xPos = xPos;
            this.yPos = yPos;

            if (occupyingPiece is null) { occupyingPiece = PieceModel.None; }
            this.OccupyingPiece = occupyingPiece;
        }

        public static TileModel CreateFromTileID(string? tileID)
        {
            var invalidParameterException = new InvalidDataException($"tileID {tileID} invalid when creating new TileModel");

            if (tileID is null) { throw invalidParameterException; }
            if (tileID.Length > 2) { throw invalidParameterException; }

            var xPos = tileID[0] - 'A';
            var yPos = tileID[1] - '0';

            TileModel result = new TileModel(xPos, yPos);
            return result;
        }

        public bool Equals(TileModel? other)
        {
            return Validation.Equals(this, other);
        }
    }
}
