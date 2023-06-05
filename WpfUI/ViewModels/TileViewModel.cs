using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Common;
using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace WpfUI.ViewModels;

public class TileViewModel 
    : ViewModelBase<TileViewModel>
    , INotifyPropertyChanged
{
    private static SolidColorBrush LIGHT_TILE_COLOR = new SolidColorBrush(Color.FromRgb(198, 166, 118));
    private static SolidColorBrush DARK_TILE_COLOR = new SolidColorBrush(Color.FromRgb(100, 46, 27));

    private static string EMPTY_UNIT_PATH = "";

    private static string BLACK_PAWN_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackPawn.png";
    private static string BLACK_ROOK_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackRook.png";
    private static string BLACK_KNIG_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackKnight.png";
    private static string BLACK_BISH_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackBishop.png";
    private static string BLACK_QUEE_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackQueen.png";
    private static string BLACK_KING_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackKing.png";

    private static string WHITE_PAWN_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhitePawn.png";
    private static string WHITE_ROOK_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhiteRook.png";
    private static string WHITE_KNIG_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhiteKnight.png";
    private static string WHITE_BISH_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhiteBishop.png";
    private static string WHITE_QUEE_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhiteQueen.png";
    private static string WHITE_KING_PATH = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\WhiteKing.png";

    private readonly IGameStateEngineService _gameStateEngineService;
    private TileModel? _tileModel;

    static List<TileViewModel> instances = new List<TileViewModel>();

    public new event PropertyChangedEventHandler? PropertyChanged;

    public Thickness SelectedBorderThickness => 
        new Thickness(IsHighlighted ? 5 : 0);

    public Thickness MouseOverBorderThickness =>
        new Thickness(MouseIsOver ? 2 : 0);

    public Brush TileBGColor => _tileModel != null ?
     (_tileModel.IsLightTile ? LIGHT_TILE_COLOR : DARK_TILE_COLOR) :
     Brushes.Magenta;

    public string ImageSource 
    {
        get
        {
            if (TileModel is null
                || TileModel.OccupyingPiece is null
                || TileModel.OccupyingPiece.CurrentPiece is null)
            {
                return EMPTY_UNIT_PATH;
            }

            switch (TileModel.OccupyingPiece.CurrentPiece)
            {
                // TODO: This needs to not use raw ints
                case 0:     return EMPTY_UNIT_PATH;

                case 1:     return WHITE_PAWN_PATH;
                case 2:     return WHITE_ROOK_PATH;
                case 3:     return WHITE_KNIG_PATH;
                case 4:     return WHITE_BISH_PATH;
                case 5:     return WHITE_QUEE_PATH;
                case 6:     return WHITE_KING_PATH;

                case 9:     return BLACK_PAWN_PATH;
                case 10:    return BLACK_ROOK_PATH;
                case 11:    return BLACK_KNIG_PATH;
                case 12:    return BLACK_BISH_PATH;
                case 13:    return BLACK_QUEE_PATH;
                case 14:    return BLACK_KING_PATH;

                default:    return EMPTY_UNIT_PATH;
            }
        } 
    }

    private bool _mouseIsOver;
    public bool MouseIsOver 
    {
        get { return _mouseIsOver; }
        set 
        {
            if (_mouseIsOver != value)
            {
                _mouseIsOver = value;
                NotifyPropertyChanged(nameof(MouseIsOver));
                NotifyPropertyChanged(nameof(MouseOverBorderThickness));
            }
        }
    }

    private bool _isHighlighted;
    public bool IsHighlighted
    {
        get 
        {
            _isHighlighted = _gameStateEngineService.SelectedTile == this.TileModel;
            return _isHighlighted; 
        }
        set
        {
            if (_isHighlighted != value)
            {
                _isHighlighted = value;
                NotifyPropertyChanged(nameof(IsHighlighted));
                NotifyPropertyChanged(nameof(SelectedBorderThickness));
            }
        }
    }

    public TileModel? TileModel
    {
        get { return _tileModel; }
        set
        {
            if (_tileModel != value)
            {
                _tileModel = value;
            }
        }
    }


    public TileViewModel()
    {
        _gameStateEngineService = Ioc.Default.GetRequiredService<IGameStateEngineService>();
        instances.Add(this);
    }

    ~TileViewModel()
    {
        instances.Remove(this);
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal void SetIndex(string boardRef)
    {
        TileModel = _gameStateEngineService.CurrentBoard[boardRef];
    }

    internal void MouseEnter()
    {
        MouseIsOver = true;
    }

    internal void MouseDown()
    {
        Validation.NotNull(_tileModel, nameof(TileModel));

        _gameStateEngineService.ClickOnTile(_tileModel.xPos, _tileModel.yPos);
        RefreshAll();
    }

    internal void MouseLeave()
    {
        MouseIsOver = false;
    }

    public void Refresh()
    {
        Type viewModelType = typeof(TileViewModel);
        PropertyInfo[] properties = viewModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach(PropertyInfo property in properties)
        {
            NotifyPropertyChanged(property.Name);
        }
    }

    public static void RefreshAll()
    {
        foreach(var item in instances)
        {
            item.Refresh();
        }
    }
}
