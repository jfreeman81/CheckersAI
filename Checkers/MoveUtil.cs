using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static class MoveUtil
    {

        public static bool TileIsOpposingColor(this CheckerBoard board, int row, int col, PieceColor opposingColor)
        {
            CheckerPiece piece = board.GetPiece(row, col);
            return (piece != null) && (piece.Owner == opposingColor);
        }

        public static int GetRowMoveAmountByColor(PieceColor color, MoveDirection direction)
        {
            if (color == PieceColor.White)
                return GetRowMoveAmountForWhite(direction);
            else
                return GetRowMoveAmountForBlack(direction);
        }

        public static int GetRowMoveAmountForWhite(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardLeft || direction == MoveDirection.ForwardRight) ? -1 : 1;
        }

        public static int GetRowMoveAmountForBlack(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardLeft || direction == MoveDirection.ForwardRight) ? 1 : -1;
        }

        public static int GetColMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardRight || direction == MoveDirection.BackwardRight) ? 1 : -1;
        }

    }
}
