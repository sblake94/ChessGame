using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Library.Models;
using Library.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfUI.ViewModels;

public partial class GameBoardViewModel
    : ViewModelBase<GameBoardViewModel>
{
    private readonly IGameStateEngineService _gameStateEngineService;

    public ICommand OnTileClicked { get; }

    public TileViewModel[] Tiles
    {
        get { return BuildTileViewModels(); }
    }



    public GameBoardViewModel()
    {
        OnTileClicked = new RelayCommand<string>(Handle_OnTileClicked);

        // TODO: This should be done via constructor injection, but it works for now
        _gameStateEngineService = Ioc.Default.GetRequiredService<IGameStateEngineService>();
    }

    [RelayCommand]
    private void Handle_OnTileClicked(string? tileID)
    {
        TileModel clickedTile = TileModel.CreateFromTileID(tileID);
        _gameStateEngineService.SelectTile(clickedTile);
    }

    private TileViewModel[] BuildTileViewModels()
    {
        var tiles = _gameStateEngineService.CurrentBoard;
        var result = new TileViewModel[64];
        foreach(var tile in tiles)
        {
            int idx = tile.xPos + tile.yPos * 8;
            var tvm = new TileViewModel();
            tvm.TileModel = tile;

            result[idx] = tvm;
        }

        return result;
    }
}
