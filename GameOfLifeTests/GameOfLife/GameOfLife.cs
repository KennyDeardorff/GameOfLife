using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Game
    {
        public List<Tuple<int, int>> LivingCells = new List<Tuple<int, int>>();

        public Game(string[] seed)
        {
            LivingCells.AddRange(StringSeedParser.ParseSeed(seed));
        }

        public void Tick()
        {
            if (LivingCells.Count <= 2)
                LivingCells.Clear();
        }

        public override string ToString()
        {
            return StringGameDisplay.PrintToString(this);
        }
    }

    public class StringSeedParser
    {
        public static List<Tuple<int, int>> ParseSeed(params string[] rows)
        {
            var livingCells = new List<Tuple<int, int>>();

            for (var y = 0; y < rows.Length; y++)
            for (var x = 0; x < rows[y].Length; x++)
                if (rows[y][x] != ' ')
                    livingCells.Add(new Tuple<int, int>(x, y));

            return livingCells;
        }
    }

    public class StringGameDisplay
    {
        public static string PrintToString(Game game)
        {
            if (game.LivingCells.Any() == false)
                return string.Empty;

            var xMin = game.LivingCells.Min(tuple => tuple.Item1);
            var xMax = game.LivingCells.Max(tuple => tuple.Item1);

            var yMin = game.LivingCells.Min(tuple => tuple.Item2);
            var yMax = game.LivingCells.Max(tuple => tuple.Item2);

            var rows = new bool[yMax - yMin + 1][];
            for (var i = 0; i < rows.Length; i++)
                rows[i] = new bool[xMax - xMin + 1];

            foreach (var livingCell in game.LivingCells)
                rows[livingCell.Item2 - yMin][livingCell.Item1 - xMin] = true;

            var output = new StringBuilder();

            for (var y = 0; y < rows.Length; y++)
            {
                for (var x = 0; x < rows[y].Length; x++)
                    output.Append(rows[y][x] ? 'X' : ' ');

                output.Append(Environment.NewLine);
            }

            output.Remove(output.Length - 2, 2);

            return output.ToString();
        }
    }
}