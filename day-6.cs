using System;
using System.IO;

namespace _6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] array = new string[130, 130];
            StreamReader sr = new StreamReader("input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++) { array[count, i] = line[i].ToString(); }
                count++;
            }
            sr.Close();
            int x = 89; int y = 67;
            string direction = "up";
            array[x, y] = "X";
            while (true)
            {
                if (x < 0 || x > 130 - 1 || y < 0 || y > 130 - 1) { break; }
                try
                {
                    if (direction == "up")
                    {
                        if (array[x - 1, y] == "#") { direction = "right"; }
                        else
                        {
                            x -= 1;
                            array[x, y] = "X";
                        }
                    }
                    else if (direction == "right")
                    {
                        if (array[x, y + 1] == "#") { direction = "down"; }
                        else
                        {
                            y += 1;
                            array[x, y] = "X";
                        }
                    }
                    else if (direction == "down")
                    {
                        if (array[x + 1, y] == "#") { direction = "left"; }
                        else
                        {
                            x += 1;
                            array[x, y] = "X";
                        }
                    }
                    else if (direction == "left")
                    {
                        if (array[x, y - 1] == "#") { direction = "up"; }
                        else
                        {
                            y -= 1;
                            array[x, y] = "X";
                        }
                    }
                }
                catch (Exception e)
                {
                    array[x, y] = "X";
                    break;
                }
            }
            int total = 0;
            for (int i = 0; i < 130; i++)
            {
                for (int j = 0; j < 130; j++)
                {
                    if (array[i, j] == "X") { total++; }
                }
            }
            Console.WriteLine(total);
            int total2 = 0;
            for (int i = 0; i < 130; i++)
            {
                for (int j = 0; j < 130; j++)
                {
                    if (array[i, j] == "X")
                    {
                        array[i, j] = "#";
                        x = 89; y = 67; int betterdirection = 1;
                        int loop = 0;
                        bool check = true;
                        while (loop < 50000)
                        {
                            try
                            {
                                if (betterdirection == 1)
                                {
                                    if (array[x - 1, y] == "#") { betterdirection = 2; }
                                    else { x -= 1; }
                                }
                                else if (betterdirection == 2)
                                {
                                    if (array[x, y + 1] == "#") { betterdirection = 3; }
                                    else { y += 1; }
                                }
                                else if (betterdirection == 3)
                                {
                                    if (array[x + 1, y] == "#") { betterdirection = 4; }
                                    else { x += 1; }
                                }
                                else if (betterdirection == 4)
                                {
                                    if (array[x, y - 1] == "#") { betterdirection = 1; }
                                    else { y -= 1; }
                                }
                            }
                            catch { check = false; break; }
                            loop++;
                        }
                        if (check) { total2++; }
                        array[i, j] = "X";
                    }
                }
            }
            Console.WriteLine(total2);
            Console.ReadKey();
        }
    }
}