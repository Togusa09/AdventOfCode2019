using AdventOfCode2019.Puzzles.Day9;
using Shouldly;
using Xunit;

namespace AdventOfCode2019.Tests
{
    public class Day9Test
    {
        [Theory]
        [InlineData("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99")]
        [InlineData("1102,34915192,34915192,7,4,7,99,0", "1219070632396864")]
        [InlineData("104,1125899906842624,99", "1125899906842624")]
        public void Part1(string program, string expectedResult)
        {
            var day9Part1 = new Day9Part1();
            var result = day9Part1.RunProgram(program);
            result.ShouldBe(expectedResult);
        }
    }
}
