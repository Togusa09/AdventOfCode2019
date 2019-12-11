using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Puzzles.Day7
{
    public class Day7Part1
    {
        public int SolveForFile()
        {
            
            var highestVal = 0;
            for (var amp1Phase = 0; amp1Phase <= 4; amp1Phase++)
            for (var amp2Phase = 0; amp2Phase <= 4; amp2Phase++)
            for (var amp3Phase = 0; amp3Phase <= 4; amp3Phase++)
            for (var amp4Phase = 0; amp4Phase <= 4; amp4Phase++)
            for (var amp5Phase = 0; amp5Phase <= 4; amp5Phase++)
            {
                var t = new[] {amp1Phase, amp2Phase, amp3Phase, amp4Phase, amp5Phase}.Distinct().Count();
                if (t != 5) continue;

                var content = File.ReadAllText("InputFiles\\Day07Input.txt");
                var result = RunProgram(content, $"{amp1Phase},{amp2Phase},{amp3Phase},{amp4Phase},{amp5Phase}");
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

            var computer = new IntCodeComputer();

            var amp1Result = computer.RunProgram(extractedProgram, splitPhaseSequence[0], 0);
            var amp2Result = computer.RunProgram(extractedProgram, splitPhaseSequence[1], amp1Result.First());
            var amp3Result = computer.RunProgram(extractedProgram, splitPhaseSequence[2], amp2Result.First());
            var amp4Result = computer.RunProgram(extractedProgram, splitPhaseSequence[3], amp3Result.First());
            var amp5Result = computer.RunProgram(extractedProgram, splitPhaseSequence[4], amp4Result.First());
            return amp5Result.First();
        }

        class IntCodeComputer
        {
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

            public IEnumerable<int> RunProgram(int[] program, params int[] queuedInputs)
            {
                var outputs = new List<int>();
                var inputStack = new Stack<int>(queuedInputs.Reverse());

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
                    parameterModes[0] = (int) char.GetNumericValue(parameterModeString[1]);
                    parameterModes[1] = (int) char.GetNumericValue(parameterModeString[0]);

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
                            program[resultIndex] = inputStack.Pop();
                            instructionPointer += 1;
                            break;
                        }
                        case 4: //Output
                        {
                            var resultIndex = program[instructionPointer + 1];
                            var val = ReadValue(program, resultIndex, parameterModes[0]);
                                
                                outputs.Add(val);
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

                return outputs;
            }
        }
    }
}
