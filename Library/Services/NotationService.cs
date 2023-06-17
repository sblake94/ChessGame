using Library.Attributes.ServiceAttributes;
using Library.Exceptions;
using Library.Models.Game;
using Microsoft.Extensions.Logging;

namespace Library.Services;

[TransientService]
public class NotationService 
    : ServiceBase<NotationService>
    , INotationService
{
    const string StartingBoard = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public NotationService(ILoggerFactoryService loggerFactoryService) 
        : base(loggerFactoryService)
    {

    }

    public BoardModel GetStartingBoard()
    {
        return FromFen(StartingBoard);
    }

    public string ToFen(BoardModel board)
    {
        throw new NotImplementedException();
    }

    private BoardModel FromFen(string fenString)
    {
        BoardModel board = new BoardModel();
        string fenboard = fenString.Split(' ')[0];
        int x = 0, y = 7;

        foreach(char symbol in fenboard)
        {
            if(symbol == '/')
            {
                x = 0;
                y--;
            }
            else
            {
                if(char.IsDigit(symbol))
                {
                    x += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    var tile = board[x, y];
                    var piece = PieceFromSymbol(symbol, tile);

                    tile.OccupyingPiece = piece;
                    x++;
                }
            }
        }
        return board;
    }

    private PieceModel PieceFromSymbol(char symbol, TileModel tile)
    {
        switch (symbol)
        {
            case 'k': return new PieceModel(TeamColor.Black, PieceModel.UnitType.King, tile);
            case 'q': return new PieceModel(TeamColor.Black, PieceModel.UnitType.Queen, tile);
            case 'b': return new PieceModel(TeamColor.Black, PieceModel.UnitType.Bishop, tile);
            case 'n': return new PieceModel(TeamColor.Black, PieceModel.UnitType.Knight, tile);
            case 'r': return new PieceModel(TeamColor.Black, PieceModel.UnitType.Rook, tile);
            case 'p': return new PieceModel(TeamColor.Black, PieceModel.UnitType.Pawn, tile);

            case 'K': return new PieceModel(TeamColor.White, PieceModel.UnitType.King, tile);
            case 'Q': return new PieceModel(TeamColor.White, PieceModel.UnitType.Queen, tile);
            case 'B': return new PieceModel(TeamColor.White, PieceModel.UnitType.Bishop, tile);
            case 'N': return new PieceModel(TeamColor.White, PieceModel.UnitType.Knight, tile);
            case 'R': return new PieceModel(TeamColor.White, PieceModel.UnitType.Rook, tile);
            case 'P': return new PieceModel(TeamColor.White, PieceModel.UnitType.Pawn, tile);
            
            default:
                throw new InvalidArgumentException(nameof(symbol));
        }
    }
}
