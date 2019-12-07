using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day6
{
    public class Day6Part1 : ISolvable<int>
    {
        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day6Input.txt");
            return Solve(content);
        }

        public int Solve(string content)
        {
            var data = content.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                {
                    var t = x.Split(")");
                    return (t[0], t[1]);
                })
                .GroupBy(x => x.Item1)
                .Select(x => (Parent: x.Key, Children: x.Select(x => x.Item2).ToArray()))
                .ToDictionary(x => x.Parent, x => x.Children);

            var result = Solve("COM", 0, data);
            
            return result;
        }

        private int Solve(string parentName, int depth, Dictionary<string, string[]> data)
        {
            if (!data.ContainsKey(parentName)) return  depth;

            var children = data[parentName];
            var result = depth + children.Sum(x => Solve(x, depth + 1, data));
            return result;
        }
    }
}
