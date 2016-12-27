using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class CheckerBoard
    {
        public static readonly int SIZE = 8;
        public static readonly int PIECES_DEPTH = 3;
        private Piece[,] boardState { get; set; }

        public CheckerBoard()
        {
            boardState = new Piece[SIZE, SIZE];
        }

        /// <summary>
        /// Places a piece on the board. Note: This does not
        /// take in the actual piece object to ensure that we never end up 
        /// with other places being able to modify the pieces on the board.
        /// </summary>
        /// <param name="color">color of the piece</param>
        /// <param name="row">row the piece will be placed</param>
        /// <param name="col">column the piece will be placed</param>
        public void AddPiece(PieceColor color, int row, int col, bool isKing = false)
        {
            var piece = new Piece(row, col, color);
            piece.IsKing = isKing;
            AddPiece(piece);
        }

        public void AddPiece(Piece piece)
        {
            //if (!TileIsInBounds(row, col) || (boardState[row, col] != null))
            //    throw new ArgumentException(String.Format("Error: Piece was attempted to be placed at invalid position Row: {0}, Col: {1}", row, col));
            var copyOfPiece = new Piece(piece);
            boardState[copyOfPiece.Row, copyOfPiece.Col] = copyOfPiece;
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
            bool useFlippedBoard = player == PieceColor.White;
            CheckerBoard boardToCheck = useFlippedBoard ? FlipBoard() : this;
            var legalMoves = new List<Move>();
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    var piece = boardToCheck.GetPiece(i, j);
                    if ((piece == null) || (piece.Owner == opposingColor))
                        continue;
                    var moves = new BoardMoveGenerator(boardToCheck, piece.Row, piece.Col).Moves;
                    if (useFlippedBoard)
                    {
                        foreach (var move in moves)
                            move.Piece.Row = SIZE - piece.Row - 1; // must be flipped back along the x axis
                    }
                    legalMoves.AddRange(moves);
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
            return (direction == MoveDirection.ForwardLeft || direction == MoveDirection.ForwardRight) ? 1 : -1;
        }

        public static int GetColMoveAmount(MoveDirection direction)
        {
            return (direction == MoveDirection.ForwardRight || direction == MoveDirection.BackwardRight) ? 1 : -1;
        }

        private static bool TileIsInBounds(int row, int col)
        {
            return row >= 0
                && row < SIZE
                && col >= 0
                && col < SIZE
                ;
        }

        /// <summary>
        /// Returns a deep copy
        /// of the current board
        /// flipped about the the x-axis.
        /// </summary>
        /// <returns></returns>
        public CheckerBoard FlipBoard()
        {
            var reversedBoard = new CheckerBoard();

            for(int i = 0; i < SIZE; i++)
            {
                int row = SIZE - i - 1;
                for (int j = 0; j < SIZE; j++)
                {
                    Piece pieceToCopy = boardState[row, j];
                    if (pieceToCopy != null)
                        reversedBoard.AddPiece(pieceToCopy.Owner, i, j);
                }
            }

            return reversedBoard;
        }
    }
}
