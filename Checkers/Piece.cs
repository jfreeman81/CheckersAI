using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class CheckerPiece
    {
        public PieceColor Owner;
        public bool IsKing = false;
        public int Row;
        public int Col;

        public CheckerPiece(int row, int col, PieceColor owner)
        {
            this.Row = row;
            this.Col = col;
            this.Owner = owner;
        }

        public static CheckerPiece AsKing(int row, int col, PieceColor owner)
        {
            var piece = new CheckerPiece(row, col, owner);
            piece.IsKing = true;
            return piece;
        }

        public CheckerPiece(CheckerPiece copy) : this(copy.Row, copy.Col, copy.Owner)
        {
            IsKing = copy.IsKing;
        }

        public static PieceColor GetOppositeColor(PieceColor color)
        {
            return (color == PieceColor.White) ? PieceColor.Black : PieceColor.White;
        }

        public override bool Equals(object obj)
        {
            var piece = obj as CheckerPiece;
            return piece != null
                && this.Row == piece.Row
                && this.Col == piece.Col
                && this.Owner == piece.Owner
                && this.IsKing == piece.IsKing
                ;
        }

    }
}
