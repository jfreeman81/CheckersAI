using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Move
    {
        public Piece Piece { get; set; }
        public MoveDirection Direction { get; set; }

        public Move(Piece piece, MoveDirection direction)
        {
            this.Piece = piece;
            this.Direction = direction;
        }
    }
}
