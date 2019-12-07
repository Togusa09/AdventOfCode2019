namespace AdventOfCode2019
{
    public interface ISummable
    {
        int Calculate(int val);

        string Filename { get; }
    }
}