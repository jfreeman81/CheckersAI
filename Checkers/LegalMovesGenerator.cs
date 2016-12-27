using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class LegalMovesGenerator
    {
        private CheckerBoard _board;

        public LegalMovesGenerator(CheckerBoard board)
        {
            _board = board;
        }

        public IList<Move> GetLegalMovesForWhite()
        {
            for (int row = 0; row < CheckerBoard.BOARD_SIZE; ++row)
            {
                for (int col = 0; col < CheckerBoard.BOARD_SIZE; ++col)
                {

                }
            }
        }

        public IList<Move> GetLegalMovesForBlack()
        {
            return new List<Move>
            {
                new Move(new Piece(CheckerBoard.BOARD_SIZE - 3, 0, PieceColor.Black), new List<MoveDirection> { MoveDirection.ForwardRight })
            }; 
        }

    }
}
