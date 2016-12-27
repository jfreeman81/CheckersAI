using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkers;

namespace CheckersTests
{
    [TestClass]
    public class LegalMovesTests
    {
        /// <summary>
        /// board setup:
        /// -  -  -  
        /// -  B  -  
        /// W  -  -  
        /// </summary>
        [TestMethod]
        public void TestLegalJumps()
        {
            var board = new CheckerBoard();
            board.AddPiece(PieceColor.White, CheckerBoard.SIZE - 1, 0);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 2, 1); // One below and to the right
            var whiteMoves = board.GetLegalMoves(PieceColor.White);
            var blackMoves = board.GetLegalMoves(PieceColor.Black);
            Assert.AreEqual(1, whiteMoves.Count);
            Assert.AreEqual(1, blackMoves.Count);
            var whiteMove = new Move(board.GetPiece(CheckerBoard.SIZE - 1, 0), new List<MoveDirection>  { MoveDirection.ForwardRight });
            var blackMove = new Move(board.GetPiece(CheckerBoard.SIZE - 2, 1), new List<MoveDirection> { MoveDirection.ForwardRight });
            Assert.AreEqual(whiteMove, whiteMoves[0]);
            Assert.AreEqual(blackMove, blackMoves[0]);
        }
        
        /// <summary>
        /// board setup:
        /// -  -  -  -  -
        /// -  -  -  B  -
        /// -  -  -  -  -
        /// -  B  -  -  -
        /// W  -  -  -  -
        /// </summary>
        [TestMethod]
        public void TestMultipleJumps()
        {
            var board = new CheckerBoard();
            board.AddPiece(PieceColor.White, CheckerBoard.SIZE - 1, 0);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 2, 1);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 4, 3);
            var whiteMoves = board.GetLegalMoves(PieceColor.White);
            Assert.AreEqual(1, whiteMoves.Count);
            var whiteMove = new Move(board.GetPiece(CheckerBoard.SIZE - 1, 0), new List<MoveDirection>
            {
                MoveDirection.ForwardRight,
                MoveDirection.ForwardRight
            });
            Assert.AreEqual(whiteMove, whiteMoves[0]);
        }

        /// <summary>
        /// board setup:
        /// -  -  -  -  -  -  -
        /// -  -  -  -  -  B  -
        /// -  -  -  -  -  -  -
        /// -  B  -  B  -  -  -
        /// -  -  W  -  -  -  -
        /// </summary>
        [TestMethod]
        public void TestLongestJump()
        {
            var board = new CheckerBoard();
            board.AddPiece(PieceColor.White, CheckerBoard.SIZE - 1, 2);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 2, 1);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 2, 3);
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 4, 5);
            var whiteMoves = board.GetLegalMoves(PieceColor.White);
            Assert.AreEqual(1, whiteMoves.Count);
            var whiteMove = new Move(board.GetPiece(CheckerBoard.SIZE - 1, 2), new List<MoveDirection>
            {
                MoveDirection.ForwardRight,
                MoveDirection.ForwardRight
            });
            Assert.AreEqual(whiteMove, whiteMoves[0]);
        }

        /// <summary>
        /// -  -  -  
        /// -  W  -  
        /// B* -  -  
        /// </summary>
        [TestMethod]
        public void TestKingJump()
        {
            var board = new CheckerBoard();
            board.AddPiece(PieceColor.Black, CheckerBoard.SIZE - 1, 0);
            board.GetPiece(CheckerBoard.SIZE - 1, 0).IsKing = true;
            board.AddPiece(PieceColor.White, CheckerBoard.SIZE - 2, 1);
            //board.PlacePiece(PieceColor.White, Board.BOARD_SIZE - 4, 3);
            var blackMoves = board.GetLegalMoves(PieceColor.Black);
            Assert.AreEqual(1, blackMoves.Count);
            var blackMove = new Move(board.GetPiece(CheckerBoard.SIZE - 1, 0), new List<MoveDirection> { MoveDirection.BackwardRight });
            Assert.AreEqual(blackMove, blackMoves[0]);
        }
    }
}
