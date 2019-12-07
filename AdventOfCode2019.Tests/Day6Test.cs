using AdventOfCode2019.Puzzles.Day6;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day6Test
    {
        [Theory]
        [InlineData("COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L", 42)]
        public void Part1(string orbitData, int result)
        {
            var day6Part1 = new Day6Part1();
            day6Part1.Solve(orbitData).ShouldBe(result);
        }
    }
}
