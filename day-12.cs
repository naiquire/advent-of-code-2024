using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _12._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 140;
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

            List<List<int[]>> regions = new List<List<int[]>>();

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    bool valuePresent = false;
                    for (int region = 0; region < regions.Count; region++)
                    {
                        for (int coords = 0; coords < regions[region].Count; coords++)
                        {
                            if (row == regions[region][coords][0] && column == regions[region][coords][1]) { valuePresent = true; break; }
                        }
                    }
                    if (!valuePresent)
                    {
                        List<int[]> currentRegion = new List<int[]>();
                        int[] temp = new int[2] { row, column };
                        currentRegion.Add(temp);
                        bool coordAdded = true;
                        char currentChar = array[row, column];
                        while (coordAdded)
                        {
                            coordAdded = false;
                            int length = currentRegion.Count;
                            for (int i = 0; i < length; i++)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    try
                                    {
                                        if (array[currentRegion[i][0] + j, currentRegion[i][1]] == array[currentRegion[i][0], currentRegion[i][1]])
                                        {
                                            int[] tempcoords = new int[2] { currentRegion[i][0] + j, currentRegion[i][1] };
                                            bool coordPresent = false;
                                            for (int coords = 0; coords < currentRegion.Count; coords++)
                                            {
                                                if (tempcoords[0] == currentRegion[coords][0] && tempcoords[1] == currentRegion[coords][1]) { coordPresent = true; break; }
                                            }
                                            if (!coordPresent) { currentRegion.Add(tempcoords); coordAdded = true; }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (array[currentRegion[i][0], currentRegion[i][1] + j] == array[currentRegion[i][0], currentRegion[i][1]])
                                        {
                                            int[] tempcoords = new int[2] { currentRegion[i][0], currentRegion[i][1] + j };
                                            bool coordPresent = false;
                                            for (int coords = 0; coords < currentRegion.Count; coords++)
                                            {
                                                if (tempcoords[0] == currentRegion[coords][0] && tempcoords[1] == currentRegion[coords][1]) { coordPresent = true; break; }
                                            }
                                            if (!coordPresent) { currentRegion.Add(tempcoords); coordAdded = true; }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        Console.WriteLine($"Found region '{currentChar}' containing {currentRegion.Count} values");
                        regions.Add(currentRegion);
                    }
                }
            }

            int[] areaTotal = new int[regions.Count];
            count = 0;
            foreach (List<int[]> currentRegion in regions)
            {
                areaTotal[count] = currentRegion.Count;
                count++;
            }

            int[] perimeterTotal = new int[regions.Count];
            for (int indexRegion = 0; indexRegion < regions.Count; indexRegion++)
            {
                int perimeter = 0;
                List<int[]> currentRegion = regions[indexRegion];
                for (int coord = 0; coord < currentRegion.Count; coord++)
                {
                    if (currentRegion[coord][0] != size - 1)
                    {
                        if (array[currentRegion[coord][0] + 1, currentRegion[coord][1]] != array[currentRegion[coord][0], currentRegion[coord][1]]) { perimeter++; }
                    }
                    else { perimeter++; }
                    if (currentRegion[coord][0] != 0)
                    {
                        if (array[currentRegion[coord][0] - 1, currentRegion[coord][1]] != array[currentRegion[coord][0], currentRegion[coord][1]]) { perimeter++; }
                    }
                    else { perimeter++; }
                    if (currentRegion[coord][1] != size - 1)
                    {
                        if (array[currentRegion[coord][0], currentRegion[coord][1] + 1] != array[currentRegion[coord][0], currentRegion[coord][1]]) { perimeter++; }
                    }
                    else { perimeter++; }
                    if (currentRegion[coord][1] != 0)
                    {
                        if (array[currentRegion[coord][0], currentRegion[coord][1] - 1] != array[currentRegion[coord][0], currentRegion[coord][1]]) { perimeter++; }
                    }
                    else { perimeter++; }
                    perimeterTotal[indexRegion] = perimeter;
                }
            }

            int total1 = 0;
            for (int i = 0; i < regions.Count; i++)
            {
                total1 += perimeterTotal[i] * areaTotal[i];
            }
            Console.Clear();         

            int[] horizontalTotal = new int[regions.Count];
            int[] verticalTotal = new int[regions.Count];
            for (int currentRegion = 0; currentRegion < regions.Count; currentRegion++)
            {
                char currentLetter = array[regions[currentRegion][0][0], regions[currentRegion][0][1]];
                for (int row = 0; row < size; row++)
                {
                    int[] currentRow = new int[size];
                    for (int column = 0; column < size; column++)
                    {
                        bool contain1 = false;
                        bool contain2 = false;
                        foreach (int[] coord in regions[currentRegion])
                        {
                            if (contain1 && contain2) { break; }
                            if (coord[0] == row && coord[1] == column) { contain1 = true; }
                            if (coord[0] == row - 1 && coord[1] == column) { contain2 = true; }
                        }
                        if (row == 0 && contain1) { currentRow[column] = 1; }
                        else if (contain1 && contain2) { currentRow[column] = 0; }
                        else if (contain1) { currentRow[column] = 1; }
                        else { currentRow[column] = 0; }
                    }
                    int tempTotal = 0;
                    if (currentRow[0] == 1) { tempTotal++; }
                    for (int i = 1; i < size; i++)
                    {
                        if (currentRow[i] == 1 && currentRow[i - 1] == 0) { tempTotal++; }
                    }
                    horizontalTotal[currentRegion] += tempTotal;
                }
                for (int row = 0; row < size; row++)
                {
                    int[] currentRow = new int[size];
                    for (int column = 0; column < size; column++)
                    {
                        bool contain1 = false;
                        bool contain2 = false;
                        foreach (int[] coord in regions[currentRegion])
                        {
                            if (contain1 && contain2) { break; }
                            if (coord[0] == row && coord[1] == column) { contain1 = true; }
                            if (coord[0] == row + 1 && coord[1] == column) { contain2 = true; }
                        }
                        if (row == size - 1 && contain1) { currentRow[column] = 1; }
                        else if (contain1 && contain2) { currentRow[column] = 0; }
                        else if (contain1) { currentRow[column] = 1; }
                        else { currentRow[column] = 0; }
                    }
                    int tempTotal = 0;
                    if (currentRow[0] == 1) { tempTotal++; }
                    for (int i = 1; i < size; i++)
                    {
                        if (currentRow[i] == 1 && currentRow[i - 1] == 0) { tempTotal++; }
                    }
                    horizontalTotal[currentRegion] += tempTotal;
                }
                for (int column = 0; column < size; column++)
                {
                    int[] currentColumn = new int[size];
                    for (int row = 0; row < size; row++)
                    {
                        bool contain1 = false;
                        bool contain2 = false;
                        foreach (int[] coord in regions[currentRegion])
                        {
                            if (contain1 && contain2) { break; }
                            if (coord[0] == row && coord[1] == column) { contain1 = true; }
                            if (coord[0] == row && coord[1] == column - 1) { contain2 = true; }
                        }
                        if (column == 0 && contain1) { currentColumn[row] = 1; }
                        else if (contain1 && contain2) { currentColumn[row] = 0; }
                        else if (contain1) { currentColumn[row] = 1; }
                        else { currentColumn[row] = 0; }
                    }
                    int tempTotal = 0;
                    if (currentColumn[0] == 1) { tempTotal++; }
                    for (int i = 1; i < size; i++)
                    {
                        if (currentColumn[i] == 1 && currentColumn[i - 1] == 0) { tempTotal++; }
                    }
                    verticalTotal[currentRegion] += tempTotal;
                }
                for (int column = 0; column < size; column++)
                {
                    int[] currentColumn = new int[size];
                    for (int row = 0; row < size; row++)
                    {
                        bool contain1 = false;
                        bool contain2 = false;
                        foreach (int[] coord in regions[currentRegion])
                        {
                            if (contain1 && contain2) { break; }
                            if (coord[0] == row && coord[1] == column) { contain1 = true; }
                            if (coord[0] == row && coord[1] == column + 1) { contain2 = true; }
                        }
                        if (column == size - 1 && contain1) { currentColumn[row] = 1; }
                        else if (contain1 && contain2) { currentColumn[row] = 0; }
                        else if (contain1) { currentColumn[row] = 1; }
                        else { currentColumn[row] = 0; }
                    }
                    int tempTotal = 0;
                    if (currentColumn[0] == 1) { tempTotal++; }
                    for (int i = 1; i < size; i++)
                    {
                        if (currentColumn[i] == 1 && currentColumn[i - 1] == 0) { tempTotal++; }
                    }
                    verticalTotal[currentRegion] += tempTotal;
                }
                Console.WriteLine($"Checking region {currentLetter} => contains {horizontalTotal[currentRegion] + verticalTotal[currentRegion]} unique edges");                
            }
            Console.Clear();
            int total = 0;
            for (int i = 0; i < regions.Count; i++)
            {
                Console.WriteLine($"Current region has total {areaTotal[i]} * {horizontalTotal[i] + verticalTotal[i]} = {(horizontalTotal[i] + verticalTotal[i]) * areaTotal[i]}");
                total += (horizontalTotal[i] + verticalTotal[i]) * areaTotal[i];
            }
            Console.Clear();
            Console.WriteLine($"Part 1 = {total1}");
            Console.WriteLine($"Part 2 = {total}");
            Console.ReadKey();
        }
    }
}