using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace _15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 50;
            char[,] array = new char[size, size];
            StreamReader sr = new StreamReader("input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++) { array[count, i] = line[i]; }
                count++;
            }
            sr.Close();
            string directions = File.ReadAllText("input2.txt");
            sr.Close();

            #region part1
            int x = 0; int y = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (array[row, column] == '@')
                    {
                        array[row, column] = '.';
                        x = row; y = column; break;
                    }
                }
            }
            for (int direction = 0; direction < directions.Length; direction++)
            {
                if (directions[direction] == '^')
                {
                    if (array[x - 1, y] == '#') { continue; }
                    else if (array[x - 1, y] == 'O')
                    {
                        for (int check = x - 2; check >= 0; check--)
                        {
                            if (array[check, y] == '#') { break; }
                            if (array[check, y] == '.')
                            {
                                array[check, y] = 'O';
                                array[x - 1, y] = '.';
                                x--; break;
                            }
                        }
                    }
                    else { x--; }
                }
                if (directions[direction] == '>')
                {
                    if (array[x, y + 1] == '#') { continue; }
                    else if (array[x, y + 1] == 'O')
                    {
                        for (int check = y + 2; check < size; check++)
                        {
                            if (array[x, check] == '#') { break; }
                            if (array[x, check] == '.')
                            {
                                array[x, check] = 'O';
                                array[x, y + 1] = '.';
                                y++; break;
                            }
                        }
                    }
                    else { y++; }
                }
                if (directions[direction] == 'v')
                {
                    if (array[x + 1, y] == '#') { continue; }
                    else if (array[x + 1, y] == 'O')
                    {
                        for (int check = x + 2; check < size; check++)
                        {
                            if (array[check, y] == '#') { break; }
                            if (array[check, y] == '.')
                            {
                                array[check, y] = 'O';
                                array[x + 1, y] = '.';
                                x++; break;
                            }
                        }
                    }
                    else { x++; }
                }
                if (directions[direction] == '<')
                {
                    if (array[x, y - 1] == '#') { continue; }
                    else if (array[x, y - 1] == 'O')
                    {
                        for (int check = y - 2; check >= 0; check--)
                        {
                            if (array[x, check] == '#') { break; }
                            if (array[x, check] == '.')
                            {
                                array[x, check] = 'O';
                                array[x, y - 1] = '.';
                                y--; break;
                            }
                        }
                    }
                    else { y--; }
                }
            }

            Console.WriteLine();
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Console.Write(array[row, column]);
                }
                Console.WriteLine();
            }

            int total = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (array[row, column] == 'O')
                    {
                        total += (100 * row) + column;
                    }
                }
            }

            Console.WriteLine(total);
            #endregion
            #region part2
            array = new char[size, size * 2];
            sr = new StreamReader("input.txt");
            count = 0;
       
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '#')
                    {
                        array[count, i * 2] = '#';
                        array[count, (i * 2) + 1] = '#';
                    }
                    if (line[i] == '.')
                    {
                        array[count, i * 2] = '.';
                        array[count, (i * 2) + 1] = '.';
                    }
                    if (line[i] == 'O')
                    {
                        array[count, i * 2] = '[';
                        array[count, (i * 2) + 1] = ']';
                    }
                    if (line[i] == '@')
                    {
                        array[count, i * 2] = '@';
                        array[count, (i * 2) + 1] = '.';
                    }
                }
                count++;
            }
            sr.Close();
            directions = File.ReadAllText("input2.txt");
            sr.Close();

            x = 0; y = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size * 2; column++)
                {
                    if (array[row, column] == '@')
                    {
                        array[row, column] = '.';
                        x = row; y = column; break;
                    }
                }
            }

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size * 2; column++)
                {
                    Console.Write(array[row, column]);
                }
                Console.WriteLine();
            }

            for (int direction = 0; direction < directions.Length; direction++)
            {
                if (directions[direction] == '^')
                {
                    if (array[x - 1, y] == '#') { continue; }
                    else if (array[x - 1, y] == '[' || array[x - 1, y] == ']')
                    {
                        List<int[]> coords = new List<int[]>();
                        int[] temp = new int[2] { x - 1, y };
                        int[] temp2 = new int[2];
                        if (array[x - 1, y] == '[') { temp2 = new int[2] { x - 1, y + 1 }; }
                        else { temp2 = new int[2] { x - 1, y - 1 }; }
                        coords.Add(temp); coords.Add(temp2);

                        bool newBoxFound = true;
                        bool tagFound = false;
                        while (newBoxFound)
                        {
                            newBoxFound = false;
                            int length = coords.Count;
                            for (int i = 0; i < length; i++)
                            {
                                int[] coord = coords[i];
                                if (array[coord[0] - 1, coord[1]] == '#') { tagFound = true; break; }
                                bool isNew = true;
                                foreach (int[] coordCheck in coords)
                                {
                                    if (coord[0] - 1 == coordCheck[0] && coord[1] == coordCheck[1]) { isNew = false; break; }
                                }
                                if ((array[coord[0] - 1, coord[1]] == '[' || array[coord[0] - 1, coord[1]] == ']') && isNew)
                                {
                                    int[] temp1 = new int[2] { coord[0] - 1, coord[1] };
                                    int[] temp12 = new int[2];
                                    if (array[coord[0] - 1, coord[1]] == '[') { temp12 = new int[2] { coord[0] - 1, coord[1] + 1 }; }
                                    else { temp12 = new int[2] { coord[0] - 1, coord[1] - 1 }; }
                                    coords.Add(temp1); coords.Add(temp12);
                                    newBoxFound = true;
                                }
                            }
                        }

                        if (tagFound) { continue; }
                        else
                        {
                            for (int row = 0; row < size; row++)
                            {
                                for (int column = 0; column < size * 2; column++)
                                {
                                    foreach (int[] coord in coords)
                                    {
                                        if (coord[0] == row && coord[1] == column)
                                        {
                                            array[row - 1, column] = array[row, column];
                                            array[row, column] = '.';
                                        }
                                    }
                                }
                            }
                            x--;
                        }
                    }
                    else { x--; }
                }
                if (directions[direction] == '>')
                {
                    if (array[x, y + 1] == '#') { continue; }
                    else if (array[x, y + 1] == '[')
                    {
                        for (int check = y + 2; check < size * 2; check++)
                        {
                            if (array[x, check] == '#') { break; }
                            if (array[x, check] == '.')
                            {
                                for (int yes = check - 1; yes > y; yes--)
                                {
                                    array[x, yes + 1] = array[x, yes];
                                    array[x, yes] = '.';
                                }
                                y++; break;
                            }
                        }
                    }
                    else { y++; }
                }
                if (directions[direction] == 'v')
                {
                    if (array[x + 1, y] == '#') { continue; }
                    else if (array[x + 1, y] == '[' || array[x + 1, y] == ']')
                    {
                        List<int[]> coords = new List<int[]>();
                        int[] temp = new int[2] { x + 1, y };
                        int[] temp2 = new int[2];
                        if (array[x + 1, y] == '[') { temp2 = new int[2] { x + 1, y + 1 }; }
                        else { temp2 = new int[2] { x + 1, y - 1 }; }
                        coords.Add(temp); coords.Add(temp2);

                        bool newBoxFound = true;
                        bool tagFound = false;
                        while (newBoxFound)
                        {
                            newBoxFound = false;
                            int length = coords.Count;
                            for (int i = 0; i < length; i++)
                            {
                                int[] coord = coords[i];
                                if (array[coord[0] + 1, coord[1]] == '#') { tagFound = true; break; }
                                bool isNew = true;
                                foreach (int[] coordCheck in coords)
                                {
                                    if (coord[0] + 1 == coordCheck[0] && coord[1] == coordCheck[1]) { isNew = false; break; }
                                }
                                if ((array[coord[0] + 1, coord[1]] == '[' || array[coord[0] + 1, coord[1]] == ']') && isNew)
                                {
                                    int[] temp1 = new int[2] { coord[0] + 1, coord[1] };
                                    int[] temp12 = new int[2];
                                    if (array[coord[0] + 1, coord[1]] == '[') { temp12 = new int[2] { coord[0] + 1, coord[1] + 1 }; }
                                    else { temp12 = new int[2] { coord[0] + 1, coord[1] - 1 }; }
                                    coords.Add(temp1); coords.Add(temp12);
                                    newBoxFound = true;
                                }
                            }
                        }

                        if (tagFound) { continue; }
                        else
                        {
                            for (int row = size - 1; row >= 0; row--)
                            {
                                for (int column = 0; column < size * 2; column++)
                                {
                                    foreach (int[] coord in coords)
                                    {
                                        if (coord[0] == row && coord[1] == column)
                                        {
                                            array[row + 1, column] = array[row, column];
                                            array[row, column] = '.';
                                        }
                                    }
                                }
                            }
                            x++;
                        }
                    }
                    else { x++; }
                }
                if (directions[direction] == '<')
                {
                    if (array[x, y - 1] == '#') { continue; }
                    else if (array[x, y - 1] == ']')
                    {
                        for (int check = y - 2; check >= 0; check--)
                        {
                            if (array[x, check] == '#') { break; }
                            if (array[x, check] == '.')
                            {
                                for (int yes = check + 1; yes < y; yes++)
                                {
                                    array[x, yes - 1] = array[x, yes];
                                    array[x, yes] = '.';
                                }
                                y--; break;
                            }
                        }
                    }
                    else { y--; }
                }
                //for (int row = 0; row < size; row++)
                //{
                //    for (int column = 0; column < size * 2; column++)
                //    {
                //        if (row == x && column == y) { Console.Write('@'); }
                //        else { Console.Write(array[row, column]); }
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine(direction);
            }
            int total2 = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size * 2; column++)
                {
                    if (array[row, column] == '[')
                    {
                        total2 += (row * 100) + column;
                    }
                }
            }
            #endregion

            Console.WriteLine(total2);
            Console.ReadKey();
        }
    }
}
