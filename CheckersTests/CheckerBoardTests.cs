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
        private CheckerBoard _board;
        private CheckerPiece _piece;

        [TestInitialize()]
        public void Initialize()
        {
            int row = 0;
            int col = 0;

            _board = new CheckerBoard();
            _piece = new CheckerPiece(row, col, PieceColor.White);
        }

        #region AddPiece Tests

        [TestMethod]
        public void AddPiece_WhiteNonKingPiece_AddsWithCorrectValues()
        {
            _board.AddPiece(_piece);
            Assert.AreEqual(_piece, _board.GetPiece(_piece.Row, _piece.Col));
        }

        [TestMethod]
        public void AddPiece_WhiteNonKingPiece_AddsPieceAsCopy()
        {
            _board.AddPiece(_piece);
            _piece.Row = 1;
            _piece.Col = 1;

            Assert.IsNull(_board.GetPiece(1, 1));
            Assert.IsNotNull(_board.GetPiece(0, 0));
            Assert.AreNotEqual(_piece, _board.GetPiece(0, 0));
        }

        [TestMethod]
        public void AddPiece_WhiteKingPiece_AddPiece()
        {
            _piece.IsKing = true;
            _board.AddPiece(_piece);

            Assert.AreEqual(_piece, _board.GetPiece(0, 0));
        }

        #endregion

        #region RemovePiece Tests

        [TestMethod]
        public void RemovePiece_PieceExists_PieceRemoved()
        {
            _board.AddPiece(_piece);
            _board.RemovePiece(_piece);
            Assert.IsNull(_board.GetPiece(_piece.Row, _piece.Col));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemovePiece_PieceDoesNotExist_ThrowsException()
        {
            _board.RemovePiece(_piece);
        }

        #endregion

    }
}
