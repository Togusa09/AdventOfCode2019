using System;
using Xunit;
using AdventOfCode2019.Puzzles;
using AdventOfCode2019.Puzzles.Day1;
using Shouldly;

namespace AdventOfCode2019.Tests
{
    public class Day1Test
    {
        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 654)]
        [InlineData(100756, 33583)]
        public void Test1Part1(int mass, int fuel)
        {
            var day1 = new Day1Part1();
            day1.Calculate(mass).ShouldBe(fuel);
        }

        [Theory]
        [InlineData(12, 2)]
        [InlineData(14, 2)]
        [InlineData(1969, 966)]
        [InlineData(100756, 50346)]
        public void Test1Part2(int mass, int fuel)
        {
            var day1 = new Day1Part2();
            day1.Calculate(mass).ShouldBe(fuel);
        }
    }
}
