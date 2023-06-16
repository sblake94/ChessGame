using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Game;

public class PlayerModel : INotifyPropertyChanged
{
    [Range(0,int.MaxValue)] private int _score = 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Score 
    { 
        get { return _score; }
        set 
        { 
            if(_score != value)
            {
                _score = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
            }
        }
    }
    public TeamColor teamColor { get; set; }

    public PlayerModel(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }

    
}