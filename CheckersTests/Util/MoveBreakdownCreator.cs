using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Util
{
    public class MoveBreakdownCreator
    {

        private CheckerBoard _board;
        private Move _move;

        public MoveBreakdown Breakdown
        {
            get
            {
                return CreateBreakdown();
            }
        }

        public MoveBreakdownCreator(CheckerBoard board, Move move)
        {
            _board = board;
            _move = move;
        }

        private MoveBreakdown CreateBreakdown()
        {
            int newRow = _move.Piece.Row;
            int newCol = _move.Piece.Col;
            var removedPieces = new List<CheckerPiece>();
            foreach (MoveDirection direction in _move.Direction)
            {
                newRow += MoveUtil.GetRowMoveAmountByColor(_move.Piece.Owner, direction);
                newCol += MoveUtil.GetColMoveAmount(direction);

                // adjust for jump
                CheckerPiece pieceAtPosition = _board.GetPiece(newRow, newCol);
                if (pieceAtPosition != null && pieceAtPosition.Owner != _move.Piece.Owner)
                {
                    removedPieces.Add(pieceAtPosition);
                    newRow += MoveUtil.GetRowMoveAmountByColor(_move.Piece.Owner, direction);
                    newCol += MoveUtil.GetColMoveAmount(direction);
                }
            }
            return new MoveBreakdown
            {
                FinalRow = newRow,
                FinalCol = newCol,
                RemovedPieces = removedPieces
            };
        }

    }

}
