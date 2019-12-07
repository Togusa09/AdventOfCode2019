using System;
using System.Linq;
using AdventOfCode2019.Puzzles.Day5;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day5Test
    {
        [Theory]

        //[InlineData("1,0,0,0,99", "", "")]
        //[InlineData("2,3,0,3,99", "", "")]
        //[InlineData("2,4,4,5,99,0", "", "")]
        //[InlineData("1,1,1,4,99,5,6,0,99", "", "")]

        //[InlineData("3,0,4,0,99", "1234", "1234")]
        //[InlineData("1101,100,-1,4,0", "", "")]
        [InlineData("1101,10, 10, 3", "", "")]
        public void TestPart1(string program, string input, string expectedOutput)
        {
            var splitInput = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var testIO = new TestIOSystem(splitInput);
            
            var day1 = new Day5Part1(testIO);
            var val = day1.RunProgram(program);

            var result = testIO.GetAllInput().FirstOrDefault() ?? "";
            result.ShouldBe(expectedOutput);
        }

        [Theory]
        // Equal
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", "8", "1")]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", "7", "0")]
        [InlineData("3,3,1108,-1,8,3,4,3,99,-1,8", "8", "1")]
        [InlineData("3,3,1108,-1,8,3,4,3,99,-1,8", "7", "0")]
        // Less than
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", "7", "1")]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", "9", "0")]
        [InlineData("3,3,1107,-1,8,3,4,3,99", "7", "1")]
        [InlineData("3,3,1107,-1,8,3,4,3,99", "9", "0")]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", "1", "1")]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", "0", "0")]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", "1", "1")]
        [InlineData("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", "0", "0")]
        public void TestPart2(string program, string input, string expectedOutput)
        {
            var splitInput = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var testIO = new TestIOSystem(splitInput);

            var day1 = new Day5Part2(testIO);
            var val = day1.RunProgram(program);

            var result = testIO.GetAllInput().FirstOrDefault() ?? "";
            result.ShouldBe(expectedOutput);
        }
    }
}
