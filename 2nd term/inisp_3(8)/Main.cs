using System;
using System.Collections.Generic;

namespace Lab3
{
    class MainClass
    {
        public static List<Matrix> data;

        const int BAD_INDEX = -1;

        public static int ReadInt(string hint)
        {
            int value;
            Console.Write(hint + " ");
            value = Int32.Parse(Console.ReadLine());
            return value;
        }

        public static int ReadIndex(string hint)
        {
            int index;
            Console.Write(hint + " ");
            index = int.Parse(Console.ReadLine());
            if (index <= 0 || index > data.Count)
            {
                Console.WriteLine("Incorrect index");
                return BAD_INDEX;
            }
            return index;
        }

        public static void PrintResult(string header, string text)
        {
            Console.Write(header);
            Console.WriteLine(text);
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        public static void Main(string[] argv)
        {
            data = new List<Matrix>();

            string cmd;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Commands: add, edit, view, math_add, mul_matrix, mul_number, minor, invert, federal, determinant, exit");
                Console.Write("> ");
                cmd = Console.ReadLine();
                if (cmd == "add")
                {
                    int rows = ReadInt("Rows count");
                    int cols = ReadInt("Columns count");
                    if (rows > 0 && cols > 0)
                    {
                        Matrix matrix = new Matrix(rows, cols);
                        data.Add(matrix);
                    }
                    else
                        Console.WriteLine("Incorrect size");
                }
                else if (cmd == "edit")
                {
                    int index = ReadIndex("Matrix index"); 
                    int row, col;
                    if (index == BAD_INDEX)
                        continue;
                    row = ReadInt("Row");
                    col = ReadInt("Col");
                    if (row > 0 && col > 0 && row <= data[index - 1].RowCount && col <= data[index - 1].ColCount)
                    {
                        Console.WriteLine("Old value " + data[index - 1].Get(row - 1, col - 1));
                        int value = ReadInt("New value");
                        data[index - 1].Set(row - 1, col - 1, value);
                    }
                    else
                        Console.WriteLine("Incorrect cell");
                }
                else if (cmd == "view")
                {
                    for (int i = 0; i < data.Count; i++)
                        Console.WriteLine((i + 1) + ":\n" + data[i].ToString());
                }
                else if (cmd == "math_add")
                {
                    int index1 = ReadIndex("Matrix 1 index");
                    if (index1 == BAD_INDEX)
                        continue;
                    int index2 = ReadIndex("Matrix 2 index");
                    if (index2 == BAD_INDEX)
                        continue;
                    Matrix matrix = Matrix.Add(data[index1 - 1], data[index2 - 1]);
					Console.WriteLine(matrix);
                    PrintResult("Matrix " + index1 + " + Matrix " + index2 + " =\n", matrix.ToString());
                }
                else if (cmd == "mul_matrix")
                {
                    int index1 = ReadIndex("Matrix 1 index");
                    if (index1 == BAD_INDEX)
                        continue;
                    int index2 = ReadIndex("Matrix 2 index");
                    if (index2 == BAD_INDEX)
                        continue;
                    Matrix matrix = Matrix.Multiply(data[index1 - 1], data[index2 - 1]);
                    PrintResult("Matrix " + index1 + " + Matrix " + index2 + " =\n", matrix.ToString());
                }
                else if (cmd == "mul_number")
                {
                    int index = ReadIndex("Matrix index");
                    if (index == BAD_INDEX)
                        continue;
                    int factor = ReadInt("Factor");
                    Matrix matrix = data[index - 1].Multiply(factor);
                    PrintResult("Matrix " + index + " * " + factor + " = \n", matrix.ToString());
                }
                else if (cmd == "minor")
                {
                    int index = ReadIndex("Matrix index");
                    Console.WriteLine("Input minor:");
                    int row = ReadInt("Row");
                    int col = ReadInt("Column");
                    Matrix matrix = data[index - 1].GetMinor(row - 1, col - 1);
                    PrintResult("Minor of Matrix " + index + " = \n", matrix.ToString());
                }
                else if (cmd == "federal")
                {
                    int index = ReadIndex("Matrix index");
                    Matrix matrix = data[index - 1].GetFederal();
                    PrintResult("Federal of Matrix " + index + "= \n", matrix.ToString());

                }
                else if (cmd == "invert")
                {
                    int index = ReadIndex("Matrix index");
                    Matrix matrix = data[index - 1].GetInverted();
                    PrintResult("Invert of Matrix " + index + "= \n", matrix.ToString());

                }
                else if (cmd == "determinant")
                {
                    int index = ReadIndex("Matrix index");
                    int determinant = data[index - 1].Determinant();
                    PrintResult("Determinant = ", determinant.ToString());
                }
            }
        }
    }
}