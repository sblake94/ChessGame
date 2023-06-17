using Domain.Models.Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Data.ValueTypes
{
    public class FENString
    {
        private readonly string _underlyingFENString;

        private FENString(string value)
        {
            _underlyingFENString = value;
        }

        public string[] Split(char splitchar)
        {
            return _underlyingFENString.Split(splitchar);
        }

        public static Result<FENString> FromString(string _input)
        {
            if (!IsValid(_input))
            { 
                return new Failure<FENString>(new Exception("Invalid FEN string"));
            }

            return new Success<FENString>(new FENString(_input)); 
        }

        /*  What is a FEN string?

        A FEN record defines a particular game position, all in one text line and using only the ASCII 
        character set. A text file with only FEN data records should use the filename extension .fen.[4]
        A FEN record contains six fields, each separated by a space. The fields are as follows:[5]

        Piece placement data: Each rank is described, starting with rank 8 and ending with rank 1, with a "/" 
            between each one; 
            within each rank, the contents of the squares are described in order from the a-file to the h-file. 
            Each piece is identified by a single letter taken from the standard English names in algebraic notation 
            (pawn = "P", knight = "N", bishop = "B", rook = "R", queen = "Q" and king = "K"). White pieces are designated 
            using uppercase letters ("PNBRQK"), while black pieces use lowercase letters ("pnbrqk"). A set of one or 
            more consecutive empty squares within a rank is denoted by a digit from "1" to "8", corresponding to the 
            number of squares.
            
        Active color: "w" means that White is to move; "b" means that Black is to move.

        Castling availability: If neither side has the ability to castle, this field uses the character "-". 
            Otherwise, this field contains one or more letters: "K" if White can castle kingside, "Q" if White can castle 
            queenside, "k" if Black can castle kingside, and "q" if Black can castle queenside. A situation that temporarily 
            prevents castling does not prevent the use of this notation.
            
        En passant target square: This is a square over which a pawn has just passed while moving two squares; it is 
            given in algebraic notation. If there is no en passant target square, this field uses the character "-". 
            This is recorded regardless of whether there is a pawn in position to capture en passant.[6] An updated version 
            of the spec has since made it so the target square is recorded only if a legal en passant capture is possible, 
            but the old version of the standard is the one most commonly used.[7][8]
            
        Halfmove clock: The number of halfmoves since the last capture or pawn advance, used for the fifty-move rule.[9]
            
        Fullmove number: The number of the full moves. It starts at 1 and is incremented after Black's move.

        */

        private static bool IsValid(string _input)
        {
            // Validate that the string is a valid Forsyth-Enwards Notation string
            // ...


            return true;
        }
    }
}
