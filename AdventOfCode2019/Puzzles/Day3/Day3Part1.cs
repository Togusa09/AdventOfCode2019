using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day3
{
    

    public class Day3Part1
    {
        private const int GridSize = 50000;

        private char[][] _grid;
        

        public Day3Part1()
        {
            _grid = new char[GridSize][];
            for (var index = 0; index < _grid.Length; index++)
            { 
                _grid[index] = new char[GridSize];
            }
        }

        public int GetResult()
        {
            var input = File.ReadAllText(@"InputFiles/Day3Input.txt");
            return Solve(input);
        }


        public int Solve(string input)
        {
            var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var path1 = lines[0].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var path2 = lines[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var pos = (0, 0);
            foreach (var step in path1)
            {
                pos = DrawLine(pos, step, 1);
            }

            pos = (0, 0);
            foreach (var step in path2)
            {
                pos = DrawLine(pos, step, 2);
            }

            var junctions = new List<(int, int)>();
            var distances = new List<int>();
            for(var x = 0; x < GridSize; x++)
            for (var y = 0; y < GridSize; y++)
            {
                if (_grid[x][y] == '+')
                {
                    junctions.Add((x, y));
                    distances.Add(x + y - GridSize);
                }
            }

            return distances.Select(Math.Abs).Min();
        }

        private (int, int) DrawLine((int, int) position, string command, int line)
        {
            var (x, y) = position;
            var dir = command[0];
            var distance = int.Parse(command.Substring(1));

            for (var i = 0; i < distance; i++)
            {
                switch (dir)
                {
                    case 'U':
                        y++;
                        break;
                    case 'D':
                        y--;
                        break;
                    case 'L':
                        x--;
                        break;
                    case 'R':
                        x++;
                        break;
                }
                DrawPoint((x, y), line);
            }

            return (x, y);
        }

        private void DrawPoint((int, int) point, int line)
        {
            var offset = GridSize / 2;
            var charToUse = (char) line;
            var (x, y) = point;
            if (_grid[x + offset][y + offset] == '\0')
            {
                _grid[x + offset][y + offset] = charToUse;
            } 
            else if (_grid[x + offset][y + offset] != charToUse)
            {
                _grid[x + offset][y + offset] = '+';
            }
        }
    }
}
