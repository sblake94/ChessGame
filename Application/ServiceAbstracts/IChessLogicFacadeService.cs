﻿using Domain.Models.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Application.ServiceAbstracts
{
    public interface IChessLogicFacadeService : IServiceBase, INotifyPropertyChanged
    {
        TileModel SelectedTile { get; }
        GameModel CurrentGame { get; }
        ObservableCollection<MoveModel> MoveHistory { get; }

        event EventHandler BoardReset;

        void ClickOnTile(int x, int y);
        List<MoveModel> GetAllPossibleMoves(TileModel tile, GameModel game);

        void StartNewGame();
    }
}
