using System.Runtime.InteropServices;
using AdventOfCode2019.Puzzles.Day10;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day10Test
    {
        [Theory]
        [InlineData(".#..#\n.....\n#####\n....#\n...##", 8)]
        [InlineData("......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####", 33)]
        public void Part1(string map, int expectedResult)
        {
            var day10Part1 = new Day10Part1();
            var t = day10Part1.Solve(map);
            t.ShouldBe(expectedResult);
        }
    }
}
