namespace AdventOfCode2019.Puzzles.Day1
{
    public class Day1Part1 : ISummable
    {
        public string Filename => "Day1Input.txt";

        public int Calculate(int mass)
        {
            var dividedMass = mass / 3;
            var subtracted = dividedMass - 2;
            return subtracted;
        }
    }
}
