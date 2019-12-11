using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day11
{
    public class Day11Part1 
    {
        private enum HullColor
        {
            Black = 0,
            White = 1
        }

        private enum Direction
        {
            Up, Right, Down, Left, 
        }

        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day11Input.txt");
            return Solve(content);
        }

        public int Solve(string content)
        {
            var extractedProgram = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            var computer = new IntCodeComputer(extractedProgram);

            var shipHull = new Dictionary<(int, int), HullColor>();
            var position = (0, 0);
            var direction = Direction.Up;

            do
            {
                var hullState = shipHull.ContainsKey(position) ? shipHull[position] : HullColor.Black;

                var outputs = computer.RunProgram((long)hullState).ToArray();
                shipHull[position] = outputs.First() == 1 ? HullColor.White : HullColor.Black;
                direction = outputs.ElementAt(1) == 1 ? ++direction : --direction;
                if (direction > Direction.Left) direction = Direction.Up;
                if (direction < Direction.Up) direction = Direction.Left;

                position = Move(direction, position);
                ;
            } 
            while (computer.IsRunning);

            return 0;
        }

        private (int, int) Move(Direction dir, (int, int) position)
        {
            var (x, y) = position;
            switch (dir)
            {
                case Direction.Up:
                    return (x, y + 1);
                case Direction.Down:
                    return (x, y - 1);
                case Direction.Left:
                    return (x - 1, y);
                case Direction.Right:
                    return (x + 1, y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
    }
}
