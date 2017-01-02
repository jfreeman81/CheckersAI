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
            if (IsValidMove(move))
            {
                _board.RemovePiece(move.Piece);
                int newRow = move.Piece.Row;
                int newCol = move.Piece.Col;
                foreach (MoveDirection direction in move.Direction)
                {
                    newRow += MoveUtil.GetRowMoveAmountByColor(move.Piece.Owner, direction);
                    newCol += MoveUtil.GetColMoveAmount(direction);

                    CheckerPiece pieceInPosition = _board.GetPiece(newRow, newCol);
                    if (pieceInPosition != null)
                    {
                        _board.RemovePiece(pieceInPosition);
                        newRow += MoveUtil.GetRowMoveAmountByColor(move.Piece.Owner, direction);
                        newCol += MoveUtil.GetColMoveAmount(direction);
                    }
                }

                var pieceAfterMove = new CheckerPiece(move.Piece);
                pieceAfterMove.Row = newRow;
                pieceAfterMove.Col = newCol;
                _board.AddPiece(pieceAfterMove);
            }
            else
                throw new ArgumentException("Invalid Move");
        }

        private bool PieceCanMoveInDirection(CheckerPiece piece, MoveDirection direction)
        {
            return piece.IsKing 
                || direction == MoveDirection.ForwardLeft
                || direction == MoveDirection.ForwardRight
                ;
        }
        
        private bool IsValidMove(Move move)
        {
            var moveGenerator = new BoardMoveGenerator(_board, move.Piece.Row, move.Piece.Col);
            return moveGenerator.Moves.Contains(move);
        }

    }
}
