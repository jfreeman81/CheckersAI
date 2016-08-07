using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class BoardMoveGenerator
    {

        private Board Board { get; set; }
        private Piece Piece { get; set; }
        private int Row { get { return Piece.Row; } }
        private int Col { get { return Piece.Col; } }
        private PieceColor? _opposingColor;
        private PieceColor OpposingColor
        {
            get
            {
                if (_opposingColor == null)
                    _opposingColor = Piece.GetOppositeColor(Piece.Owner);
                return _opposingColor.Value;
            }
        }

        private IList<Move> _moves;

        public IList<Move> Moves
        {
            get
            {
                if (_moves == null)
                    GenerateMoves();
                return _moves;
            }
        }


        public BoardMoveGenerator(Board board, int row, int col)
        {
            Board = board;
            if (Board == null || Board.GetPiece(row, col) == null)
                throw new ArgumentException("Error: Move generator must have both a valid board and piece to generate moves.");
            Piece = board.GetPiece(row, col);
        }

        private void GenerateMoves()
        {
            if (PieceCanJump(Row, Col))
            {
                GenerateJumps();
            }
            else
            {
                _moves = new List<Move>();
                if (ForwardRightTileIsFree(Row, Col))
                {
                    _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.ForwardRight }));
                }
                if (ForwardLeftTileIsFree(Row, Col))
                {
                    _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.ForwardLeft }));
                }
                if (Piece.IsKing)
                {
                    if (BackLeftTileIsFree(Row, Col))
                    {
                        _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.BackwardLeft }));
                    }
                    if (BackRightTileIsFree(Row, Col))
                    {
                        _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.BackwardRight }));
                    }
                }
            }
        }

        private void GenerateJumps()
        {
            _moves = new List<Move>();
            var tmpMoves = new List<Move>();
            GenerateJumps(Row, Col, new HashSet<Tuple<int, int>>(), new List<MoveDirection>(), tmpMoves);
            int maxDirectionCount = 0;
            foreach (var move in tmpMoves)
                maxDirectionCount = Math.Max(maxDirectionCount, move.Direction.Count);
            _moves = tmpMoves.Where(m => m.Direction.Count == maxDirectionCount).ToList();
        }

        private void GenerateJumps(int row, int col, HashSet<Tuple<int, int>> capturedTiles, List<MoveDirection> directions, List<Move> moves)
        {
            if (TileCanBeJumped(row, col, MoveDirection.ForwardLeft) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.ForwardLeft)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.ForwardLeft);
            if (TileCanBeJumped(row, col, MoveDirection.ForwardRight) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.ForwardRight)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.ForwardRight);
            if (TileCanBeJumped(row, col, MoveDirection.BackwardLeft) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.BackwardLeft)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.BackwardLeft);
            if (TileCanBeJumped(row, col, MoveDirection.BackwardRight) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.BackwardRight)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.BackwardRight);
        }

        private void GenerateJumpsForDirection(int row, int col, HashSet<Tuple<int, int>> capturedTiles, List<MoveDirection> directions, List<Move> moves, MoveDirection direction)
        {
            var forwardLeftTile = GetTileFromDirection(row, col, direction);
            var tempCapturedTiles = new HashSet<Tuple<int, int>>(capturedTiles);
            tempCapturedTiles.Add(forwardLeftTile);
            var moveDirections = new List<MoveDirection>(directions);
            moveDirections.Add(direction);

            // multiply by 2 since jumping moves 2 rows and two columns
            int newRow = row + 2 * Board.GetRowMoveAmount(direction); 
            int newCol = col + 2 * Board.GetColMoveAmount(direction);

            if (PieceCanJump(newRow, newCol))
                GenerateJumps(newRow, newCol, tempCapturedTiles, moveDirections, moves);
            else
                moves.Add(new Move(Piece, moveDirections));
        }

        private Tuple<int, int> GetTileFromDirection(int row, int col, MoveDirection direction)
        {
            return new Tuple<int, int>(
                row + Board.GetRowMoveAmount(MoveDirection.ForwardLeft), 
                col + Board.GetColMoveAmount(MoveDirection.ForwardLeft));
        }

        #region Helper Methods

        private bool PieceCanJump(int row, int col)
        {
            return PieceCanJumpForward(row, col)
                || (Piece.IsKing && PieceCanJumpBack(row, col))
                ;
        }

        private bool PieceCanJumpForward(int row, int col)
        {
            return TileCanBeJumped(row, col, MoveDirection.ForwardRight)
                || TileCanBeJumped(row, col, MoveDirection.ForwardLeft)
                ;
        }

        private bool PieceCanJumpBack(int row, int col)
        {
            return TileCanBeJumped(row, col, MoveDirection.BackwardRight)
                || TileCanBeJumped(row, col, MoveDirection.BackwardLeft)
                ;
        }

        public bool TileCanBeJumped(int row, int col, MoveDirection direction)
        {
            int jumpRow = row + Board.GetRowMoveAmount(direction);
            int jumpCol = col + Board.GetColMoveAmount(direction);
            return Board.TileIsFree(jumpRow, jumpCol, direction)
                && Board.TileIsOpposingColor(jumpRow, jumpCol, OpposingColor)
                ;
        }

        public bool ForwardRightTileIsFree(int row, int col)
        {
            return Board.TileIsFree(row, col, MoveDirection.ForwardRight);
        }

        public bool ForwardLeftTileIsFree(int row, int col)
        {
            return Board.TileIsFree(row, col, MoveDirection.ForwardLeft);
        }

        public bool BackLeftTileIsFree(int row, int col)
        {
            return Board.TileIsFree(row, col, MoveDirection.BackwardLeft);
        }

        public bool BackRightTileIsFree(int row, int col)
        {
            return Board.TileIsFree(row, col, MoveDirection.BackwardRight);
        }

        #endregion

    }
}
