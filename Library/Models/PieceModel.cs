using Library.Exceptions;
using System.Net;

namespace Library.Models;

public class PieceModel
{
    public Guid Id = Guid.NewGuid();
    public static Dictionary<Guid, PieceModel> registeredPieces = new Dictionary<Guid, PieceModel>();

    public int? CurrentPiece { get; set; }
    public bool IsInPlay { get; set; } = true;
    public TileModel? StartingTile { get; } = null;
    public int ScoreValue { get; set; } = 0;

    public TeamColor MyTeam
    { 
        get 
        {
            return (TeamColor)(CurrentPiece / 8);
        }
    }

    public enum UnitType
    {
        None = 0,
        Pawn = 1,
        Rook = 2,
        Knight = 3,
        Bishop = 4,
        Queen = 5,
        King = 6
    }
    public UnitType MyUnit
    { 
        get 
        {
            return (UnitType)(CurrentPiece % 8);
        }
    }

    public PieceModel(TeamColor team, UnitType unit, TileModel? originTile = null)
    {
        CurrentPiece = (int)team * 8 + (int)unit;
        StartingTile = originTile;

        registeredPieces.Add(Id, this);
        ScoreValue = GetPieceValue(unit);
    }

    public PieceModel(int currentPiece, TileModel? originTile = null)
    {
        CurrentPiece = currentPiece;
        StartingTile = originTile;

        if(currentPiece == 0)
        {
            return;
        }

        registeredPieces.Add(Id, this);
    }

    public static PieceModel None = new PieceModel(0);

    public static PieceModel WhitePawn = new PieceModel(1);
    public static PieceModel WhiteRook = new PieceModel(2);
    public static PieceModel WhiteKnight = new PieceModel(3);
    public static PieceModel WhiteBishop = new PieceModel(4);
    public static PieceModel WhiteQueen = new PieceModel(5);
    public static PieceModel WhiteKing = new PieceModel(6);

    public static PieceModel BlackPawn = new PieceModel(9);
    public static PieceModel BlackRook = new PieceModel(10);
    public static PieceModel BlackKnight = new PieceModel(11);
    public static PieceModel BlackBishop = new PieceModel(12);
    public static PieceModel BlackQueen = new PieceModel(13);
    public static PieceModel BlackKing = new PieceModel(14);

    public static int GetPieceValue(UnitType unit)
    {
        switch (unit)
        {
            case UnitType.None:     return 0;
            case UnitType.Pawn:     return 1;
            case UnitType.Rook:     return 5;
            case UnitType.Knight:   return 3;
            case UnitType.Bishop:   return 3;
            case UnitType.Queen:    return 9;
            case UnitType.King:     return 1000;
            default:                throw new InvalidArgumentException(nameof(unit));
        }
    }

    public static bool operator ==(PieceModel? a, PieceModel? b)
    {
        if (a is null && b is null) { return true; }
        if (a is null || b is null) { return false; }

        return a.Id == b.Id;
    }

    public static bool operator !=(PieceModel? a, PieceModel? b)
    {
        if (a is null && b is null) { return true; }
        if (a is null || b is null) { return false; }

        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {

        return $"{MyTeam} {MyUnit}";
    }
}
