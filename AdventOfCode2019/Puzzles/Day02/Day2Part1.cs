using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day2
{

    public class Day2Part1
    {
        public int GetResult()
        {
            var content = File.ReadAllText("InputFiles\\Day02Input.txt");

            var extractedProgram = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();


            return RunProgram(extractedProgram)[0];
        }

        public string RunProgram(string input)
        {
            var extractedProgram = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            return string.Join(",", RunProgram(extractedProgram));
        }

        
        public int[] RunProgram(int[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                switch (program[i])
                {
                    case 1:
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = program[firstIndex] + program[secondIndex];
                        i += 3;
                            break;
                    }
                    case 2:
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = program[firstIndex] * program[secondIndex];
                        i += 3;
                        break;
                    }
                    case 99:
                        i = program.Length - 1;
                        break;
                    default:
                        throw new Exception("Bad command");
                }
            }

            return program;
        }
    }
}
