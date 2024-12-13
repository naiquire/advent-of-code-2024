using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numEntries = 320;
            double[,,] array = new double[numEntries, 3, 2];
            StreamReader sr = new StreamReader("input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < 2; i++)
                {
                    string temp12 = sr.ReadLine();
                    array[count, i, 0] = double.Parse(temp12.Substring(12, 2));
                    array[count, i, 1] = double.Parse(temp12.Substring(18, 2));
                }
                string temp3 = sr.ReadLine();
                int csv = 0;
                for (int i = 0; i < temp3.Length; i++)
                {
                    if (temp3[i] == ',') { csv = i; break; }
                }
                array[count, 2, 0] = double.Parse(temp3.Substring(9, csv - 9)) + 10000000000000;
                array[count, 2, 1] = double.Parse(temp3.Substring(csv + 4, temp3.Length - (csv + 4))) + 10000000000000;
                count++;
                sr.ReadLine();
            }
            sr.Close();

            long[,] values = new long[numEntries, 2];
            bool[] equationSolvable = new bool[numEntries];
            for (int equation = 0; equation < numEntries; equation++)
            {
                equationSolvable[equation] = true;
                double[,] inputArray = new double[2, 3];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        inputArray[i, j] = array[equation, j, i];
                    }
                }
                double[] solutions = gaussianElimination(inputArray);
                values[equation, 0] = Convert.ToInt64(solutions[0]);
                values[equation, 1] = Convert.ToInt64(solutions[1]);

                if (values[equation, 0] * array[equation, 0, 0] + values[equation, 1] * array[equation, 1, 0] != array[equation, 2, 0]) { equationSolvable[equation] = false; }
                if (values[equation, 0] * array[equation, 0, 1] + values[equation, 1] * array[equation, 1, 1] != array[equation, 2, 1]) { equationSolvable[equation] = false; }
                if (values[equation, 0] < 0 || values[equation, 1] < 0) { equationSolvable[equation] = false; }
                Console.WriteLine($"Solved equation {equation + 1}/320");
            }

            long[] prices = new long[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                if (equationSolvable[i]) { prices[i] = values[i, 0] * 3 + values[i, 1]; }
            }
            Console.WriteLine("\n" + prices.Sum());
            Console.ReadKey();
        }
        static double[] gaussianElimination(double[,] matrix)
        {
            int numOfUnknowns = 2;
            double multiplier, temp;
            temp = matrix[0, 0];
            for (int i = 0; i < numOfUnknowns + 1; i++) { matrix[0, i] /= temp; }
            for (int row = 1; row < numOfUnknowns; row++)
            {
                for (int sub = 1; sub <= row; sub++)
                {
                    multiplier = matrix[row, sub - 1] / matrix[sub - 1, sub - 1];
                    for (int j = 0; j < numOfUnknowns + 1; j++)
                    {
                        matrix[row, j] = matrix[row, j] - (matrix[sub - 1, j] * multiplier);
                    }
                }
                temp = matrix[row, row];
                for (int i = 0; i < numOfUnknowns + 1; i++) { matrix[row, i] /= temp; }
            }
            double[] solutions = new double[numOfUnknowns];
            for (int numOfSubs = 0; numOfSubs < numOfUnknowns; numOfSubs++)
            {
                double total = 0;
                total = matrix[numOfUnknowns - numOfSubs - 1, numOfUnknowns];
                for (int i = 1; i <= numOfSubs; i++)
                {
                    total -= matrix[numOfUnknowns - numOfSubs - 1, numOfUnknowns - i] * solutions[numOfUnknowns - i];
                }
                solutions[numOfUnknowns - numOfSubs - 1] = total;
            }
            return solutions;
        }
    }
}
