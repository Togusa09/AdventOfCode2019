using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day5
{
    public class Day5Part2
    {
        private IIoSystem _ioSystem;
        public Day5Part2(IIoSystem ioSystem)
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
            var infiniteLoopCheck = 0;
            var running = true;
            var instructionPointer = 0;

            //for (int i = 0; i < program.Length; i++)
            while (running && infiniteLoopCheck < int.MaxValue)
            {
                infiniteLoopCheck++;

                var opcode = program[instructionPointer] % 100;
                var parameterModeString = ((program[instructionPointer] - opcode) / 100).ToString("00");
                parameterModes[0] = (int)char.GetNumericValue(parameterModeString[1]);
                parameterModes[1] = (int)char.GetNumericValue(parameterModeString[0]);
                
                switch (opcode)
                {
                    case 1: //Add
                    {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];
                        var resultIndex = program[instructionPointer + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0])
                                               + ReadValue(program, secondIndex, parameterModes[1]);
                        instructionPointer += 3;
                            break;
                    }
                    case 2: // Mult
                    {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];
                        var resultIndex = program[instructionPointer + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0]) *
                                               ReadValue(program, secondIndex, parameterModes[1]);
                        instructionPointer += 3;
                        break;
                    }
                    case 3: //Input
                    {
                        var resultIndex = program[instructionPointer + 1];
                        program[resultIndex] = int.Parse(_ioSystem.ReadIn());
                        instructionPointer += 1;
                        break;
                    }
                    case 4: //Output
                    {
                        var resultIndex = program[instructionPointer + 1];
                        var val = ReadValue(program, resultIndex, parameterModes[0]);
                        _ioSystem.WriteOut(val.ToString());
                        instructionPointer += 1;
                        break;
                    }
                    case 5: //jump-if-true
                        {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        if (value1 != 0)
                        {
                            instructionPointer = value2;
                            continue;
                        }
                        instructionPointer += 2;
                            break;
                    }
                    case 6: //jump-if-false
                        {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        if (value1 == 0)
                        {
                            instructionPointer = value2;
                            continue;
                        }
                        instructionPointer += 2;

                        break;
                    }
                    case 7: //less than
                    {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        var resultIndex = program[instructionPointer + 3];
                        program[resultIndex] = (value1 < value2) ? 1 : 0;
                        instructionPointer += 3;
                        break;
                    }
                    case 8: // Equals
                    {
                        var firstIndex = program[instructionPointer + 1];
                        var secondIndex = program[instructionPointer + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        var resultIndex = program[instructionPointer + 3];
                        program[resultIndex] = (value1 == value2) ? 1 : 0;
                        instructionPointer += 3;
                        break;
                    }
                    case 99: // Terminate
                        running = false;
                        continue;
                    default:
                        throw new Exception("Bad command");
                }

                instructionPointer++;

            }

            return program;
        }
    }
}
