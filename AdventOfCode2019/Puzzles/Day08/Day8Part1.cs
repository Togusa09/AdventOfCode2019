using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day8
{
    public class Day8Part1 
    {
        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day08Input.txt");
            return Solve(25, 6, content.Trim());
        }

        public int Solve(int xSize, int ySize, string content)
        {
            var layerSize = xSize * ySize;
            var layerCount = content.Length / layerSize;

            var layers = new List<char[]>();
            for (var i = 0; i < layerCount; i++)
            {
                var layer = content.ToCharArray().Skip(i * layerSize).Take(layerSize).ToArray();
                layers.Add(layer);
            }

            //for (var i = 0; i < content.Length; i += layerSize)
            //{
            //    layers.Add(content.Substring(i, layerSize));
            //}

            //var t = layers.Select(x => (Num: x.Count(y => y == '0'), Row: x));
            //var f = t.OrderBy(x => x.Num).First();
            //var g = f.Row.Count(x => x == '1') * f.Row.Count(x => x == '2');



            var rowWithFewest = layers.OrderBy(x => x.Count(y => y == '0')).First();
            return rowWithFewest.Count(x => x == '1') * rowWithFewest.Count(x => x == '2');

        }
    }
}
