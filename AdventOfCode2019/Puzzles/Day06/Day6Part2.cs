using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day6
{
    public class Day6Part2 : ISolvable<int>
    {
        public int SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day06Input.txt");
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

            //var result = Solve("COM", 0, data);
            var san = GetPathFromRoot("COM", "SAN", data).ToList();
            var you = GetPathFromRoot("COM", "YOU", data).ToList();

            var path = san.Where(x => !you.Contains(x)).Union(you.Where(x => !san.Contains(x))).ToList();

            return path.Count - 2;
        }

        private IEnumerable<string> GetPathFromRoot(string parentName, string destination, Dictionary<string, string[]> data)
        {
            if (!data.ContainsKey(parentName)) return new string[0];

            var children = data[parentName];
            if (children.Contains(destination)) return new [] {destination };

            foreach (var child in children)
            {
                var result = GetPathFromRoot(child, destination, data).ToList();
                if (result.Any())
                {
                    return new[] {child}.Union(result);
                }
            }

            return new string[0];
        }

        //private int Solve(string parentName, int depth, Dictionary<string, string[]> data)
        //{
        //    if (!data.ContainsKey(parentName)) return  depth;

        //    var children = data[parentName];
        //    var result = depth + children.Sum(x => Solve(x, depth + 1, data));
        //    return result;
        //}
    }
}
