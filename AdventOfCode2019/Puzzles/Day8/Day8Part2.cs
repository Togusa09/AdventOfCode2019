using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day8
{
    public class Day8Part2 
    {
        public string SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day8Input.txt");
            return Solve(25, 6, content.Trim());
        }

        public string Solve(int xSize, int ySize, string content)
        {
            var layerSize = xSize * ySize;
            var layerCount = content.Length / layerSize;

            var layers = new List<char[]>();
            for (var i = 0; i < layerCount; i++)
            {
                var layer = content.ToCharArray().Skip(i * layerSize).Take(layerSize).ToArray();
                layers.Add(layer);
            }

            var actualImage = new char[layerSize];

            for (var x = 0; x < xSize; x++)
            for (var y = 0; y < ySize; y++)
            {
                var pixels = layers.Select(p => p[x + y * xSize]).ToArray();
                var pixel = pixels.First(x => x != '2');
                actualImage[x + y * xSize] = pixel;
            }

            //actualImage.Select(x => new string(x)).J
            var imageLines = new List<string>();
            for (var i = 0; i < ySize; i++)
            {
                var layer = actualImage.Skip(i * xSize).Take(xSize).ToArray();
                imageLines.Add(new string(layer));
            }

            return string.Join("\n", imageLines);
        }
    }
}
