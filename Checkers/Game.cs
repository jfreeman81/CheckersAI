using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Game
    {
        private Board Board { get; set; }

        public Game()
        {
            Board = new Board();
            InitBoard();
        }

        private void InitBoard()
        {
            AddBlackPieces();
            AddWhitePieces();
        }

        private void AddBlackPieces()
        {
            bool beginRowWithPiece = false; // Top left does not begin with a piece for black
            for (int i = 0; i < Board.PIECES_DEPTH; i++)
            {
                AddPiecesToRow(PieceColor.Black, i, beginRowWithPiece);
                beginRowWithPiece = !beginRowWithPiece;
            }
        }

        private void AddWhitePieces()
        {
            bool beginRowWithPiece = true;
            for (int i = 0; i < Board.PIECES_DEPTH; i++)
            {
                int row = Board.BOARD_SIZE - i - 1; // white starts at the bottom
                AddPiecesToRow(PieceColor.White, row, beginRowWithPiece);
                beginRowWithPiece = !beginRowWithPiece;
            }
        }

        private void AddPiecesToRow(PieceColor color, int row, bool beginsWithPiece)
        {
            bool placePiece = beginsWithPiece;
            for (int j = 0; j < Board.BOARD_SIZE; j++)
            {
                if (placePiece)
                    Board.PlacePiece(color, row, j);
                placePiece = !placePiece;
            }
        }
    }
}
