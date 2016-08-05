using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Game
    {
        Board Board = new Board();

        public void PrintBoard()
        {
            Board.Print();
        }

        public void Test()
        {
            var moves = Board.GetLegalMoves(PieceColor.Black);
        }
    }
}
