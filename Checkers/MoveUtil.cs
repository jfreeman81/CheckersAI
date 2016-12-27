using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static class MoveUtil
    {

        private static bool TileInDirectionIsFree(this Board board, Tile currentTile, MoveDirection direction)
        {
            int rowInDirection = currentTile.Row + GetRowMoveAmount(direction);
            int colInDirection = currentTile.Col + GetColMoveAmount(direction);
            Tile tileInDirection = Tile.FromRowCol(rowInDirection, colInDirection);
            return Board.TileIsInBounds(tileInDirection) && board.GetPiece(tileInDirection) == null;
        }

        public static bool ForwardLeftTileIsFree(this Borad board, Tile currentTile, MoveDirection direction)
        {
            return board.TileInDirectionIsFree(currentTile, direction);
        }

        public static bool TileIsOpposingColor(this Board board, Tile tile, PieceColor opposingColor)
        {
            Piece piece = board.GetPiece(tile);
            return (piece != null) && (piece.Owner == opposingColor);
        }

        public static int GetRowMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardLeft || direction == MoveDirection.ForwardRight) ? 1 : -1;
        }

        public static int GetColMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardRight || direction == MoveDirection.BackwardRight) ? 1 : -1;
        }

    }
}
