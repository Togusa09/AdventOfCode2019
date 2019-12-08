using System;
using System.Linq;
using AdventOfCode2019.Puzzles.Day5;
using AdventOfCode2019.Puzzles.Day7;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day7Test
    {
        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0", "43210")]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5, 23,23,1,24,23,23,4,23,99,0,0", "0,1,2,3,4", "54321")]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", "1,0,4,3,2", "65210")]
        public void TestPart2(string program, string input, string expectedOutput)
        {
            var splitInput = input.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var day7 = new Day7Part1();
            var val = day7.RunProgram(program, input);

            val.ToString().ShouldBe(expectedOutput);
        }
    }
}
