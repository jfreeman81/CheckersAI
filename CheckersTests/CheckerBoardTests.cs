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
    public class CheckerBoardTests
    {

        #region AddPiece Tests

        [TestMethod]
        public void AddPiece_WhiteNonKingPiece_AddsWithCorrectValues()
        {
            int row = 0;
            int col = 0;

            var board = new CheckerBoard();
            var piece = new Piece(row, col, PieceColor.White);
            board.AddPiece(piece);

            Assert.AreEqual(piece, board.GetPiece(row, col));
        }

        [TestMethod]
        public void AddPiece_WhiteNonKingPiece_AddsPieceAsCopy()
        {
            int row = 0;
            int col = 0;

            var board = new CheckerBoard();
            var piece = new Piece(row, col, PieceColor.White);
            board.AddPiece(piece);
            piece.Row = 1;
            piece.Col = 1;

            Assert.IsNull(board.GetPiece(1, 1));
            Assert.IsNotNull(board.GetPiece(0, 0));
            Assert.AreNotEqual(piece, board.GetPiece(0, 0));
        }

        [TestMethod]
        public void AddPiece_WhiteKingPiece_AddPiece()
        {
            int row = 0;
            int col = 0;

            var board = new CheckerBoard();
            var piece = new Piece(row, col, PieceColor.White);
            piece.IsKing = true;
            board.AddPiece(piece);

            Assert.AreEqual(piece, board.GetPiece(0, 0));
        }

        #endregion

    }
}
