using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public interface IBoardUIDisplay
    {
        void UpdateDisplay(CheckerBoard board);
    }

    public class BoardPlainTextUIDisplay : IBoardUIDisplay
    {

        private ITextDisplay _display;

        public BoardPlainTextUIDisplay(ITextDisplay display)
        {
            _display = display;
        }

        public void UpdateDisplay(CheckerBoard board)
        {
            for (int row = 0; row < CheckerBoard.SIZE; row++)
            {
                for (int col = 0; col < CheckerBoard.SIZE; col++)
                {
                    CheckerPiece tile = board.GetPiece(row, col);
                    string tileRep = GetTileDisplayRepresentationWithPadding(tile);
                    _display.DisplayText(tileRep);
                }
                _display.DisplayText($"{Environment.NewLine}");
            }
        }

        private static string GetTileDisplayRepresentationWithPadding(CheckerPiece piece)
        {
            if (piece == null)
                return "-  ";
            else if (piece.Owner == PieceColor.Black)
                return piece.IsKing ? "B* " : "B  ";
            else
                return piece.IsKing ? "W* " : "W  ";
        }

    }

    public interface ITextDisplay
    {
        void DisplayText(string text);
    }

    public class CommandLineDisplay : ITextDisplay
    {
        public void DisplayText(string text)
        {
            Console.WriteLine(text);
        }
    }

}
