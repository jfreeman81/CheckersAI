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
        public IList<MoveDirection> Direction { get; set; }

        public Move(Piece piece, IEnumerable<MoveDirection> direction)
        {
            this.Piece = piece;
            this.Direction = direction.ToList();
        }

        public override bool Equals(object obj)
        {
            var move = obj as Move;
            if (!this.Piece.Equals(move.Piece) || this.Direction.Count != move.Direction.Count)
                return false;
            for(int i = 0; i < this.Direction.Count; ++i)
            {
                if (this.Direction[i] != move.Direction[i]) // Order matters when comparing directions
                    return false;
            }
            return true;
        }

    }
}
