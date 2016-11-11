using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

// Kudos to @HuBeZa
// Highly based on http://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c

namespace DotNetOutdated
{
    public static class TableParser
    {
        public static void ToStringTable<T>(
            this IEnumerable<T> values,
            string[] columnHeaders,
            Func<T, ConsoleColor> colorSelector,
            params Func<T, object>[] valueSelectors)
        {
            ToStringTable(values.ToArray(), columnHeaders, colorSelector, valueSelectors);
        }

        public static void ToStringTable<T>(
            this T[] values,
            string[] columnHeaders,
            Func<T, ConsoleColor> colorSelector,
            params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);
            var colors = new ConsoleColor[values.Length];

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                colors[rowIndex - 1] = colorSelector.Invoke(values[rowIndex - 1]);
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex].Invoke(values[rowIndex - 1]).ToString();
                }
            }

            ToStringTable(arrValues, colors);
        }

        private static void ToStringTable(this string[,] arrValues, ConsoleColor[] colors)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {

                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);

                    Console.Write(" | ");
                    if (rowIndex > 0)
                        Console.ForegroundColor = colors[rowIndex - 1];
                    Console.Write(cell);
                    Console.ResetColor();
                }

                // Print end of line
                Console.Write(" | ");
                Console.WriteLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    Console.Write(" |{0}| ", headerSpliter);
                    Console.WriteLine();
                }
            }
            
            Console.WriteLine();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }
    }
}