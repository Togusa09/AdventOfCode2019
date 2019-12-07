﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day5
{
    public class Day5Part2
    {
        private IIOSystem _ioSystem;
        public Day5Part2(IIOSystem ioSystem)
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

                switch (opcode)
                {
                    case 1: //Add
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0])
                                               + ReadValue(program, secondIndex, parameterModes[1]);
                        i += 3;
                            break;
                    }
                    case 2: // Mult
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];
                        var resultIndex = program[i + 3];
                        program[resultIndex] = ReadValue(program, firstIndex, parameterModes[0]) *
                                               ReadValue(program, secondIndex, parameterModes[1]);
                        i += 3;
                        break;
                    }
                    case 3: //Input
                    {
                        var resultIndex = program[i + 1];
                        program[resultIndex] = int.Parse(_ioSystem.ReadIn());
                        i += 1;
                        break;
                    }
                    case 4: //Output
                    {
                        var resultIndex = program[i + 1];
                        var val = ReadValue(program, resultIndex, parameterModes[0]);
                        _ioSystem.WriteOut(val.ToString());
                        i += 1;
                        break;
                    }
                    case 5: //jump-if-true
                        {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        if (value1 != 0) i = value2;

                        break;
                    }
                    case 6: //jump-if-false
                        {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        if (value1 == 0) i = value2;

                        break;
                    }
                    case 7: //less than
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        var resultIndex = program[i + 3];
                        if (value1 < value2) program[resultIndex] = 1;

                        break;
                    }
                    case 8: // Equals
                    {
                        var firstIndex = program[i + 1];
                        var secondIndex = program[i + 2];

                        var value1 = ReadValue(program, firstIndex, parameterModes[0]);
                        var value2 = ReadValue(program, secondIndex, parameterModes[1]);
                        var resultIndex = program[i + 3];
                        if (value1 == value2) program[resultIndex] = 1;

                        break;
                    }
                    case 99: // Terminate
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
