namespace Library.Models.Game;

public class PlayerModel
{
    public TeamColor teamColor { get; set; }

    public PlayerModel(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }
}