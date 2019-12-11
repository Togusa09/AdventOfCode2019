using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day9
{
    public class Day9Part2
    {
        public string SolveForFile()
        {
            var content = File.ReadAllText("InputFiles\\Day09Input.txt");
            var result = RunProgram(content, "2");

            return result;
        }

        public string RunProgram(string program, string input = "")
        {
            var extractedProgram = program.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            var extractedInputs = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();

            var computer = new IntCodeComputer(extractedProgram);
            var outputs = computer.RunProgram(extractedInputs);

            return string.Join(",", outputs);
        }

        class IntCodeComputer
        {
            public bool IsRunning { get; private set; }
            private long[] _program;
            private long _instructionPointer = 0;
            private long _relativeBase = 0;

            public IntCodeComputer(long[] program)
            {
                _program = new long[program.Length];
                program.CopyTo(_program, 0);
            }

            private long ReadValue(long parameter, long parameterMode)
            {
                switch (parameterMode)
                {
                    case 0:
                        return ReadIndex(parameter);
                    case 1:
                        return parameter;
                    case 2:
                        return ReadIndex(_relativeBase + parameter);
                    default:
                        throw new ArgumentException("Bad parameter mode", nameof(parameterMode));
                }
            }

            private void WriteIndex(long index, long value)
            {
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "index out of range");
                ExtendMemoryIfNeeded(index);
                _program[index] = value;
            }

            private long ReadIndex(long index)
            {
                if (index < 0) throw  new ArgumentOutOfRangeException(nameof(index), "index out of range");
                ExtendMemoryIfNeeded(index);
                return _program[index];
            }

            private void ExtendMemoryIfNeeded(long index)
            {
                if (index + 1 > _program.Length)
                {
                    var newArray = new long[index + 1];
                    _program.CopyTo(newArray, 0);
                    _program = newArray;
                }
            }


            private void WriteValue(long parameter, long parameterMode, long value)
            {
                switch (parameterMode)
                {
                    case 0:
                        WriteIndex(parameter, value);
                        break;
                    case 1:
                        throw new Exception("Can't write to direct parameters");
                    case 2:
                        WriteIndex(parameter + _relativeBase, value);
                        break;
                    default:
                        throw new ArgumentException("Bad parameter mode", nameof(parameterMode));
                }
            }

            private int _infiniteLoopCheck = 0;

            public IEnumerable<long> RunProgram(params long[] queuedInputs)
            {
                var outputs = new List<long>();
                var inputStack = new Stack<long>(queuedInputs.Reverse());

                var parameterModes = new int[3];
               
                IsRunning = true;

                while (IsRunning && _infiniteLoopCheck < int.MaxValue)
                {
                    _infiniteLoopCheck++;

                    if (_instructionPointer > _program.Length)
                    {
                        IsRunning = false;
                        continue;
                    }

                    var opcode = _program[_instructionPointer] % 100;
                    var parameterModeString = ((_program[_instructionPointer] - opcode) / 100).ToString("000");
                    parameterModes[0] = (int)char.GetNumericValue(parameterModeString[2]);
                    parameterModes[1] = (int)char.GetNumericValue(parameterModeString[1]);
                    parameterModes[2] = (int)char.GetNumericValue(parameterModeString[0]);

                    switch (opcode)
                    {
                        case 1: //Add
                        {
                            var resultIndex = _program[_instructionPointer + 3];
                            var value = ReadValue(_program[_instructionPointer + 1], parameterModes[0])
                                                   + ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            WriteValue(resultIndex, parameterModes[2], value);
                            _instructionPointer += 3;
                            break;
                        }
                        case 2: // Mult
                        {
                            var resultIndex = _program[_instructionPointer + 3];
                            var value = ReadValue(_program[_instructionPointer + 1], parameterModes[0]) *
                                                   ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            WriteValue(resultIndex, parameterModes[2], value);
                            _instructionPointer += 3;
                            break;
                        }
                        case 3: //Input
                        {
                            var resultIndex = _program[_instructionPointer + 1];
                            if (!inputStack.Any())
                            {
                                return outputs;
                            }

                            WriteValue(resultIndex, parameterModes[0], inputStack.Pop());
                            _instructionPointer += 1;
                            break;
                        }
                        case 4: //Output
                        {
                            var val = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                                
                            outputs.Add(val);
                            _instructionPointer += 1;
                            break;
                        }
                        case 5: //jump-if-true
                        {
                            var value1 = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                            var value2 = ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            if (value1 != 0)
                            {
                                _instructionPointer = value2;
                                continue;
                            }

                            _instructionPointer += 2;
                            break;
                        }
                        case 6: //jump-if-false
                        {
                            var value1 = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                            var value2 = ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            if (value1 == 0)
                            {
                                _instructionPointer = value2;
                                continue;
                            }

                            _instructionPointer += 2;

                            break;
                        }
                        case 7: //less than
                        {
                            var value1 = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                            var value2 = ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            var resultIndex = _program[_instructionPointer + 3];
                            var value = (value1 < value2) ? 1 : 0;
                            WriteValue(resultIndex, parameterModes[2], value);
                                
                            _instructionPointer += 3;
                            break;
                        }
                        case 8: // Equals
                        {
                            var value1 = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                            var value2 = ReadValue(_program[_instructionPointer + 2], parameterModes[1]);
                            var resultIndex = _program[_instructionPointer + 3];
                            var value = (value1 == value2) ? 1 : 0;
                            WriteValue(resultIndex, parameterModes[2], value);
                            _instructionPointer += 3;
                            break;
                        }
                        case 9: // adjust relative base
                        {
                            var value1 = ReadValue(_program[_instructionPointer + 1], parameterModes[0]);
                            _relativeBase += value1;
                            _instructionPointer += 1;
                            break;
                        }
                        case 99: // Terminate
                            IsRunning = false;
                            continue;
                        default:
                            throw new Exception("Bad command");
                    }

                    _instructionPointer++;
                }

                return outputs;
            }
        }
    }
}
