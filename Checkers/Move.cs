using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Move
    {
        Piece Piece { get; set; }
        MoveDirection Direction { get; set; }

        public Move(Piece piece, MoveDirection direction)
        {
            this.Piece = piece;
            this.Direction = direction;
        }
    }
}
