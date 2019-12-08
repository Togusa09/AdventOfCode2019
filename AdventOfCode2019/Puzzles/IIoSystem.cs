namespace AdventOfCode2019.Puzzles
{
    public interface IIoSystem
    {
        string ReadIn();
        void WriteOut(string value);
    }
}