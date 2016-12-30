using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class BoardMoveGenerator
    {

        private CheckerBoard Board { get; set; }
        private CheckerPiece Piece { get; set; }
        private int Row { get { return Piece.Row; } }
        private int Col { get { return Piece.Col; } }
        private PieceColor? _opposingColor;
        private PieceColor OpposingColor
        {
            get
            {
                if (_opposingColor == null)
                    _opposingColor = CheckerPiece.GetOppositeColor(Piece.Owner);
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


        public BoardMoveGenerator(CheckerBoard board, int row, int col)
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
                if (ForwardLeftTileInDirectionIsFree(Row, Col))
                {
                    _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.ForwardLeft }));
                }
                if (Piece.IsKing)
                {
                    if (BackLeftTileInDirectionIsFree(Row, Col))
                    {
                        _moves.Add(new Move(Piece, new List<MoveDirection> { MoveDirection.BackwardLeft }));
                    }
                    if (BackRightTileInDirectionIsFree(Row, Col))
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
            GenerateJumps(Row, Col, new HashSet<Tile>(), new List<MoveDirection>(), tmpMoves);
            int maxDirectionCount = 0;
            foreach (var move in tmpMoves)
                maxDirectionCount = Math.Max(maxDirectionCount, move.Direction.Count);
            _moves = tmpMoves.Where(m => m.Direction.Count == maxDirectionCount).ToList();
        }

        private void GenerateJumps(int row, int col, HashSet<Tile> capturedTiles, List<MoveDirection> directions, List<Move> moves)
        {
            if (TileCanBeJumpedInDirection(row, col, MoveDirection.ForwardLeft) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.ForwardLeft)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.ForwardLeft);
            if (TileCanBeJumpedInDirection(row, col, MoveDirection.ForwardRight) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.ForwardRight)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.ForwardRight);
            if (TileCanBeJumpedInDirection(row, col, MoveDirection.BackwardLeft) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.BackwardLeft)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.BackwardLeft);
            if (TileCanBeJumpedInDirection(row, col, MoveDirection.BackwardRight) && !capturedTiles.Contains(GetTileFromDirection(row, col, MoveDirection.BackwardRight)))
                GenerateJumpsForDirection(row, col, capturedTiles, directions, moves, MoveDirection.BackwardRight);
        }

        private void GenerateJumpsForDirection(int row, int col, HashSet<Tile> capturedTiles, List<MoveDirection> directions, List<Move> moves, MoveDirection direction)
        {
            var forwardLeftTile = GetTileFromDirection(row, col, direction);
            var tempCapturedTiles = new HashSet<Tile>(capturedTiles);
            tempCapturedTiles.Add(forwardLeftTile);
            var moveDirections = new List<MoveDirection>(directions);
            moveDirections.Add(direction);

            // Get the movement amount and then multiply by two since a jump moves
            // twice as far.
            int newRow = row + 2 * MoveUtil.GetRowMoveAmountByColor(Piece.Owner, direction);
            int newCol = col + 2 * MoveUtil.GetColMoveAmount(direction);

            if (PieceCanJump(newRow, newCol))
                GenerateJumps(newRow, newCol, tempCapturedTiles, moveDirections, moves);
            else
                moves.Add(new Move(Piece, moveDirections));
        }

        private Tile GetTileFromDirection(int row, int col, MoveDirection direction)
        {
            return Tile.FromRowCol(
                row + MoveUtil.GetRowMoveAmountByColor(Piece.Owner, MoveDirection.ForwardLeft), 
                col + MoveUtil.GetColMoveAmount(MoveDirection.ForwardLeft));
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
            return TileCanBeJumpedInDirection(row, col, MoveDirection.ForwardRight)
                || TileCanBeJumpedInDirection(row, col, MoveDirection.ForwardLeft)
                ;
        }

        private bool PieceCanJumpBack(int row, int col)
        {
            return TileCanBeJumpedInDirection(row, col, MoveDirection.BackwardRight)
                || TileCanBeJumpedInDirection(row, col, MoveDirection.BackwardLeft)
                ;
        }

        public bool TileCanBeJumpedInDirection(int row, int col, MoveDirection direction)
        {
            int jumpRow = row + MoveUtil.GetRowMoveAmountByColor(Piece.Owner, direction);
            int jumpCol = col + MoveUtil.GetColMoveAmount(direction);
            return CheckerBoard.TileIsInBounds(jumpRow, jumpCol)
                && TileInDirectionIsFree(jumpRow, jumpCol, direction)
                && Board.TileIsOpposingColor(jumpRow, jumpCol, OpposingColor)
                ;
        }

        public bool TileInDirectionIsFree(int row, int col, MoveDirection direction)
        {
            int newRow = row + MoveUtil.GetRowMoveAmountByColor(Piece.Owner, direction);
            int newCol = col + MoveUtil.GetColMoveAmount(direction);
            return CheckerBoard.TileIsInBounds(newRow, newCol)
                && Board.GetPiece(newRow, newCol) == null
                ;
        }

        public bool ForwardRightTileIsFree(int row, int col)
        {
            return TileInDirectionIsFree(row, col, MoveDirection.ForwardRight);
        }

        public bool ForwardLeftTileInDirectionIsFree(int row, int col)
        {
            return TileInDirectionIsFree(row, col, MoveDirection.ForwardLeft);
        }

        public bool BackLeftTileInDirectionIsFree(int row, int col)
        {
            return TileInDirectionIsFree(row, col, MoveDirection.BackwardLeft);
        }

        public bool BackRightTileInDirectionIsFree(int row, int col)
        {
            return TileInDirectionIsFree(row, col, MoveDirection.BackwardRight);
        }

        #endregion

    }

    class Tile : IEquatable<Tile>
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Tile() { }
        
        private Tile(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Tile FromRowCol(int row, int col)
        {
            return new Tile(row, col);
        }

        public override bool Equals(object obj)
        {
            Tile other = obj as Tile;
            return other != null
                && Equals(other)
                ;
        }

        public bool Equals(Tile other)
        {
            if (this == other)
                return true;

            return other != null
                && Row == other.Row
                && Col == other.Col
                ;
        }

        public override int GetHashCode()
        {
            return (Row * 23) ^ (Col * 31);
        }
    }

}
