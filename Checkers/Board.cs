using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Board
    {
        private static readonly int BOARD_SIZE = 8;
        private static readonly int PIECES_DEPTH = 3;
        private Piece[,] boardState { get; set; }

        public Board()
        {
            boardState = new Piece[BOARD_SIZE, BOARD_SIZE];

            List<Piece> row = new List<Piece>();

            AddBlackPieces();
            AddWhitePieces();
        }

        private void AddBlackPieces()
        {
            bool beginRowWithPiece = false; // Top left does not begin with a piece for black
            for (int i = 0; i < PIECES_DEPTH; i++)
            {
                AddPiecesToRow(PieceColor.Black, i, beginRowWithPiece);
                beginRowWithPiece = !beginRowWithPiece;
            }
        }

        private void AddWhitePieces()
        {
            bool beginRowWithPiece = true;
            for (int i = 0; i < PIECES_DEPTH; i++)
            {
                int row = BOARD_SIZE - i- 1; // white starts at the bottom
                AddPiecesToRow(PieceColor.White, row, beginRowWithPiece);
                beginRowWithPiece = !beginRowWithPiece;
            }
        }

        private void AddPiecesToRow(PieceColor color, int row, bool beginsWithPiece)
        {
            bool placePiece = beginsWithPiece;
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                if (placePiece)
                    boardState[row, j] = new Piece(row, j, color);
                placePiece = !placePiece;
            }
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

        public bool MakeMove(Move move)
        {
            return false;
        }

        public List<Move> GetLegalMoves(PieceColor player)
        {
            PieceColor opposingColor = Piece.GetOppositeColor(player);
            var checkBoard = (player == PieceColor.White) ? FlipBoard() : boardState;
            var legalMoves = new List<Move>();
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    var piece = boardState[i, j];
                    if ((piece == null) || (piece.Owner == opposingColor))
                        continue;

                    int row = piece.Row;
                    int col = piece.Col;

                    if (ForwardRightTileIsFree(row, col, opposingColor, checkBoard))
                    {
                        legalMoves.Add(new Move(piece, MoveDirection.ForwardRight));
                    }
                    if (ForwardLeftTileIsFree(row, col, opposingColor, checkBoard))
                    {
                        legalMoves.Add(new Move(piece, MoveDirection.ForwardLeft));
                    }
                    if (piece.IsKing)
                    {
                        if (BackLeftTileIsFree(row, col, opposingColor, checkBoard))
                        {
                            legalMoves.Add(new Move(piece, MoveDirection.BackwardLeft));
                        }
                        if (BackRightTileIsFree(row, col, opposingColor, checkBoard))
                        {
                            legalMoves.Add(new Move(piece, MoveDirection.BackwardRight));
                        }
                    }
                }
            }
            return legalMoves;
        }

        private static bool ForwardRightTileIsFree(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return TileIsFree(row + 1, col + 1, opposingColor, board)
                || TileCanBeJumped(row + 1, col + 1, opposingColor, board, MoveDirection.ForwardRight)
                ;
        }

        private static bool ForwardLeftTileIsFree(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return TileIsFree(row + 1, col - 1, opposingColor, board)
                || TileCanBeJumped(row + 1, col - 1, opposingColor, board, MoveDirection.ForwardLeft)
                ;
        }

        private static bool BackLeftTileIsFree(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return TileIsFree(row - 1, col + 1, opposingColor, board)
                || TileCanBeJumped(row - 1, col + 1, opposingColor, board, MoveDirection.BackwardLeft)
                ;
        }

        private static bool BackRightTileIsFree(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return TileIsFree(row - 1, col - 1, opposingColor, board)
                || TileCanBeJumped(row - 1, col - 1, opposingColor, board, MoveDirection.BackwardRight)
                ;
        }


        private static bool TileIsFree(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return TileIsInBounds(row, col) && board[row, col] == null;
        }

        private static bool TileCanBeJumped(int row, int col, PieceColor opposingColor, Piece[,] board, MoveDirection direction)
        {
            int rowMove = (direction == MoveDirection.ForwardRight || direction == MoveDirection.ForwardLeft) ? 1 : -1;
            int colMove = (direction == MoveDirection.ForwardRight || direction == MoveDirection.BackwardLeft) ? 1 : -1;
            return TileIsOpposingColor(row, col, opposingColor, board) 
                && TileIsFree(row + rowMove, col + colMove, opposingColor, board)
                ;
        }

        private static bool TileIsOpposingColor(int row, int col, PieceColor opposingColor, Piece[,] board)
        {
            return (board[row, col].Owner == opposingColor);
        }

        private static bool TileIsInBounds(int row, int col)
        {
            return row >= 0
                && row < BOARD_SIZE
                && col >= 0
                && col < BOARD_SIZE
                ;
        }

        public Piece[,] FlipBoard()
        {
            var reversedBoard = new Piece[BOARD_SIZE, BOARD_SIZE];

            for(int i = 0; i < BOARD_SIZE; i++)
            {
                int row = BOARD_SIZE - i - 1;
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    int col = BOARD_SIZE - j - 1;
                    Piece pieceToCopy = boardState[row, col];
                    if (pieceToCopy != null)
                        reversedBoard[i, j] = new Piece(i, j, pieceToCopy.Owner); // Can't use copy constructor here since row/col is different
                }
            }

            return reversedBoard;
        }
    }
}
