using System;
using AdventOfCode2019.Puzzles.Day5;

namespace AdventOfCode2019.Puzzles
{
    public class ActualIOSystem : IIoSystem
    {
        public string ReadIn()
        {
            return Console.ReadLine();
        }

        public void WriteOut(string value)
        {
            Console.WriteLine(value);
        }
    }
}