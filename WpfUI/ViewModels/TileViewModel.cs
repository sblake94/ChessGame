using CommunityToolkit.Mvvm.DependencyInjection;
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

    private string _imageSource;
    public string ImageSource 
    {
        get 
        {
            ImageSource = "D:\\Dev\\Visual Studio Projects\\Portfolio\\ChessGame\\ChessGame\\WpfUI\\Resources\\Images\\BlackPawn.png";

            return _imageSource;
        } 
        set
        {
            if(_imageSource != value)
            {
                _imageSource = value;
                NotifyPropertyChanged(nameof(ImageSource));
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

    internal void SetIndex(int v)
    {
        TileModel = _gameStateEngineService.CurrentBoard[v];
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
        _gameStateEngineService.SelectTile(_tileModel);
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
