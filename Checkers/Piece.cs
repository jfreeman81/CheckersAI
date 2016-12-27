using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Piece
    {
        public PieceColor Owner;
        public bool IsKing = false;
        public int Row;
        public int Col;

        public Piece(int row, int col, PieceColor owner)
        {
            this.Row = row;
            this.Col = col;
            this.Owner = owner;
        }

        public static Piece AsKing(int row, int col, PieceColor owner)
        {
            var piece = new Piece(row, col, owner);
            piece.IsKing = true;
            return piece;
        }

        public Piece(Piece copy) : this(copy.Row, copy.Col, copy.Owner)
        {
            IsKing = copy.IsKing;
        }

        public static PieceColor GetOppositeColor(PieceColor color)
        {
            return (color == PieceColor.White) ? PieceColor.Black : PieceColor.White;
        }

        public override bool Equals(object obj)
        {
            var piece = obj as Piece;
            return piece != null
                && this.Row == piece.Row
                && this.Col == piece.Col
                && this.Owner == piece.Owner
                && this.IsKing == piece.IsKing
                ;
        }

    }
}
