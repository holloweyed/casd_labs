using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace casd_labs {
    public static class Program {
        static void Main()
        {
            string file = @"C:\\Users\\galuz\\Desktop\\file1.txt";
            string[] lines = File.ReadAllLines(file);

            int n = int.Parse(lines[0]);

            double[,] matr = new double[n, n];
            int numLine = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matr[i, j] = double.Parse(lines[numLine]);
                    numLine++;
                }
            }

            double[] vec = new double[n];
            for (int i = 0; i < n; i++)
            {
                vec[i] = double.Parse(lines[numLine]);
                numLine++;
            }

            bool Simmetry(double[,] xmatr, int y)
            {
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        if (xmatr[i, j] != xmatr[j, i]) return false;
                    }
                }
                return true;
            }

            double VecLength(double[,] xMatr, double[] yVec, int z)
            {
                if (Simmetry(xMatr, z) == false)
                {
                    Console.WriteLine("Матрица не симметрична");
                    return 0;
                }
                double[] vec2 = new double[z];
                double res = 0;
                for (int i = 0; i < z; i++)
                {
                    for (int j = 0; j < z; j++) vec2[i] += yVec[j] * xMatr[i, j];
                }

                for (int i = 0; i < z; i++) res += vec2[i] * yVec[i];

                return Math.Sqrt(res);
            }

            Console.WriteLine("Длина вектора = " + VecLength(matr, vec, n));
        }
    }
}