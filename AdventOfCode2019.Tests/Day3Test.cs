﻿using AdventOfCode2019.Puzzles;
using AdventOfCode2019.Puzzles.Day3;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day3Test
    {
        [Theory]
        [InlineData("R8,U5,L5,D3\nU7,R6,D4,L4", 6)]
        //[InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void TestPart1(string path, int result)
        {
            var day1 = new Day3Part1();
            var val = day1.Solve(path);
            val.ShouldBe(result);
        }

        [Theory]
        [InlineData("R8,U5,L5,D3\nU7,R6,D4,L4", 30)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void TestPart2(string path, int result)
        {
            var day3 = new Day3Part2();
            var val = day3.Solve(path);
            val.ShouldBe(result);
        }


        [Theory]
        [InlineData("R8,U5,L5,D3\nU7,R6,D4,L4", 30)]
        [InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void TestPart2V2(string path, int result)
        {
            var day3 = new Day3Part2v2();
            var val = day3.Solve(path);
            val.ShouldBe(result);
        }
    }
}
