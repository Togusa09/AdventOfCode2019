using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day10
{
    public class Day10Part1 : ISolvable<int>
    {
        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day10Input.txt");
            return Solve(content);
        }

        public int Solve(string content)
        {
            //(28, 22)
            var map = content.Split("\n")
                .ToArray();

            int mostVisible = 0;
            var coords = (0, 0);

            for(var x = 0; x < map.Length; x++)
            for(var y = 0; y < map[x].Length; y++)
            {
                if (map[y][x] == '.') continue;

                var visible = CalcVisible(map, x, y);
                if (visible > mostVisible)
                {
                    mostVisible = visible;
                    coords = (x, y);
                }
            }

            return mostVisible;
        }

        private int CalcVisible(string[] map, int xPos, int yPos)
        {
            //var angles = new double[map.Length * map[0].Length];
            var angles = new List<double>();

            var Rad2Deg = 180.0 / Math.PI;
            for (var x = 0; x < map.Length; x++)
            for (var y = 0; y < map[x].Length; y++)
            {
                if (map[y][x] == '.') continue;
                if (x == xPos && y == yPos) continue;

              
                    //var angle = Math.Atan2(yPos - y, xPos - x) * Rad2Deg;
                //var angle = (double)(yPos - y) / (double)(xPos - x);
                var angle = CalculateAngle(xPos, yPos, x, y);
                angles.Add(angle);
                //angles[x + y * map.Length] = Math.Atan2(yPos - y, xPos - x) * Rad2Deg;
            }

            var angleCount = angles.Distinct().Count();
            return angleCount;
        }

        private double CalculateAngle(int x1, int y1, int x2, int y2)
        {
            var deltaX = Math.Pow((x1 - x2), 2);
            var deltaY = Math.Pow((y1 - y2), 2);

            var radian = Math.Atan2((y1 - y2), (x1 - x2));
            var angle = (radian * (180 / Math.PI) + 360) % 360;

            return angle;
        }

    }
}
