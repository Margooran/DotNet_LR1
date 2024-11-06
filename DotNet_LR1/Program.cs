using System;
using System.Diagnostics;
using System.Threading;

namespace DotNet_LR1
{
    class Program
    {
        static void Main()
        {
            Comparing compare0 = new Comparing(4, 3, 2, Environment.ProcessorCount, 1, 10, true);
            compare0.CompareApproach();

            Comparing compare1 = new Comparing(10000, 20000, 20, Environment.ProcessorCount);
            compare1.CompareApproach();

            Comparing compare2 = new Comparing(500, 1000, 20, Environment.ProcessorCount);
            compare2.CompareApproach();

            Comparing compare3 = new Comparing(10000, 20000, 20, 8);
            compare3.CompareApproach();

            Comparing compare4 = new Comparing(500, 1000, 20, 4);
            compare4.CompareApproach();
        }
    }
}