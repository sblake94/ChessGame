using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Services;
using System.ComponentModel;

namespace Presentation_WPF.ViewModels;

public class GameDataViewModel
    : ViewModelBase<GameDataViewModel>
    , INotifyPropertyChanged
{
    private string _titleContent = "Game Data";
    public string TitleContent 
    {
        get { return _titleContent; }
        set 
        {
            if(_titleContent != value)
            {
                _titleContent = value;
                OnPropertyChanged(nameof(TitleContent));
            }
        }
    }

    private string _team = "-";
    public string Team 
    {
        get 
        {
            var piece = _chessLogicFacadeService.SelectedTile?.OccupyingPiece.CurrentPiece == 0 ? "Empty" : _team;
            return $"Team: {piece}"; 
        }
        set 
        {
            if(_team != value)
            {
                _team = value;
                OnPropertyChanged(nameof(Team));
            }
        }
    }

    private string _unitType = "-";
    public string UnitType 
    {
        get { return _unitType; }
        set
        {
            if(_unitType != value)
            {
                _unitType = value;
                OnPropertyChanged(nameof(UnitType));
            }
        }
    }

    private string _coords = "-";
    public string Coords 
    {
        get { return _coords; }
        set 
        {
            if(_coords != value)
            {
                _coords = value;
                OnPropertyChanged(nameof(Coords));
            }
        }
    }

    public int WhiteScore
    {
        get { return _chessLogicFacadeService.CurrentGame.FirstPlayer.Score; }
    }
    public int BlackScore
    {
        get { return _chessLogicFacadeService.CurrentGame.SecondPlayer.Score; }
    }

    public GameDataViewModel()
    {
        _chessLogicFacadeService.PropertyChanged += (sender, e) => 
        {
            if (e.PropertyName == nameof(_chessLogicFacadeService.SelectedTile))
            {
                Coords = _chessLogicFacadeService.SelectedTile?.ClassicCoords ?? "-";
                UnitType = _chessLogicFacadeService.SelectedTile?.OccupyingPiece.MyUnit.ToString() ?? "-";
                Team = _chessLogicFacadeService.SelectedTile?.OccupyingPiece.MyTeam.ToString() ?? "-";
            }
        };

        _chessLogicFacadeService.CurrentGame.FirstPlayer.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_chessLogicFacadeService.CurrentGame.FirstPlayer.Score))
            {
                OnPropertyChanged(nameof(WhiteScore));
            }
        };

        _chessLogicFacadeService.CurrentGame.SecondPlayer.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_chessLogicFacadeService.CurrentGame.SecondPlayer.Score))
            {
                OnPropertyChanged(nameof(BlackScore));
            }
        };

        _chessLogicFacadeService.CurrentGame.OnGameOver += (sender, e) =>
        {
            string winner = _chessLogicFacadeService.CurrentGame.Winner.teamColor.ToString();
            TitleContent = $"Game Over : {winner} wins!";
        };
    }
}
