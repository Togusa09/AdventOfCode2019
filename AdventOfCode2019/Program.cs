using System;
using AdventOfCode2019.Puzzles;
using AdventOfCode2019.Puzzles.Day2;
using AdventOfCode2019.Puzzles.Day3;
using AdventOfCode2019.Puzzles.Day4;
using AdventOfCode2019.Puzzles.Day5;
using AdventOfCode2019.Puzzles.Day6;
using AdventOfCode2019.Puzzles.Day7;
using AdventOfCode2019.Puzzles.Day8;
using AdventOfCode2019.Puzzles.Day9;

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

            //var day5Part1 = new Day5Part1(new ActualIOSystem()).SolveForFile();
            //var day5Part2 = new Day5Part2(new ActualIOSystem()).SolveForFile();

            //var day6Part1 = new Day6Part1().SolveForFile();
            //var day6Part2 = new Day6Part2().SolveForFile();

            //var day7Part1 = new Day7Part1().SolveForFile();
            //var day7Part1 = new Day7Part2().SolveForFile();

            //var day8Part1 = new Day8Part1().SolveForFile();
            //var day8Part2 = new Day8Part2().SolveForFile().Replace('1', '█').Replace('0', ' ');

            var day9Part1 = new Day9Part1().SolveForFile();
            Console.WriteLine(day9Part1);

            Console.WriteLine("Hello World!");
        }
    }
}
