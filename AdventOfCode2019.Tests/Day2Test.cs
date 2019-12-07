using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019.Puzzles;
using AdventOfCode2019.Puzzles.Day2;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day2Test
    {
        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void TestAddition(string input, string expectedResult)
        {
            var day2Part1 = new Day2Part1();
            var result = day2Part1.RunProgram(input);
            result.ShouldBe(expectedResult);
        }
    }
}
