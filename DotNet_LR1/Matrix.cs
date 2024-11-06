using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_LR1
{
    class Matrix
    {
        private int[,] matrix;
        private int m;
        private int n;
        private Random random = new(123);
        public Matrix(int m, int n, int min = 1, int max = 100, bool printResult = false)
        {
            this.m = m;
            this.n = n;
            this.matrix = new int[m, n];
            for (int i = 0; i < m * n; i++)
            {
                matrix[i / n, i % n] = random.Next(min, max);
            }
            if (printResult) this.Print();
        }

        public void Print()
        {
            Console.WriteLine("Matrix:");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int[] MultiplyOneThread(int[] vector)
        {
            int[] result = new int[m];
            for (int i = 0; i < m; i++)
            {
                result[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }

        public int[] MultiplyMultiThread(int[] vector, int blockSize, int threadCount)
        {
            int[] result = new int[m];
            int blockCount = (m + blockSize - 1) / blockSize;
            Thread[] threads = new Thread[threadCount];

            for (int t = 0; t < threadCount; t++)
            {
                int threadIndex = t;
                threads[t] = new Thread(() =>
                {
                    for (int b = threadIndex; b < blockCount; b += threadCount)
                    {
                        int startRow = b * blockSize;
                        int endRow = Math.Min(startRow + blockSize, m);
                        for (int i = startRow; i < endRow; i++)
                        {
                            result[i] = 0;
                            for (int j = 0; j < n; j++)
                            {
                                result[i] += matrix[i, j] * vector[j];
                            }
                        }
                    }
                });
                threads[t].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }
    }

}
