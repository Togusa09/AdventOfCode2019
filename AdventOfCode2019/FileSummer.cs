using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    public class FileSummer<T> where T : ISummable, new()
    {
        private T _summable;

        public FileSummer()
        {
            _summable = new T();
        }

        public int SumAll()
        {
            var lines = File.ReadAllLines("InputFiles\\" + _summable.Filename);
            var total = lines.Select(int.Parse)
                .Select(x => _summable.Calculate(x))
                .Sum();
            return total;
        }
    }
}