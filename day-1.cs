using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            StreamReader sr = new StreamReader("input.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                left.Add(int.Parse(line.Substring(0, 5)));
                right.Add(int.Parse(line.Substring(line.Length - 5, 5)));
            }
            sr.Close();
            left.Sort();
            right.Sort();
            long total = 0;
            long total2 = 0;
            for (int i = 0; i < left.Count; i++)
            {
                if (right[i] > left[i]) { total += right[i] - left[i]; }
                else if (left[i] > right[i]) { total += left[i] - right[i]; }
                else { total += 0; }
                int sub = 0;
                foreach (int value in right)
                {
                    if (left[i] == value) { sub++; }
                }
                total2 += left[i] * sub;
            }
            Console.WriteLine(total);
            Console.WriteLine(total2);
            Console.ReadKey();
        }
    }
}