using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day5
{
    public class TestIOSystem : IIoSystem
    {
        Stack<string> _input = new Stack<string>();
        List<string> _output = new List<string>();

        public TestIOSystem(string[] input)
        {
            foreach (var val in input.Reverse())
            {
                _input.Push(val);
            }
        }

        public string ReadIn()
        {
            return _input.Pop();
        }

        public void WriteOut(string value)
        {
            _output.Add(value);
        }

        public List<string> GetAllInput()
        {
            return _output;
        }
    }


    public class Day5Part1
    {
        private IIoSystem _ioSystem;
        public Day5Part1(IIoSystem ioSystem)
        {
            _ioSystem = ioSystem;
        }

        public string SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day5Input.txt");

            return RunProgram(content);
        }

        public string RunProgram(string input)
        {
            var extractedProgram = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            return string.Join(",", RunProgram(extractedProgram));
        }

        public int ReadValue(int[] program, int parameter, int parameterMode)
        {
            
            switch (parameterMode) 
            {
                case 0:
                    return program[parameter];
                case 1:
                    return parameter;
                default:
                    throw  new ArgumentException("Bad parameter mode", nameof(parameterMode));
            }
        }
        
        public int[] RunProgram(int[] program)
        {
            var parameterModes = new int[2];

            for (int i = 0; i < program.Length; i++)
            {
                var opcode = program[i] % 100;
                var parameterModeString = ((program[i] - opcode) / 100).ToString("00");
                parameterModes[0] = (int)char.GetNumericValue(parameterModeString[1]);
                parameterModes[1] = (int)char.GetNumericValue(parameterModeString[0]);
                //parameterModes[2] = (int)char.GetNumericValue(parameterModeString[1]);
                //parameterModes[3] = (int)char.GetNumericValue(parameterModeString[0]);

                switch (opcode)
                {
                    case 1:
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0])
                                               + ReadValue(program, secondIndex, parameterModes[1]);
                        i += 3;
                            break;
                    }
                    case 2:
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0]) *
                                               ReadValue(program, secondIndex, parameterModes[1]);
                        i += 3;
                        break;
                    }
                    case 3:
                    {
                        var resultIndex = program[i + 1];
                        program[resultIndex] = int.Parse(_ioSystem.ReadIn());
                        i += 1;
                        break;
                    }
                    case 4:
                    {
                        var resultIndex = program[i + 1];
                        var val = ReadValue(program, resultIndex, parameterModes[0]);
                        _ioSystem.WriteOut(val.ToString());
                        i += 1;
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
