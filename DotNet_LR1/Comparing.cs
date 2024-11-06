using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_LR1
{
    class Comparing
    {
        private Random random = new(111);
        private int blockSize;
        private int[] vector;
        Matrix matrix;
        private int threadCount;
        private int m;
        private int n;
        private bool printResult;
        public Comparing(int m, int n, int blockSize, int threadCount, int min = 1, int max = 100, bool printResult = false)
        {
            this.m = m;
            this.n = n;
            this.blockSize = blockSize;
            this.threadCount = threadCount;
            Console.WriteLine($"m = {m}  n = {n}\n");
            vector = Enumerable.Range(0, n).Select(_ => random.Next(min, max)).ToArray();

            matrix = new Matrix(m, n, min, max, printResult);

            if (printResult)
            {
                Console.WriteLine("Vector: ");
                foreach (int x in vector)
                {
                    Console.WriteLine(x);
                }
                Console.WriteLine("\n");
            }

            this.printResult = printResult;

        }
        public void CompareApproach()
        {
            var timeOneThread = MeasureExecutionTime(() => matrix.MultiplyOneThread(vector), out var resultOneThread);
            Console.WriteLine($"One Thread.\n" +
                $"Time spent: {timeOneThread}\n");

            if (printResult)
            {
                Console.WriteLine("OneThread Result: ");
                foreach (int x in resultOneThread)
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine("\n");
            }

            var timeMultiThread = MeasureExecutionTime(() => matrix.MultiplyMultiThread(vector, blockSize, threadCount), out var resultMultiThread);
            Console.WriteLine($"MultiThread.\n" +
                $"Thread number: {threadCount}\n" +
                $"Blocksize: {blockSize}\n" +
                $"Time spent: {timeMultiThread}\n");

            if (printResult)
            {
                Console.WriteLine("MultiThread Result:");
                foreach (int x in resultMultiThread)
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
            }

            double boost = (double)timeOneThread / timeMultiThread;
            Console.WriteLine($"Boost: {boost}\n____________________________________________\n");
        }
        private int MeasureExecutionTime(Func<int[]> action, out int[] result)
        {
            var stopwatch = Stopwatch.StartNew();
            result = action();
            stopwatch.Stop();
            return (int)stopwatch.ElapsedMilliseconds;
        }
    }
}
