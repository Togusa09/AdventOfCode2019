using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    interface ISolvable<T>
    {
        T SolveForFile();
        T Solve(string content);
    }
}
