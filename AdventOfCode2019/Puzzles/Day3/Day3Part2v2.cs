using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day3
{
    public class Day3Part2v2 
    {
        private const int GridSize = 20000;

        //private int[][][] _grid;

        private List<(int, int)> _path1 = new List<(int, int)>();
        private List<(int, int)> _path2 = new List<(int, int)>();

        public Day3Part2v2()
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

        public int GetResult()
        {
            var input = File.ReadAllText(@"InputFiles/Day3Input.txt");
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

            

            var merged = _path1.Intersect(_path2);
            var distances = merged.Select(x => _path1.IndexOf(x) + _path2.IndexOf(x) + 2).Min();


            return distances;
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

                
                if (line == 1) _path1.Add((x, y));
                if (line == 2) _path2.Add((x, y));
            }

            return (x, y);
        }

        //private void DrawPoint((int, int) point, int line)
        //{
        //    var offset = GridSize / 2;
        //    var charToUse = (char) line;
        //    var (x, y) = point;

        //    _totalDistance[line]++;

        //    if (ReadGrid(x + offset, y + offset, line) == 0)
        //    {
        //        SetGrid(x + offset, y+offset, line, _totalDistance[line]);
        //            //_grid[x + offset][y + offset][line] = _totalDistance[line];
        //    } 
        //}
    }
}
