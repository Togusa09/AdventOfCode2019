using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day3
{
    public class Intersection
    {
        public int X { get; }
        public int Y { get; }
        public int Distance { get; }

        public Intersection(int x, int y, int distance)
        {
            X = x;
            Y = y;
            Distance = distance;
        }
    }

    public class Day3Part2
    {
        private const int GridSize = 20000;

        //private int[][][] _grid;
        private int[] _grid = new int[GridSize * GridSize * 2];


        public Day3Part2()
        {
            //_grid = new int[GridSize][][];
            //for (var index = 0; index < GridSize; index++)
            //{ 
            //    _grid[index] = new int[GridSize][];
            //    for (var index2 = 0; index2 < GridSize; index2++)
            //    {
            //        _grid[index][index2] = new int[3];
            //    }
            //}
        }

        void SetGrid(int x, int y, int line, int val)
        {
            var pos = (x * GridSize + y) * line;
            _grid[pos] = val;
        }

        int ReadGrid(int x, int y, int line)
        {
            var pos = (x * GridSize + y) * line;
            return _grid[pos];
        }

        public int GetResult()
        {
            var input = File.ReadAllText(@"InputFiles/Day03Input.txt");
            return Solve(input);
        }

        private int[] _totalDistance = new int[3];
        private List<Intersection>[] _intersectingJunctions = new [] { new List<Intersection>(),  new List<Intersection>(), new List<Intersection>() };

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

            var distances = new List<int>();

            for (var x = 0; x < GridSize; x++)
            for (var y = 0; y < GridSize; y++)
            {
                if (ReadGrid(x, y, 1) != 0 && ReadGrid(x, y, 2) != 0)
                {
                    distances.Add(ReadGrid(x, y, 1) + ReadGrid(x, y, 2));
                }
            }
            
            var ordered = distances.OrderBy(x => x).ToList();

            return ordered.First();
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

            _totalDistance[line]++;

            if (ReadGrid(x + offset, y + offset, line) == 0)
            {
                SetGrid(x + offset, y+offset, line, _totalDistance[line]);
                    //_grid[x + offset][y + offset][line] = _totalDistance[line];
            } 
        }
    }
}
