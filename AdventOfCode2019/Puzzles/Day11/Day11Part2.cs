using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day11
{
    public class Day11Part2
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

        public string SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day11Input.txt");
            return Solve(content);
        }

        public string Solve(string content)
        {
            var extractedProgram = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            var computer = new IntCodeComputer(extractedProgram);

            var shipHull = new Dictionary<(int x, int y), HullColor>();
            var position = (x:0, y:0);
            shipHull.Add((0,0), HullColor.White);
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
            } 
            while (computer.IsRunning);

            var maxX = shipHull.Keys.Select(t => t.x).Max();
            var maxY = shipHull.Keys.Select(t => Math.Abs(t.y)).Max() + 1;

            var outputHull = new char[maxY][];

            var stringBuilder = new StringBuilder();

            for (var y = 0; y < maxY; y++)
            {
                outputHull[y] = new char[maxX];

                for (var x = 0; x < maxX; x++)
                {
                   

                    if (shipHull.ContainsKey((x, -y)) && shipHull[(x, -y)] == HullColor.White)
                    {
                        outputHull[y][x] = '█';
                    }

                }

                stringBuilder.AppendLine(new string(outputHull[y]));
            }

            return stringBuilder.ToString();
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
