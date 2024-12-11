using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int>[] lists = new List<int>[197];
            int count = 0;
            StreamReader sr = new StreamReader("lists.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] split = line.Split(',');
                lists[count] = new List<int>();
                foreach (string s in split)
                {
                    lists[count].Add(int.Parse(s));
                }
                count++;
            }
            sr.Close();
            List<string> rules = new List<string>();
            sr = new StreamReader("rules.txt");
            while (!sr.EndOfStream)
            {
                rules.Add(sr.ReadLine());
            }
            sr.Close();
            int total = 0;
            int incTotal = 0;
            for (int i = 0; i < lists.Length; i++)
            {
                bool validList = true;
                for (int value = 0; value < lists[i].Count; value++)
                {
                    for (int currentRule = 0; currentRule < rules.Count; currentRule++)
                    {
                        if (int.Parse(rules[currentRule].Substring(0, 2)) == lists[i][value])
                        {
                            int hold = int.Parse(rules[currentRule].Substring(3, 2));
                            for (int prevValue = 0; prevValue < value; prevValue++)
                            {
                                if (lists[i][prevValue] == hold) { validList = false; break; }
                            }
                        }
                    }
                }
                if (validList)
                {
                    total += lists[i][((lists[i].Count + 1) / 2) - 1];
                }
                else
                {
                    int currentSelected = lists[i].Count - 1;
                    while (currentSelected != 0)
                    {
                        bool swap = false;
                        for (int value = currentSelected - 1; value >= 0; value--)
                        {
                            for (int currentRule = 0; currentRule < rules.Count; currentRule++)
                            {
                                if (int.Parse(rules[currentRule].Substring(0, 2)) == lists[i][currentSelected])
                                {
                                    int hold = int.Parse(rules[currentRule].Substring(3, 2));
                                    
                                    if (lists[i][value] == hold)
                                    {
                                        int positionToSwap = value;
                                        int holdValue = lists[i][currentSelected];
                                        lists[i][currentSelected] = lists[i][positionToSwap];
                                        lists[i][positionToSwap] = holdValue;
                                        swap = true;
                                        currentSelected = lists[i].Count - 1;
                                    }
                                }
                            }
                        }
                        if (!swap) { currentSelected--; }
                    }
                    Console.WriteLine($"Checking incorrect list {i}/197");
                    incTotal += lists[i][((lists[i].Count + 1) / 2) - 1];
                }
            }
            Console.WriteLine();
            Console.WriteLine(total);
            Console.WriteLine(incTotal);
            Console.ReadKey();
        }
    }
}
