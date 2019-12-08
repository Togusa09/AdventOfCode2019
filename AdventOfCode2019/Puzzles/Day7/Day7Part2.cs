using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day7
{
    public class Day7Part2
    {
        public int SolveForFile()
        {
            var highestVal = 0;
            for (var amp1Phase = 5; amp1Phase <= 9; amp1Phase++)
            for (var amp2Phase = 5; amp2Phase <= 9; amp2Phase++)
            for (var amp3Phase = 5; amp3Phase <= 9; amp3Phase++)
            for (var amp4Phase = 5; amp4Phase <= 9; amp4Phase++)
            for (var amp5Phase = 5; amp5Phase <= 9; amp5Phase++)
            {
                var t = new[] {amp1Phase, amp2Phase, amp3Phase, amp4Phase, amp5Phase}.Distinct().Count();
                if (t != 5) continue;

                var content = File.ReadAllText("InputFiles\\Day7Input.txt");
                var result = RunProgram(content,  $"{amp1Phase},{amp2Phase},{amp3Phase},{amp4Phase},{amp5Phase}");
                if (result > highestVal) highestVal = result;
            }
            
            return highestVal;
        }

        public int RunProgram(string program, string phaseSequence)
        {
            var extractedProgram = program.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();
            var splitPhaseSequence = phaseSequence.Split(",")
                .Select(x => int.Parse(x))
                .ToArray();

            var ampResult = new int[5];

            var ampComputer1 = new IntCodeComputer(extractedProgram);
            var ampComputer2 = new IntCodeComputer(extractedProgram);
            var ampComputer3 = new IntCodeComputer(extractedProgram);
            var ampComputer4 = new IntCodeComputer(extractedProgram);
            var ampComputer5 = new IntCodeComputer(extractedProgram);

            ampResult[0] = ampComputer1.RunProgram(splitPhaseSequence[0], ampResult[4]).First();
            ampResult[1] = ampComputer2.RunProgram(splitPhaseSequence[1], ampResult[0]).First();
            ampResult[2] = ampComputer3.RunProgram(splitPhaseSequence[2], ampResult[1]).First();
            ampResult[3] = ampComputer4.RunProgram(splitPhaseSequence[3], ampResult[2]).First();
            ampResult[4] = ampComputer5.RunProgram(splitPhaseSequence[4], ampResult[3]).First();

            while (ampComputer5.IsRunning)
            {
                ampResult[0] = ampComputer1.RunProgram(ampResult[4]).FirstOrDefault();
                ampResult[1] = ampComputer2.RunProgram(ampResult[0]).FirstOrDefault();
                ampResult[2] = ampComputer3.RunProgram(ampResult[1]).FirstOrDefault();
                ampResult[3] = ampComputer4.RunProgram(ampResult[2]).FirstOrDefault();
                ampResult[4] = ampComputer5.RunProgram(ampResult[3]).FirstOrDefault();
            }

            return ampResult[4];
        }

        class IntCodeComputer
        {
            public bool IsRunning { get; private set; }
            private bool _waitingForInput = false;
            private int[] _program;
            private int _instructionPointer = 0;

            public IntCodeComputer(int[] program)
            {
                _program = new int[program.Length];
                program.CopyTo(_program, 0);
            }

            private int ReadValue(int[] program, int parameter, int parameterMode)
            {
                switch (parameterMode)
                {
                    case 0:
                        return program[parameter];
                    case 1:
                        return parameter;
                    default:
                        throw new ArgumentException("Bad parameter mode", nameof(parameterMode));
                }
            }

            private int _infiniteLoopCheck = 0;

            public IEnumerable<int> RunProgram(params int[] queuedInputs)
            {
                var outputs = new List<int>();
                var inputStack = new Stack<int>(queuedInputs.Reverse());

                var parameterModes = new int[2];
               
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
                    var parameterModeString = ((_program[_instructionPointer] - opcode) / 100).ToString("00");
                    parameterModes[0] = (int) char.GetNumericValue(parameterModeString[1]);
                    parameterModes[1] = (int) char.GetNumericValue(parameterModeString[0]);

                    switch (opcode)
                    {
                        case 1: //Add
                        {
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];
                            var resultIndex = _program[_instructionPointer + 3];
                            _program[resultIndex] = ReadValue(_program, firstIndex, parameterModes[0])
                                                   + ReadValue(_program, secondIndex, parameterModes[1]);
                            _instructionPointer += 3;
                            break;
                        }
                        case 2: // Mult
                        {
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];
                            var resultIndex = _program[_instructionPointer + 3];
                            _program[resultIndex] = ReadValue(_program, firstIndex, parameterModes[0]) *
                                                   ReadValue(_program, secondIndex, parameterModes[1]);
                            _instructionPointer += 3;
                            break;
                        }
                        case 3: //Input
                        {
                            var resultIndex = _program[_instructionPointer + 1];
                            if (!inputStack.Any())
                            {
                                _waitingForInput = true;
                                return outputs;
                            }

                            _waitingForInput = false;
                            _program[resultIndex] = inputStack.Pop();
                            _instructionPointer += 1;
                            break;
                        }
                        case 4: //Output
                        {
                            var resultIndex = _program[_instructionPointer + 1];
                            var val = ReadValue(_program, resultIndex, parameterModes[0]);
                                
                            outputs.Add(val);
                            _instructionPointer += 1;
                            break;
                        }
                        case 5: //jump-if-true
                        {
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];

                            var value1 = ReadValue(_program, firstIndex, parameterModes[0]);
                            var value2 = ReadValue(_program, secondIndex, parameterModes[1]);
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
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];

                            var value1 = ReadValue(_program, firstIndex, parameterModes[0]);
                            var value2 = ReadValue(_program, secondIndex, parameterModes[1]);
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
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];

                            var value1 = ReadValue(_program, firstIndex, parameterModes[0]);
                            var value2 = ReadValue(_program, secondIndex, parameterModes[1]);
                            var resultIndex = _program[_instructionPointer + 3];
                            _program[resultIndex] = (value1 < value2) ? 1 : 0;
                            _instructionPointer += 3;
                            break;
                        }
                        case 8: // Equals
                        {
                            var firstIndex = _program[_instructionPointer + 1];
                            var secondIndex = _program[_instructionPointer + 2];

                            var value1 = ReadValue(_program, firstIndex, parameterModes[0]);
                            var value2 = ReadValue(_program, secondIndex, parameterModes[1]);
                            var resultIndex = _program[_instructionPointer + 3];
                            _program[resultIndex] = (value1 == value2) ? 1 : 0;
                            _instructionPointer += 3;
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
