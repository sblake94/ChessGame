using Library.Common;
using System.Net.Http.Headers;

namespace Library.Models;

public class PieceModel
{
    public Guid Id = Guid.NewGuid();

    public int? CurrentPiece { get; set; }
    public string ToString 
    {
        get
        {
            return $"{MyTeam} {MyUnit}";
        }
    }

    public enum TeamType
    {
        White = 0, 
        Black = 1
    }
    public TeamType MyTeam
    { 
        get 
        {
            return (TeamType)(CurrentPiece / 8);
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

    public PieceModel(TeamType team, UnitType unit)
    {
        CurrentPiece = (int)team * 8 + (int)unit;
    }

    public PieceModel(int currentPiece)
    {
        CurrentPiece = currentPiece;
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

}
