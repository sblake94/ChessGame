namespace Library.Models.Game
{
    public class GameModel
    {
        public PlayerModel FirstPlayer { get; set; }
        public PlayerModel SecondPlayer { get; set; }

        private bool _isFirstPlayerTurn = true;
        public PlayerModel ActivePlayer { get { return _isFirstPlayerTurn ? FirstPlayer : SecondPlayer; } }
        public PlayerModel InactivePlayer { get { return _isFirstPlayerTurn ? SecondPlayer : FirstPlayer; } }

        public BoardModel Board { get; set; }


        public GameModel(BoardModel board)
        {
            FirstPlayer = new PlayerModel(TeamColor.White);
            SecondPlayer = new PlayerModel(TeamColor.Black);

            Board = board;
        }

        public void EndTurn()
        {
            _isFirstPlayerTurn = !_isFirstPlayerTurn;
        }
    }
}
