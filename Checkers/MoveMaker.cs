using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class MoveMaker
    {
        private CheckerBoard _board;

        public MoveMaker(CheckerBoard board)
        {
            _board = board;
        }

        public void MakeMove(Move move)
        {
            _board.RemovePiece(move.Piece);
            MoveDirection direction = move.Direction[0];
            int rowAfterMove = move.Piece.Row + MoveUtil.GetRowMoveAmountByColor(move.Piece.Owner, direction);
            int colAfterMove = move.Piece.Col + MoveUtil.GetColMoveAmount(direction);

            var pieceAfterMove = new CheckerPiece(move.Piece);
            pieceAfterMove.Row = rowAfterMove;
            pieceAfterMove.Col = colAfterMove;
            _board.AddPiece(pieceAfterMove);
        }

    }
}
