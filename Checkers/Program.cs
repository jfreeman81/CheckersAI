using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            Game thisGame = new Game();
            thisGame.Test();
            thisGame.PrintBoard();
            Console.ReadLine();
        }
    }

    public enum Players
    {
        None,
        Black,
        White
    }

    public enum Directions
    {
        ForwardRight,
        ForwardLeft,
        BackwardRight,
        BackwardLeft
    }

    class Piece
    {
        public Players Owner;
        public bool IsKing = false;
        public int Row;
        public int Col;

        public Piece(int row, int col, Players owner = Players.None)
        {
            this.Row = row;
            this.Col = col;
            this.Owner = owner;
        }
    }

    class Move
    {
        Piece Piece;
        Directions Direction;

        public Move(Piece piece, Directions direction)
        {
            this.Piece = piece;
            this.Direction = direction;
        }
    }

    class Board
    {
        int boardSize = 8;
        int piecesDepth = 2;
        List<List<Piece>> boardState = new List<List<Piece>>();

        public Board()
        {
            List<Piece> row = new List<Piece>();
            bool placePiece = true;

            // add black's pieces
            for (int i = 0; i < piecesDepth; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (placePiece) row.Add(new Piece(i, j, Players.Black));
                    else row.Add(null);

                    placePiece = !placePiece;
                }
                boardState.Add(row);
                row = new List<Piece>();
                if (boardSize % 2 == 0) placePiece = !placePiece;
            }

            // add blank tiles
            for (int j = 0; j < boardSize; j++)
            {
                row.Add(null);
            }
            for (int i = 0; i < boardSize - (2 * piecesDepth); i++)
            {
                boardState.Add(new List<Piece>(row));
            }
            row = new List<Piece>();

            // add white's pieces
            List<List<Piece>> whiteSide = new List<List<Piece>>();
            placePiece = true;
            for (int i = 0; i < piecesDepth; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (placePiece) row.Add(new Piece(i, j, Players.White));
                    else row.Add(null);

                    placePiece = !placePiece;
                }
                whiteSide.Insert(0, row);
                row = new List<Piece>();
                if (boardSize % 2 == 0) placePiece = !placePiece;
            }
            boardState.AddRange(whiteSide);
        }

        public void Print()
        {
            foreach (List<Piece> row in boardState)
            {
                foreach (Piece tile in row)
                {
                    string pieceRep = "";
                    if (tile == null) pieceRep = "-";
                    else if (tile.Owner == Players.Black) pieceRep = "B";
                    else if (tile.Owner == Players.White) pieceRep = "W";

                    Console.Write("{0,3}", pieceRep);
                }
                Console.WriteLine();
            }
        }

        public bool MakeMove(Move move)
        {
            return false;
        }

        public List<Move> GetLegalMoves(Players player)
        {
            Players otherPlayer = player == Players.White ? Players.Black : Players.White;
            List<List<Piece>> checkBoard = player == Players.White ? FlipBoard() : boardState;
            AddBuffer(checkBoard);
            List<Move> legalMoves = new List<Move>();
            foreach (List<Piece> row in checkBoard)
            {
                foreach (Piece piece in row.Where(t => t.Owner == player))
                {
                    if (checkBoard[piece.Row + 1][piece.Col + 1] == null
                        || checkBoard[piece.Row + 1][piece.Col + 1].Owner == otherPlayer
                        && checkBoard[piece.Row + 1][piece.Col + 1] == null)
                    {
                        legalMoves.Add(new Move(piece, Directions.ForwardLeft));
                    }
                    if (checkBoard[piece.Row + 1][piece.Col - 1] == null
                        || checkBoard[piece.Row + 1][piece.Col - 1].Owner == otherPlayer
                        && checkBoard[piece.Row + 2][piece.Col - 2] == null)
                    {
                        legalMoves.Add(new Move(piece, Directions.ForwardRight));
                    }
                    if (piece.IsKing)
                    {
                        if (checkBoard[piece.Row - 1][piece.Col + 1] == null
                            || checkBoard[piece.Row - 1][piece.Col + 1].Owner == otherPlayer
                            && checkBoard[piece.Row - 1][piece.Col + 1] == null)
                        {
                            legalMoves.Add(new Move(piece, Directions.BackwardLeft));
                        }
                        if (checkBoard[piece.Row - 1][piece.Col - 1] == null
                            || checkBoard[piece.Row - 1][piece.Col - 1].Owner == otherPlayer
                            && checkBoard[piece.Row - 2][piece.Col - 2] == null)
                        {
                            legalMoves.Add(new Move(piece, Directions.BackwardRight));
                        }
                    }
                }
            }
            return legalMoves;
        }

        public List<List<Piece>> FlipBoard()
        {
            List<List<Piece>> tempBoard = new List<List<Piece>>(boardState);
            List<List<Piece>> boardStateReverse = new List<List<Piece>>();

            while (tempBoard.Count != 0)
            {
                List<Piece> lastRow = tempBoard.Last();
                foreach (Piece piece in lastRow)
                {
                    int newRow = piece.Row - boardSize - 1;
                    piece.Row = newRow < 0? newRow * -1 : newRow;
                }
                boardStateReverse.Add(lastRow);
                tempBoard.Remove(lastRow);
            }

            // fix piece locations

            return boardStateReverse;
        }

        public void AddBuffer(List<List<Piece>> inputBoard)
        {
            List<Piece> row = new List<Piece>();
            Piece bufferPiece = new Piece(-1, -1);

            

            foreach (List<Piece> thisRow in inputBoard)
            {
                thisRow.Insert(0, bufferPiece);
                thisRow.Add(bufferPiece);
            }

            for (int i = 0; i < boardSize + 2; i++)
            {
                row.Add(bufferPiece);
            }
            inputBoard.Insert(0, row);
            inputBoard.Add(row);
        }
    }

    class Game
    {
        Board Board = new Board();

        public void PrintBoard()
        {
            Board.Print();
        }

        public void Test()
        {
            Board.GetLegalMoves(Players.Black);
        }
    }
}
