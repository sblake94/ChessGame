﻿using System.ComponentModel;

namespace Domain.Models.Game
{
    public class GameModel
    {
        public PlayerModel FirstPlayer { get; set; }
        public PlayerModel SecondPlayer { get; set; }

        private bool _isFirstPlayerTurn = true;

        public event PropertyChangedEventHandler FirstPlayerScoreChanged;
        public event PropertyChangedEventHandler SecondPlayerScoreChanged;
        public event PropertyChangedEventHandler OnGameOver;

        public PlayerModel ActivePlayer { get { return _isFirstPlayerTurn ? FirstPlayer : SecondPlayer; } }
        public PlayerModel InactivePlayer { get { return _isFirstPlayerTurn ? SecondPlayer : FirstPlayer; } }
        public PlayerModel Winner { get; set; }

        public BoardModel Board { get; set; }

        public GameModel(BoardModel board)
        {
            FirstPlayer = new PlayerModel(TeamColor.White);
            SecondPlayer = new PlayerModel(TeamColor.Black);

            Board = board;

            FirstPlayer.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FirstPlayer.Score))
                {
                    FirstPlayerScoreChanged?.Invoke(this, args);
                }
            };

            SecondPlayer.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SecondPlayer.Score))
                {
                    SecondPlayerScoreChanged?.Invoke(this, args);
                }
            };
        }

        public void EndTurn()
        {
            _isFirstPlayerTurn = !_isFirstPlayerTurn;
        }

        public void GameOver(PlayerModel victoriousPlayer)
        {
            Winner = victoriousPlayer;
            OnGameOver?.Invoke(this, new PropertyChangedEventArgs(nameof(Winner)));
        }
    }
}
