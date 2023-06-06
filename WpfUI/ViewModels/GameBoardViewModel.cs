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


    public TileViewModel[] Tiles
    {
        get { return BuildTileViewModels(); }
    }

    public GameBoardViewModel()
    {
        // TODO: This should be done via constructor injection, but it works for now
        _gameStateEngineService = Ioc.Default.GetRequiredService<IGameStateEngineService>();

    }

    private TileViewModel[] BuildTileViewModels()
    {
        var tiles = _gameStateEngineService.CurrentBoard;
        var result = new TileViewModel[64];
        foreach(var tile in tiles)
        {
            int idx = tile.X + tile.Y * 8;
            var tvm = new TileViewModel();
            tvm.TileModel = tile;

            result[idx] = tvm;
        }

        return result;
    }
}
