using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests
{
    [TestClass]
    public class MoveMakerTests
    {

        #region Normal Jumps

        [TestMethod]
        public void MakeMove_NormalWhitePieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            int currentRow = CheckerBoard.SIZE - 1;
            int currentCol = 2;

            var board = new CheckerBoard();
            var piece = new CheckerPiece(currentRow, currentCol, PieceColor.White);
            board.AddPiece(piece);
            var moveMaker = new MoveMaker(board);

            var forwardLeftMove = new Move(piece, MoveDirection.ForwardLeft);

            moveMaker.MakeMove(forwardLeftMove);
            AssertForwardLeftMoveIsCorrect(board, currentRow, currentCol, PieceColor.White);
        }

        [TestMethod]
        public void MakeMove_NormalWhitePieceForwardRightNoJump_MovesToCorrectSpot()
        {
            int currentRow = CheckerBoard.SIZE - 1;
            int currentCol = 2;

            var board = new CheckerBoard();
            var piece = new CheckerPiece(currentRow, currentCol, PieceColor.White);
            board.AddPiece(piece);
            var moveMaker = new MoveMaker(board);

            var forwardRightMove = new Move(piece, MoveDirection.ForwardRight);

            moveMaker.MakeMove(forwardRightMove);
            AssertForwardRightMoveIsCorrect(board, currentRow, currentCol, PieceColor.White);
        }

        [TestMethod]
        public void MakeMove_NormalBlackPieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            int currentRow = 0;
            int currentCol = 2;

            var board = new CheckerBoard();
            var piece = new CheckerPiece(currentRow, currentCol, PieceColor.Black);
            board.AddPiece(piece);
            var moveMaker = new MoveMaker(board);

            var forwardRightMove = new Move(piece, MoveDirection.ForwardLeft);

            moveMaker.MakeMove(forwardRightMove);
            AssertForwardLeftMoveIsCorrect(board, currentRow, currentCol, PieceColor.Black);
        }

        [TestMethod]
        public void MakeMove_NormalBlackPieceForwardRightNoJump_MovesToCorrectSpot()
        {
            int currentRow = 0;
            int currentCol = 2;

            var board = new CheckerBoard();
            var piece = new CheckerPiece(currentRow, currentCol, PieceColor.Black);
            board.AddPiece(piece);
            var moveMaker = new MoveMaker(board);
            var forwardRightMove = new Move(piece, MoveDirection.ForwardRight);

            moveMaker.MakeMove(forwardRightMove);
            AssertForwardRightMoveIsCorrect(board, currentRow, currentCol, PieceColor.Black);
        }

        #endregion

        #region King Jumps



        #endregion

        private static void AssertForwardLeftMoveIsCorrect(CheckerBoard board, int originalRow, int originalColumn, PieceColor color)
        {
            Assert.IsNull(board.GetPiece(originalRow, originalColumn));

            int newRow = originalRow + MoveUtil.GetRowMoveAmountByColor(color, MoveDirection.ForwardLeft);
            int newCol = originalColumn + MoveUtil.GetColMoveAmount(MoveDirection.ForwardLeft);
            AssertPieceExists(board, newRow, newCol, color);
        }

        private static void AssertForwardRightMoveIsCorrect(CheckerBoard board, int originalRow, int originalColumn, PieceColor color)
        {
            Assert.IsNull(board.GetPiece(originalRow, originalColumn));

            int newRow = originalRow + MoveUtil.GetRowMoveAmountByColor(color, MoveDirection.ForwardRight);
            int newCol = originalColumn + MoveUtil.GetColMoveAmount(MoveDirection.ForwardRight);
            AssertPieceExists(board, newRow, newCol, color);
        }

        private static void AssertPieceExists(CheckerBoard board, int row, int col, PieceColor color)
        {
            var piece = board.GetPiece(row, col);
            Assert.IsNotNull(piece);
            Assert.AreEqual(color, piece.Owner);
        }

    }
}
