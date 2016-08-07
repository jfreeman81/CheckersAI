using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Board
    {
        public static readonly int BOARD_SIZE = 8;
        public static readonly int PIECES_DEPTH = 3;
        private Piece[,] boardState { get; set; }

        public Board()
        {
            boardState = new Piece[BOARD_SIZE, BOARD_SIZE];
        }

        /// <summary>
        /// Places a piece on the board. Note: This does not
        /// take in the actual piece object to ensure that we never end up 
        /// with other places being able to modify the pieces on the board.
        /// </summary>
        /// <param name="color">color of the piece</param>
        /// <param name="row">row the piece will be placed</param>
        /// <param name="col">column the piece will be placed</param>
        public void PlacePiece(PieceColor color, int row, int col)
        {
            if (!TileIsInBounds(row, col) || (boardState[row, col] != null))
                throw new ArgumentException(String.Format("Error: Piece was attempted to be placed at invalid position Row: {0}, Col: {1}", row, col));
            boardState[row, col] = new Piece(row, col, color);
        }

        /// <summary>
        /// Removes the piece from the board.
        /// Throws an exception if a piece is not there
        /// or the position is out of bounds.
        /// </summary>
        /// <param name="row">row the piece will be placed</param>
        /// <param name="col">column the piece will be placed</param>
        public void RemovePiece(int row, int col)
        {
            if (!TileIsInBounds(row, col) || (boardState[row, col] == null))
                throw new ArgumentException(String.Format("Error: Attempted to remove at invalid position Row: {0}, Col: {1}", row, col));
            boardState[row, col] = null;
        }

        public Piece GetPiece(int row, int col)
        {
            if (!TileIsInBounds(row, col))
                throw new ArgumentException("Tile position is out of bounds.");
            return boardState[row, col];
        }

        public void Print()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Piece tile = boardState[i, j];
                    string tileRep = GetTileRepresentation(tile);
                    Console.Write("{0,3}", tileRep);
                }
                Console.WriteLine();
            }
        }

        private static string GetTileRepresentation(Piece piece)
        {
            if (piece == null)
                return "-";
            else
                return (piece.Owner == PieceColor.Black) ? "B" : "W";
        }

        //public bool MakeMove(Move move)
        //{
            //PieceColor thisColor = move.Piece.Owner;
            //if (!GetLegalMoves(thisColor).Contains(move))
            //    return false;

            //var tempBoard = (move.Piece.Owner == PieceColor.White) ? FlipBoard() : boardState;
            //var opposingColor = Piece.GetOppositeColor(move.Piece.Owner);
            //int rowMove = GetRowMoveAmount(move.Direction);
            //int colMove = GetColMoveAmount(move.Direction);

            //if (TileCanBeJumped(move.Piece.Row + rowMove, move.Piece.Col + colMove, opposingColor, boardState, move.Direction))
            //{
            //    // move piece two times further
            //    // capture opposing piece
            //    // check that piece can't jump again
            //    if (PieceCanJump(move.Piece, opposingColor, boardState))
            //    {
            //        // get move from player to jump
            //    }
            //}
            //else
            //{
            //    // move piece
            //}
            //return true;
        //}

        public List<Move> GetLegalMoves(PieceColor player)
        {
            PieceColor opposingColor = Piece.GetOppositeColor(player);
            var checkBoard = (player == PieceColor.White) ? FlipBoard() : this;
            var legalMoves = new List<Move>();
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    var piece = checkBoard.GetPiece(i, j);
                    if ((piece == null) || (piece.Owner == opposingColor))
                        continue;
                    legalMoves.AddRange(new BoardMoveGenerator(checkBoard, piece.Row, piece.Col).Moves);
                }
            }
            return legalMoves;
        }


        public bool TileIsFree(int row, int col, MoveDirection direction)
        {
            row += GetRowMoveAmount(direction);
            col += GetColMoveAmount(direction);
            return TileIsInBounds(row, col) && boardState[row, col] == null;
        }

        public bool TileIsOpposingColor(int row, int col, PieceColor opposingColor)
        {
            return (boardState[row, col] != null) && (boardState[row, col].Owner == opposingColor);
        }

        public static int GetRowMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardRight || direction == MoveDirection.ForwardLeft) ? 1 : -1;
        }

        public static int GetColMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardRight || direction == MoveDirection.BackwardLeft) ? 1 : -1;
        }

        private static bool TileIsInBounds(int row, int col)
        {
            return row >= 0
                && row < BOARD_SIZE
                && col >= 0
                && col < BOARD_SIZE
                ;
        }

        /// <summary>
        /// Returns a deep copy
        /// of the current board
        /// with flipped about the the x-axis.
        /// </summary>
        /// <returns></returns>
        public Board FlipBoard()
        {
            var reversedBoard = new Board();

            for(int i = 0; i < BOARD_SIZE; i++)
            {
                int row = BOARD_SIZE - i - 1;
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    Piece pieceToCopy = boardState[row, j];
                    if (pieceToCopy != null)
                        reversedBoard.PlacePiece(pieceToCopy.Owner, i, j);
                }
            }

            return reversedBoard;
        }
    }
}
