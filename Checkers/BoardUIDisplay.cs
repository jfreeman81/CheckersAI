using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public interface BoardUIDisplay
    {
        void UpdateDisplay(Board board);
    }

    public class BoardCommandLineUIDisplay : BoardUIDisplay
    {

        public void UpdateDisplay(Board board)
        {
            for (int row = 0; row < Board.BOARD_SIZE; row++)
            {
                for (int col = 0; col < Board.BOARD_SIZE; col++)
                {
                    Piece tile = board.GetPiece(row, col);
                    string tileRep = GetTileDisplayRepresentation(tile);
                    Console.Write("{0,3}", tileRep);
                }
                Console.WriteLine();
            }
        }

        private static string GetTileDisplayRepresentation(Piece piece)
        {
            if (piece == null)
                return "-";
            else if (piece.Owner == PieceColor.Black)
                return piece.IsKing ? "B*" : "B";
            else
                return piece.IsKing ? "W*" : "W";
        }

    }

}
