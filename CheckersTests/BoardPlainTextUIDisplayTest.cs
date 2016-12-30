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
    public class BoardPlainTextUIDisplayTest
    {
        private static readonly string EMPTY_ROW = $"-  -  -  -  -  -  -  -  {Environment.NewLine}";

        [TestMethod]
        public void TestDisplayEmptyBoard()
        {
            var board = new CheckerBoard();
            var display = new Mocks.MockTextDisplay();
            var uiDisplay = new BoardPlainTextUIDisplay(display);

            uiDisplay.UpdateDisplay(board);

            Assert.AreEqual(GetEmptyBoardDisplay(), display.Text);
        }

        [TestMethod]
        public void TestDisplayIndividualPieces()
        {
            var board = new CheckerBoard();
            board.AddPiece(PieceColor.White, 0, 0);
            board.AddPiece(PieceColor.Black, 1, 1);

            string result = $"W  -  -  -  -  -  -  -  {Environment.NewLine}";
            result += $"-  B  -  -  -  -  -  -  {Environment.NewLine}";
            for (int row = 2; row < CheckerBoard.SIZE; ++row)
            {
                result += EMPTY_ROW;
            }

            var display = new Mocks.MockTextDisplay();
            var uiDisplay = new BoardPlainTextUIDisplay(display);
            uiDisplay.UpdateDisplay(board);

            Assert.AreEqual(result, display.Text);
        }

        [TestMethod]
        public void TestDisplayKingsOnLeftSide()
        {
            var board = new CheckerBoard();
            var whiteKing = CheckerPiece.AsKing(0, 0, PieceColor.White);
            var blackKing = CheckerPiece.AsKing(1, 1, PieceColor.Black);
            board.AddPiece(whiteKing);
            board.AddPiece(blackKing);

            string result = $"W* -  -  -  -  -  -  -  {Environment.NewLine}";
            result += $"-  B* -  -  -  -  -  -  {Environment.NewLine}";
            for (int row = 2; row < CheckerBoard.SIZE; ++row)
            {
                result += EMPTY_ROW;
            }

            var display = new Mocks.MockTextDisplay();
            var uiDisplay = new BoardPlainTextUIDisplay(display);
            uiDisplay.UpdateDisplay(board);

            Assert.AreEqual(result, display.Text);
        }

        [TestMethod]
        public void TestDisplayKingsOnRightSide()
        {
            var board = new CheckerBoard();
            var whiteKing = CheckerPiece.AsKing(0, CheckerBoard.SIZE - 1, PieceColor.White);
            var blackKing = CheckerPiece.AsKing(1, CheckerBoard.SIZE - 1, PieceColor.Black);
            board.AddPiece(whiteKing);
            board.AddPiece(blackKing);

            string result = $"-  -  -  -  -  -  -  W* {Environment.NewLine}";
            result += $"-  -  -  -  -  -  -  B* {Environment.NewLine}";
            for (int row = 2; row < CheckerBoard.SIZE; ++row)
            {
                result += EMPTY_ROW;
            }

            var display = new Mocks.MockTextDisplay();
            var uiDisplay = new BoardPlainTextUIDisplay(display);
            uiDisplay.UpdateDisplay(board);

            Assert.AreEqual(result, display.Text);
        }


        private static string GetEmptyBoardDisplay()
        {
            string display = string.Empty;
            for (int row = 0; row < CheckerBoard.SIZE; ++row)
            {
                display += EMPTY_ROW;
            }
            return display;
        }
    }
}
