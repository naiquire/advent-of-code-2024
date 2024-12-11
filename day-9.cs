using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string input = sr.ReadLine();
            int[] data = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                data[i] = int.Parse(input[i].ToString());
            }
            sr.Close();
            List<int> chars = new List<int>();
            for (int character = 0; character < input.Length; character++)
            {
                if (character % 2 == 0)
                {
                    for (int i = 0; i < data[character]; i++) { chars.Add(character / 2); }
                }
                else
                {
                    for (int i = 0; i < data[character]; i++)
                    {
                        chars.Add(-1);
                    }
                }
            }

            #region part1
            //List<int> dotLocations = new List<int>();
            //for (int i = 0; i < chars.Count; i++)
            //{
            //    if (chars[i] == -1) { dotLocations.Add(i); }
            //}
            //for (int dot = 0; dot < dotLocations.Count; dot++)
            //{
            //    bool keepgoing = false;
            //    for (int i = dotLocations[dot]; i < chars.Count; i++)
            //    {
            //        if (chars[i] != -1) { keepgoing = true; break; }
            //    }

            //    if (keepgoing)
            //    {
            //        for (int back = chars.Count - 1; back >= 0; back--)
            //        {
            //            if (chars[back] != -1)
            //            {
            //                chars[dotLocations[dot]] = chars[back];
            //                chars[back] = -1;
            //                break;
            //            }
            //        }
            //    }
            //}
            #endregion

            int startIndex = chars.Count - 1;
            while (startIndex != 6)
            {
                for (int i = startIndex; i >= 0; i--)
                {
                    if (chars[startIndex] == -1)
                    {
                        startIndex = i - 1;
                        break;
                    }
                    else if (chars[i] != chars[startIndex])
                    {
                        int currentLength = startIndex - i;
                        for (int j = 0; j < i; j++)
                        {
                            if (chars[j] == -1)
                            {
                                int ismuchdots = 0;
                                for (int dotcheck = j; dotcheck < j + currentLength; dotcheck++)
                                {
                                    if (dotcheck > chars.Count - 1) { ismuchdots = -1; break; }
                                    if (chars[dotcheck] == -1) { ismuchdots++; }
                                }
                                if (ismuchdots >= currentLength)
                                {
                                    
                                    for (int k = 0; k < currentLength; k++)
                                    {
                                        chars[j + k] = chars[i + 1 + k];
                                        chars[i + 1 + k] = -1;
                                    }
                                    break;
                                }
                            }
                        }
                        startIndex = i;
                        break;
                    }
                }
            }
            long total = 0;
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i] != -1) { total += chars[i] * i; }
            }
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
