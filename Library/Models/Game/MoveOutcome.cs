using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Game
{
    public enum MoveOutcome
    {
        InvalidMove = 0,
        MoveToEmptyTile = 1,
        CaptureStandardPiece = 2,
        CaptureKing = 4,

        Check = 8,
        Checkmate = 16,
        Stalemate = 32,

        Castle = 64,
        Promotion = 128,
    }
}
