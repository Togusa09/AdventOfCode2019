using AdventOfCode2019.Puzzles.Day8;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day8Test
    {
        [Theory]
        [InlineData(3, 2, "123456789012", 1)]
        public void Part1(int xSize, int ySize, string data, int expectedResult)
        {
            var day8Part1 = new Day8Part1();
            day8Part1.Solve(xSize, ySize, data).ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(2, 2, "0222112222120000", "01\n10")]
        public void Part2(int xSize, int ySize, string data, string finalImage)
        {
            var day8Part2 = new Day8Part2();
            day8Part2.Solve(xSize, ySize, data).ShouldBe(finalImage);
        }
    }
}
