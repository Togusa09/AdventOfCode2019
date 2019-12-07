using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day4
{
    class Day4Part1 : ISolvable<int>
    {



        public Day4Part1()
        {
            
        }


        public int SolveForFile()
        {
            return Solve("193651-649729");
        }

        public int Solve(string content)
        {
            var vals = content.Split("-");
            var min = int.Parse(vals[0]);
            var max = int.Parse(vals[1]);

            var rules = new Func<int, bool>[]
            {
                (x) => x.ToString().Length == 6,
                (x) => x > min && x < max,
                (x) =>
                {
                    var t = x.ToString();
                    var pairs = new List<string>();
                    for (var i = 1; i < 6; i++)
                    {
                        pairs.Add("" + t[i - 1] + t[i]);
                    }

                    return pairs.Any(p => p[0] == p[1]);
                },
                (x) =>{
                    var t = x.ToString();
                    var pairs = new List<string>();
                    for (var i = 1; i < 6; i++)
                    {
                        
                        if (t[i - 1] > t[i]) return false;
                    }

                    return true;
                }
            };

            var validNumbers = new List<int>();
            for (var x = min; x <= max; x++)
            {
                if(rules.All(r => r(x))) validNumbers.Add(x);
            }

            return validNumbers.Count();
        }
    }
}
