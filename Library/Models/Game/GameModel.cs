using System.ComponentModel;

namespace Library.Models.Game
{
    public class GameModel : INotifyPropertyChanged
    {
        public PlayerModel FirstPlayer { get; set; }
        public PlayerModel SecondPlayer { get; set; }

        private bool _isFirstPlayerTurn = true;

        public event PropertyChangedEventHandler? FirstPlayerScoreChanged;
        public event PropertyChangedEventHandler? SecondPlayerScoreChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public PlayerModel ActivePlayer { get { return _isFirstPlayerTurn ? FirstPlayer : SecondPlayer; } }
        public PlayerModel InactivePlayer { get { return _isFirstPlayerTurn ? SecondPlayer : FirstPlayer; } }

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
    }
}
