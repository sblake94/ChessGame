namespace Library.Models;

public class PieceModel
{
    public Guid Id = Guid.NewGuid();
    public TeamType Team { get; set; }
    public UnitType Unit { get; set; }

    public static PieceModel EmptySpace { get; } = new PieceModel(TeamType.None, UnitType.None);

    public PieceModel(TeamType team, UnitType unit)
    {
        Team = team;
        Unit = unit;
    }


    public enum TeamType
    {
        None = 0,
        White,
        Black
    }

    public enum UnitType
    {
        None,
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }
}
