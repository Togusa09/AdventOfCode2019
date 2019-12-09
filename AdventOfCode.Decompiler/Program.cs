using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Decompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var content = File.ReadAllText("InputFiles\\Day2Input.txt");
            //var content = File.ReadAllText("InputFiles\\Day5Input.txt");
            //var content = File.ReadAllText("InputFiles\\Day7Input.txt");
            var content = File.ReadAllText("InputFiles\\Day9Input.txt");

            var extractedProgram = content.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToArray();

            var decoder = new IntDecoder();
            var result = decoder.Decode(extractedProgram);

            Console.WriteLine(result);
        }
    }

    class IntDecoder
    {
        private string FormatVal(int val, int mode)
        {
            switch (mode)
            {
                case 0:
                    return $"*{val}";
                case 1:
                    return $"{val}";
                case 2:
                    return $"+{val}";
            }

            return val.ToString();
        }

        public string Decode(int[] program)
        {
            var textCode = new StringBuilder();

            for (var i = 0; i < program.Length; i++)
            {
                var opcode = program[i] % 100;
                var parameterModeString = ((program[i] - opcode) / 100).ToString("000");
                var parameterModes = new int[3];
                parameterModes[0] = (int)char.GetNumericValue(parameterModeString[2]);
                parameterModes[1] = (int)char.GetNumericValue(parameterModeString[1]);
                parameterModes[2] = (int)char.GetNumericValue(parameterModeString[0]);

                var val1 = FormatVal(program.ElementAtOrDefault(i + 1), parameterModes[0]);
                var val2 = FormatVal(program.ElementAtOrDefault(i + 2), parameterModes[1]);
                var val3 = FormatVal(program.ElementAtOrDefault(i + 3), parameterModes[2]);

                textCode.Append($"#{i:000} ");

                switch (opcode)
                {
                    case 1:
                        textCode.AppendLine($"ADD {val1}\t{val2}\t{val3}");
                        i += 3;
                        break;
                    case 2:
                        textCode.AppendLine($"MUL {val1}\t{val2}\t{val3}");
                        i += 3;
                        break;
                    case 3:
                        textCode.AppendLine($"INP {val1}");
                        i += 1;
                        break;
                    case 4:
                        textCode.AppendLine($"OUT {val1}");
                        i += 1;
                        break;
                    case 5:
                        textCode.AppendLine($"JIT {val1}\t{val2}");
                        i += 2;
                        break;
                    case 6:
                        textCode.AppendLine($"JIF {val1}\t{val2}");
                        i += 2;
                        break;
                    case 7:
                        textCode.AppendLine($"LST {val1}\t{val2}\t{val3}");
                        i += 3;
                        break;
                    case 8:
                        textCode.AppendLine($"EQL {val1}\t{val2}\t{val3}");
                        i += 3;
                        break;
                    case 9:
                        textCode.AppendLine($"ARB {val1}");
                        i += 2;
                        break;
                    case 99:
                        textCode.AppendLine("END");
                        break;
                    default:
                        textCode.AppendLine($"MEM {program[i]}");
                        break;

                }
            }

            return textCode.ToString();
        }
    }
}
