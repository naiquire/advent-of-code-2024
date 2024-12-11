using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 50;
            char[,] array = new char[size, size];
            char[,] altArray = new char[size, size];
            StreamReader sr = new StreamReader("input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int c = 0; c < line.Length; c++) { array[count, c] = line[c]; }
                count++;
            }
            sr.Close();
            int total = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (array[row, column] != '.')
                    {
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                if (row == i && column == j) { break; }
                                else
                                {
                                    if (array[i, j] == array[row, column])
                                    {
                                        int diffX = i - row;
                                        int diffY = j - column;
                                        int pointX = i + diffX; int pointY = j + diffY;
                                        if (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                        {
                                            if (altArray[pointX, pointY] != '#')
                                            {
                                                altArray[pointX, pointY] = '#';
                                            }
                                        }
                                        while (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                        {
                                            pointX = pointX + diffX; pointY = pointY + diffY;
                                            if (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                            {
                                                if (altArray[pointX, pointY] != '#')
                                                {
                                                    altArray[pointX, pointY] = '#';
                                                }
                                            }
                                        }
                                        diffX = row - i;
                                        diffY = column - j;
                                        pointX = row + diffX; pointY = column + diffY;
                                        if (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                        {
                                            if (altArray[pointX, pointY] != '#')
                                            {
                                                altArray[pointX, pointY] = '#';
                                            }
                                        }
                                        while (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                        {
                                            pointX = pointX + diffX; pointY = pointY + diffY;
                                            if (pointX >= 0 && pointX < size && pointY >= 0 && pointY < size)
                                            {
                                                if (altArray[pointX, pointY] != '#')
                                                {
                                                    altArray[pointX, pointY] = '#';
                                                }
                                            }
                                        }
                                        altArray[row, column] = '#';
                                        altArray[i, j] = '#';
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (altArray[i, j] == '#' && array[i, j] == '.') { Console.Write(altArray[i, j]); }
                    else { Console.Write(array[i,j]); }
                }
                Console.WriteLine();
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (altArray[i, j] == '#') { total++; }
                }
            }

            Console.WriteLine();
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
