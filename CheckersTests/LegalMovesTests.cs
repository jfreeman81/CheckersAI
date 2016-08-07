using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkers;

namespace CheckersTests
{
    [TestClass]
    public class LegalMovesTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var board = new Board();
            board.PlacePiece(PieceColor.White, Board.BOARD_SIZE - 1, 0);
            board.PlacePiece(PieceColor.Black, Board.BOARD_SIZE - 2, 1); // One below and to the right
            var whiteMoves = board.GetLegalMoves(PieceColor.White);
            var blackMoves = board.GetLegalMoves(PieceColor.Black);
            Assert.AreEqual(1, whiteMoves.Count);
            Assert.AreEqual(1, blackMoves.Count);
            var whiteMove = new Move(board.GetPiece(Board.BOARD_SIZE - 1, 0), new List<MoveDirection>  { MoveDirection.ForwardRight });
            var blackMove = new Move(board.GetPiece(Board.BOARD_SIZE - 2, 1), new List<MoveDirection> { MoveDirection.ForwardRight });
            Assert.AreEqual(whiteMove, whiteMoves[0]);
            Assert.AreEqual(blackMove, blackMoves[0]);
        }
    }
}
