using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int end = 850;
            long[] results = new long[end];
            List<int>[] num = new List<int>[end];
            StreamReader sr = new StreamReader("input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == ':')
                    {
                        results[count] = long.Parse(line.Substring(0, i));
                        string numbers = line.Substring(i + 2, line.Length - i - 2);
                        List<string> temp = numbers.Split(' ').ToList();
                        num[count] = new List<int>();
                        for (int j = 0; j < temp.Count; j++) { num[count].Add(int.Parse(temp[j])); }
                    }
                }
                count++;
            }
            sr.Close();
            long total = 0;
            for (int number = 0; number < end;  number++)
            {
                bool found = false;
                bool notend = false;
                int[] array = new int[num[number].Count - 1];
                while ((!found) && (!notend))
                {
                    long currentTotal = (long)num[number][0];
                    for (int i = 0; i < num[number].Count - 1; i++)
                    {
                        if (array[i] == 0) { currentTotal += (long)num[number][i + 1]; }
                        if (array[i] == 1) { currentTotal *= (long)num[number][i + 1]; }
                        if (array[i] == 2) { currentTotal = long.Parse(currentTotal.ToString() + num[number][i + 1].ToString()); }
                    }
                    if (currentTotal == results[number]) { found = true; }
                    array[array.Length - 1] += 1;
                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        if (array[i] == 3)
                        {
                            if (i == 0) { notend = true; break; }
                            else
                            {
                                array[i] = 0;
                                array[i - 1] += 1;
                            }
                        }
                    }
                    if (found) { total += results[number]; }
                }
                Console.WriteLine($"{number}/850");
            }
            Console.Clear();
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
