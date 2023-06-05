using Library.Attributes.ServiceAttributes;
using Library.Exceptions;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace Library.Services;

[TransientService]
public class NotationService 
    : ServiceBase<NotationService>
    , INotationService
{
    const string StartingBoard = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public NotationService(ILogger<NotationService> logger) 
        : base(logger)
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
                    var piece = PieceFromSymbol(symbol);

                    board[x, y].OccupyingPiece = piece;
                    x++;
                }
            }
        }
        return board;
    }

    private PieceModel PieceFromSymbol(char symbol)
    {
        switch (symbol)
        {
            case 'k': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.King);
            case 'q': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Queen);
            case 'b': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Bishop);
            case 'n': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Knight);
            case 'r': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Rook);
            case 'p': return new PieceModel(PieceModel.TeamType.Black, PieceModel.UnitType.Pawn);

            case 'K': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.King);
            case 'Q': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.Queen);
            case 'B': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.Bishop);
            case 'N': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.Knight);
            case 'R': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.Rook);
            case 'P': return new PieceModel(PieceModel.TeamType.White, PieceModel.UnitType.Pawn);
            
            default:
                throw new InvalidArgumentException(nameof(symbol));
        }
    }
}
