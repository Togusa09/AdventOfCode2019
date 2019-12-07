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
    }
}
