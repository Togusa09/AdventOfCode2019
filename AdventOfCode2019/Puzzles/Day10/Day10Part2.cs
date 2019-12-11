using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day10
{
    public class Day10Part2 : ISolvable<int>
    {
        class Asteroid
        {
            public int X { get; }
            public int Y { get; }
            public double Angle { get; }
            public double Distance { get; }

            public Asteroid(int x, int y, double angle, double distance)
            {
                X = x;
                Y = y;
                Angle = angle;
                Distance = distance;
            }
        }

        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day10Input.txt");
            return Solve(content);
        }

        public int Solve(string content)
        {
            var map = content.Split("\n")
                .ToArray();

            var stationPos = (x: 22, y: 28);

            //var positions = new Dictionary<(int, int), double>();
            //var positions = new Dictionary<double, (int, int)>();
            var asteroids = new List<Asteroid>();

            for (var x = 0; x < map.Length; x++)
            for (var y = 0; y < map[x].Length; y++)
            {
                if (stationPos.x == x && stationPos.y == y) continue;
                if (map[y][x] != '#') continue;

                var angle = CalculateAngle(stationPos.x, stationPos.y, x, y);

                var distance = Math.Sqrt(Math.Pow(stationPos.x -x , 2) + Math.Pow(stationPos.y - y, 2));
                var a = new Asteroid(x, y, angle, Math.Abs(distance));
                asteroids.Add(a);
            }

            var destroyedAsteroids = new List<Asteroid>();

            var grouped = asteroids.GroupBy(x => x.Angle).OrderBy(x => x.Key);
            while (destroyedAsteroids.Count() < asteroids.Count())
            {
                foreach (var angle in grouped)
                {
                    var lineOfSight = angle.Where(x => !destroyedAsteroids.Contains(x)).ToList();
                    if (!lineOfSight.Any()) continue;

                    var closest = lineOfSight.OrderBy(x => x.Distance).FirstOrDefault();

                    destroyedAsteroids.Add(closest);
                }
            }

            var target = destroyedAsteroids.ElementAt(199);

            return target.X * 100 + target.Y;
        }



        private int CalcVisible(char[][] map, int xPos, int yPos)
        {
            

            var angles = new List<double>();

            var Rad2Deg = 180.0 / Math.PI;
            for (var x = 0; x < map.Length; x++)
            for (var y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] == '.') continue;
                if (x == xPos && y == yPos) continue;

                var angle = CalculateAngle(xPos, yPos, x, y);
                angles.Add(angle);
            }

            var angleCount = angles.Distinct().Count();
            return angleCount;
        }

        private double CalculateAngle(int x1, int y1, int x2, int y2)
        {
            var deltaX = Math.Pow((x1 - x2), 2);
            var deltaY = Math.Pow((y1 - y2), 2);

            var radian = Math.Atan2((y1 - y2), (x1 - x2));
            var angle = (radian * (180 / Math.PI) + 270) % 360;

            return angle;
        }
    }
}
