using Checkers;
using CheckersTests.Util;
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

        private static CheckerPiece _bottomLeftNonCornerWhitePiece = new CheckerPiece(CheckerBoard.SIZE - 1, 2, PieceColor.White);
        private static CheckerPiece _bottomLeftBlackPieceToJumpRight = new CheckerPiece(CheckerBoard.SIZE - 2, 3, PieceColor.Black);
        private static CheckerPiece _bottomLeftBlackPieceToJumpLeft = new CheckerPiece(CheckerBoard.SIZE - 2, 1, PieceColor.Black);
        private static CheckerPiece _topLeftNonCornerBlackPiece = new CheckerPiece(0, 2, PieceColor.Black);
        private static CheckerPiece _bottomLeftNonCornerWhiteKing = CheckerPiece.AsKing(CheckerBoard.SIZE - 1, 2, PieceColor.White);
        private static CheckerPiece _topLeftNonCornerBlackKing = CheckerPiece.AsKing(0, 2, PieceColor.Black);

        #region Normal Moves

        [TestMethod]
        public void MakeMove_NormalWhitePieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            var forwardLeftMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.ForwardLeft);
            AssertMoveCanBeMadeCorrectly(forwardLeftMove);
        }

        [TestMethod]
        public void MakeMove_NormalWhitePieceForwardRightNoJump_MovesToCorrectSpot()
        {
            var forwardRightMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.ForwardRight);
            AssertMoveCanBeMadeCorrectly(forwardRightMove);
        }

        [TestMethod]
        public void MakeMove_NormalBlackPieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            var forwardLeftMove = new Move(_topLeftNonCornerBlackPiece, MoveDirection.ForwardLeft);
            AssertMoveCanBeMadeCorrectly(forwardLeftMove);
        }

        [TestMethod]
        public void MakeMove_NormalBlackPieceForwardRightNoJump_MovesToCorrectSpot()
        {
            var forwardRightMove = new Move(_topLeftNonCornerBlackPiece, MoveDirection.ForwardRight);
            AssertMoveCanBeMadeCorrectly(forwardRightMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalWhitePieceBackwardLeft_ThrowsException()
        {
            var invalidMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.BackwardLeft);
            CreateBoardAndMakeMove(invalidMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalWhitePieceBackwardRight_ThrowsException()
        {
            var invalidMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.BackwardRight);
            CreateBoardAndMakeMove(invalidMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalBlackPieceBackwardLeft_ThrowsException()
        {
            var invalidMove = new Move(_topLeftNonCornerBlackPiece, MoveDirection.BackwardLeft);
            CreateBoardAndMakeMove(invalidMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalBlackPieceBackwardRight_ThrowsException()
        {
            var invalidMove = new Move(_topLeftNonCornerBlackPiece, MoveDirection.BackwardRight);
            CreateBoardAndMakeMove(invalidMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalWhitePieceMutltiMoveWithBackwards_ThrowsException()
        {
            var multiDirectionMove = new Move(_bottomLeftNonCornerWhitePiece, new List<MoveDirection>
            {
                MoveDirection.ForwardLeft,
                MoveDirection.BackwardLeft
            });
            CreateBoardAndMakeMove(multiDirectionMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalWhitePieceOutOfBounds_ThrowsException()
        {
            var multiDirectionMove = new Move(_bottomLeftNonCornerWhitePiece, new List<MoveDirection>
            {
                MoveDirection.ForwardLeft,
                MoveDirection.ForwardLeft,
                MoveDirection.ForwardLeft
            });
            CreateBoardAndMakeMove(multiDirectionMove);
        }

        #endregion

        #region King Moves

        [TestMethod]
        public void MakeMove_KingWhitePieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            var forwardLeftMove = new Move(_bottomLeftNonCornerWhiteKing, MoveDirection.ForwardLeft);
            AssertMoveCanBeMadeCorrectly(forwardLeftMove);
        }

        [TestMethod]
        public void MakeMove_KingWhitePieceForwardRightNoJump_MovesToCorrectSpot()
        {
            var forwardRightMove = new Move(_bottomLeftNonCornerWhiteKing, MoveDirection.ForwardRight);
            AssertMoveCanBeMadeCorrectly(forwardRightMove);
        }

        [TestMethod]
        public void MakeMove_KingBlackPieceForwardLeftNoJump_MovesToCorrectSpot()
        {
            var forwardLeftMove = new Move(_topLeftNonCornerBlackKing, MoveDirection.ForwardLeft);
            AssertMoveCanBeMadeCorrectly(forwardLeftMove);
        }

        [TestMethod]
        public void MakeMove_KingBlackPieceForwardRightNoJump_MovesToCorrectSpot()
        {
            var forwardRightMove = new Move(_topLeftNonCornerBlackKing, MoveDirection.ForwardRight);
            AssertMoveCanBeMadeCorrectly(forwardRightMove);
        }

        #endregion

        #region Captures

        [TestMethod]
        public void MakeMove_NormalPieceSingleJump_PiecesMovedCorrectly()
        {
            CheckerBoard board = CreateBoardWithPieces(new List<CheckerPiece>
            {
                _bottomLeftNonCornerWhitePiece, _bottomLeftBlackPieceToJumpLeft
            });
            Move forwardLeftWhiteMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.ForwardLeft);
            AssertMoveCanBeMadeCorrectly(forwardLeftWhiteMove, board);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_NormalPieceAttemptToJumpOverSamePiece_ThrowsException()
        {
            var pieceToTryAndJump = new CheckerPiece(_bottomLeftBlackPieceToJumpLeft.Row, _bottomLeftBlackPieceToJumpLeft.Col, PieceColor.White);
            CheckerBoard board = CreateBoardWithPieces(new List<CheckerPiece>
            {
                _bottomLeftNonCornerWhitePiece, pieceToTryAndJump
            });
            Move forwardLeftWhiteMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.ForwardLeft);
            MakeMove(board, forwardLeftWhiteMove);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeMove_AttemptToNotTakeLongestJump_ThrowsException()
        {
            var pieceToTryAndJumpRightFirst = new CheckerPiece(_bottomLeftBlackPieceToJumpLeft.Row, 3, PieceColor.Black);
            var pieceToTryAndJumpRightSecond = new CheckerPiece(_bottomLeftBlackPieceToJumpLeft.Row - 2, 5, PieceColor.Black);
            CheckerBoard board = CreateBoardWithPieces(new List<CheckerPiece>
            {
                _bottomLeftNonCornerWhitePiece, _bottomLeftBlackPieceToJumpLeft, pieceToTryAndJumpRightFirst, pieceToTryAndJumpRightSecond
            });
            Move forwardLeftWhiteMove = new Move(_bottomLeftNonCornerWhitePiece, MoveDirection.ForwardLeft);
            MakeMove(board, forwardLeftWhiteMove);
        }

        #endregion

        private static void AssertMoveCanBeMadeCorrectly(Move move, CheckerBoard board = null)
        {
            MoveBreakdown moveBreakdown;
            if (board == null)
            {
                board = CreateBoardWithPieces(new List<CheckerPiece> { move.Piece });
                moveBreakdown = new MoveBreakdownCreator(board, move).Breakdown;
            }
            else
            {
                moveBreakdown = new MoveBreakdownCreator(board, move).Breakdown;
            }
            MakeMove(board, move);
            AssertMoveWasMadeCorrectly(board, move, moveBreakdown);
        }

        private static CheckerBoard CreateBoardAndMakeMove(Move move)
        {
            CheckerBoard board = CreateBoardWithPieces(new List<CheckerPiece> { move.Piece });
            MakeMove(board, move);
            return board;
        }

        private static void MakeMove(CheckerBoard board, Move move)
        {
            var moveMaker = new MoveMaker(board);
            moveMaker.MakeMove(move);
        }

        private static CheckerBoard CreateBoardWithPieces(IEnumerable<CheckerPiece> pieces)
        {
            var board = new CheckerBoard();
            foreach(var piece in pieces)
            {
                board.AddPiece(piece);
            }
            return board;
        }

        private static void AssertMoveWasMadeCorrectly(CheckerBoard board, Move move, MoveBreakdown moveBreakdown)
        {
            AssertPieceExists(board, moveBreakdown.FinalRow, moveBreakdown.FinalCol, move.Piece.Owner);
            AssertPiecesRemoved(board, moveBreakdown.RemovedPieces);
        }

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

        private static void AssertBackwardLeftMoveIsCorrect(CheckerBoard board, int originalRow, int originalColumn, PieceColor color)
        {
            Assert.IsNull(board.GetPiece(originalRow, originalColumn));

            int newRow = originalRow + MoveUtil.GetRowMoveAmountByColor(color, MoveDirection.BackwardLeft);
            int newCol = originalColumn + MoveUtil.GetColMoveAmount(MoveDirection.BackwardLeft);
            AssertPieceExists(board, newRow, newCol, color);
        }

        private static void AssertBackwardRightMoveIsCorrect(CheckerBoard board, int originalRow, int originalColumn, PieceColor color)
        {
            Assert.IsNull(board.GetPiece(originalRow, originalColumn));

            int newRow = originalRow + MoveUtil.GetRowMoveAmountByColor(color, MoveDirection.BackwardRight);
            int newCol = originalColumn + MoveUtil.GetColMoveAmount(MoveDirection.BackwardRight);
            AssertPieceExists(board, newRow, newCol, color);
        }

        private static void AssertPieceExists(CheckerBoard board, int row, int col, PieceColor color)
        {
            var piece = board.GetPiece(row, col);
            Assert.IsNotNull(piece);
            Assert.AreEqual(color, piece.Owner);
        }

        private static void AssertPiecesRemoved(CheckerBoard board, IEnumerable<CheckerPiece> pieces)
        {
            foreach(var piece in pieces)
            {
                Assert.AreNotEqual(piece, board.GetPiece(piece.Row, piece.Col));
            }
        }

    }
}
