using System;
using AdventOfCode2019.Puzzles;
using AdventOfCode2019.Puzzles.Day3;
using AdventOfCode2019.Puzzles.Day4;
using AdventOfCode2019.Puzzles.Day5;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            //var day1Result = new FileSummer<Day1Part1>().SumAll();
            //var day2Result = new FileSummer<Day1Part2>().SumAll();

            //var day2 = new Day2Part1().GetResult();
            //var day2Part2 = new Day2Part2().GetResult();

            //var day3Part1 = new Day3Part1().GetResult();
            //var day3Part2 = new Day3Part2v2().GetResult();

            //var day4Part1 = new Day4Part1().SolveForFile();
            //var day4Part2 = new Day4Part2().SolveForFile();

            var day5Part1 = new Day5Part1(new ActualIOSystem()).SolveForFile();

            Console.WriteLine("Hello World!");
        }
    }
}
