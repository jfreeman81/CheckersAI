using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Util
{
    public class MoveBreakdown
    {
        public int FinalRow { get; set; }
        public int FinalCol { get; set; }
        public IEnumerable<CheckerPiece> RemovedPieces { get; set; }
    }
}
