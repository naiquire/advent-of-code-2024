using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _19._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> patterns = new List<string>();
            HashSet<string> availablePatterns = new HashSet<string>();
            StreamReader sr = new StreamReader("input.txt");
            while (!sr.EndOfStream) { patterns.Add(sr.ReadLine()); }
            sr.Close();

            sr = new StreamReader("input2.txt");
            string[] temp = sr.ReadLine().Split(' ');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                temp[i] = temp[i].Substring(0, temp[i].Length - 1);
            }
            foreach (string str in temp) { availablePatterns.Add(str); }

            int total = 0;
            int count = 1;
            foreach (string pattern in patterns)
            {
                HashSet<string> invalid = new HashSet<string>();
                Dictionary<string, bool> valid = new Dictionary<string, bool>();
                if (calculate(pattern, availablePatterns, invalid, valid)) { total++; }
                //Console.WriteLine($"{count}/400\t{yes}");
                count++;
            }
            Console.WriteLine($"Part 1: {total}");

            long total2 = 0;
            int count2 = 1;
            foreach (string pattern in patterns)
            {
                HashSet<string> invalid = new HashSet<string>();
                Dictionary<string, long> valid = new Dictionary<string, long>();
                long returned = calculate2(pattern, availablePatterns, invalid, valid, 0);
                total2 += returned;
                //Console.WriteLine($"{count2}/400\t{returned}");
                count2++;
            }
            Console.WriteLine($"Part 2: {total2}");

            Console.ReadKey();

        }

        static bool calculate(string pattern, HashSet<string> availablePatterns, HashSet<string> invalid, Dictionary<string, bool> valid)
        {
            if (valid.TryGetValue(pattern, out bool result) == true) { return result; }
            else if (availablePatterns.Contains(pattern)) { return true; }
            else if (invalid.Contains(pattern)) { return false; }
            else if (pattern.Length == 1) { return false; }
            for (int i = pattern.Length; i > 0; i--)
            {
                if (availablePatterns.Contains(pattern.Substring(0, i)))
                {
                    if (calculate(pattern.Substring(i), availablePatterns, invalid, valid))
                    {
                        try { valid.Add(pattern.Substring(i), true); }
                        catch { }
                        return true;
                    }
                    else { invalid.Add(pattern.Substring(i)); }
                }
            }
            return false;
        }
        static long calculate2(string pattern, HashSet<string> availablePatterns, HashSet<string> invalid, Dictionary<string, long> valid, long total3)
        {
            long temp2 = 0;
            if (valid.TryGetValue(pattern, out temp2)) { return temp2; }
            else if (invalid.Contains(pattern)) { return 0; }
            else if (availablePatterns.Contains(pattern)) { total3++; }
            else if (pattern.Length == 1) { return 0; }
            for (int i = pattern.Length; i > 0; i--)
            {
                if (availablePatterns.Contains(pattern.Substring(0, i)))
                {
                    long temp = calculate2(pattern.Substring(i), availablePatterns, invalid, valid, 0);
                    if (temp != 0)
                    {
                        total3 += temp;
                        try { valid.Add(pattern.Substring(i), temp); }
                        catch { }
                    }
                    else { invalid.Add(pattern.Substring(i)); }
                }
            }
            return total3;
        }
    }
}
