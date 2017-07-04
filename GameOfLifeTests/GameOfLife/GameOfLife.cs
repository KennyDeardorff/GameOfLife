using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Game
    {
        public List<Cell> LivingCells = new List<Cell>();

        public Game(string[] seed)
        {
            LivingCells.AddRange(StringSeedParser.ParseSeed(seed));
        }

        public void Tick()
        {
            var dead = new List<Cell>();
            var newborns = new List<Cell>();

            for (var cellIndex = LivingCells.Count - 1; cellIndex >= 0; cellIndex--)
            {
                var livingCell = LivingCells[cellIndex];

                var livingNeighbors = LivingCells
                    .Where(cell => livingCell.Neighbors.Contains(cell))
                    .ToList();

                var deadNeighbors = livingCell.Neighbors
                    .Except(livingNeighbors)
                    .ToList();

                if (livingNeighbors.Count < 2 || 3 < livingNeighbors.Count)
                    dead.Add(livingCell);

                foreach (var deadNeighbor in deadNeighbors)
                {
                    if (newborns.Contains(deadNeighbor))
                        continue;

                    if (LivingCells.Count(cell => deadNeighbor.Neighbors.Contains(cell)) == 3)
                        newborns.Add(deadNeighbor);
                }
            }

            LivingCells.RemoveAll(tuple => dead.Contains(tuple));
            LivingCells.AddRange(newborns);
        }

        public override string ToString()
        {
            return StringGameDisplay.PrintToString(this);
        }
    }

    public class Cell
    {
        private readonly Lazy<List<Cell>> _neighbors;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            _neighbors = new Lazy<List<Cell>>(() => GetNeighbors().ToList());
        }

        public int X { get; }
        public int Y { get; }

        public List<Cell> Neighbors => _neighbors.Value;

        private IEnumerable<Cell> GetNeighbors()
        {
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            for (var yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0)
                    continue;

                yield return new Cell(X + xOffset, Y + yOffset);
            }
        }

        #region Equality Members

        protected bool Equals(Cell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        #endregion
    }

    public class StringSeedParser
    {
        public static List<Cell> ParseSeed(params string[] rows)
        {
            var livingCells = new List<Cell>();

            for (var y = 0; y < rows.Length; y++)
            for (var x = 0; x < rows[y].Length; x++)
                if (rows[y][x] != ' ')
                    livingCells.Add(new Cell(x, y));

            return livingCells;
        }
    }

    public class StringGameDisplay
    {
        public static string PrintToString(Game game)
        {
            if (game.LivingCells.Any() == false)
                return string.Empty;

            var xMin = game.LivingCells.Min(cell => cell.X);
            var xMax = game.LivingCells.Max(cell => cell.X);

            var yMin = game.LivingCells.Min(cell => cell.Y);
            var yMax = game.LivingCells.Max(cell => cell.Y);

            var rows = new bool[yMax - yMin + 1][];
            for (var i = 0; i < rows.Length; i++)
                rows[i] = new bool[xMax - xMin + 1];

            foreach (var livingCell in game.LivingCells)
                rows[livingCell.Y - yMin][livingCell.X - xMin] = true;

            var output = new StringBuilder();

            for (var y = 0; y < rows.Length; y++)
            {
                for (var x = 0; x < rows[y].Length; x++)
                    output.Append(rows[y][x] ? 'X' : ' ');

                if (y < rows.Length - 1)
                    output.Append(Environment.NewLine);
            }

            return output.ToString();
        }
    }
}