namespace AdventOfCode2019.Puzzles.Day1
{
    public class Day1Part2 : ISummable
    {
        public string Filename => "Day01Input.txt";

        public int Calculate(int mass)
        {
            var f = CalcFuel(mass);
            var total = f;
            while (CalcFuel(f) >= 0)
            {
                f = CalcFuel(f);
                total += f;
            }

            return total;
        }

        private int CalcFuel(int mass)
        {
            var dividedMass = mass / 3;
            var subtracted = dividedMass - 2;
            return subtracted;
        }
    }
}
