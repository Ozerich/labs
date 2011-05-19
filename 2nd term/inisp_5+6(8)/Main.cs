using System;
using System.Collections.Generic;

namespace Lab6
{
    class IncorrectMatrixIndex : Exception
    {
        public IncorrectMatrixIndex() : base() { }
    }

    class IncorrectCommand : Exception
    {
        public IncorrectCommand() : base() { }
    }

    class MainClass
    {
        public static List< Matrix<IntValue> > data;

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
                throw new IncorrectMatrixIndex();
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
            data = new List< Matrix<IntValue> >();

            string cmd;
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Commands: add, edit, view, math_add, mul_matrix, mul_number, minor, determinant, exit");
                    Console.Write("> ");
                    cmd = Console.ReadLine();
                    if (cmd == "add")
                    {
                        int rows = ReadInt("Rows count");
                        int cols = ReadInt("Columns count");
                        data.Add(new Matrix<IntValue>(rows, cols));
                    }
                    else if (cmd == "edit")
                    {
                        int index = ReadIndex("Matrix index");
                        int row = ReadInt("Row"), col = ReadInt("Col");
                        Console.WriteLine("Old value " + data[index - 1][row, col]);
                        int value = ReadInt("New value");
                        data[index - 1][row, col] = (IntValue)value;

                    }
                    else if (cmd == "view")
                    {
                        for (int i = 0; i < data.Count; i++)
                            Console.WriteLine((i + 1) + ":\n" + data[i].ToString());
                    }
                    else if (cmd == "math_add")
                    {
                        int index1 = ReadIndex("Matrix 1 index");
                        int index2 = ReadIndex("Matrix 2 index");
                        Matrix<IntValue> matrix = data[index1 - 1] + data[index2 - 1];
                        Console.WriteLine(matrix);
                        PrintResult("Matrix " + index1 + " + Matrix " + index2 + " =\n", matrix.ToString());
                    }
                    else if (cmd == "mul_matrix")
                    {
                        int index1 = ReadIndex("Matrix 1 index");
                        int index2 = ReadIndex("Matrix 2 index");
                        Matrix<IntValue> matrix = data[index1 - 1] * data[index2 - 1];
                        PrintResult("Matrix " + index1 + " + Matrix " + index2 + " =\n", matrix.ToString());
                    }
                    else if (cmd == "mul_number")
                    {
                        int index = ReadIndex("Matrix index");
                        int factor = ReadInt("Factor");
                        Matrix<IntValue> matrix = data[index - 1] * factor;
                        PrintResult("Matrix " + index + " * " + factor + " = \n", matrix.ToString());
                    }
                    else if (cmd == "minor")
                    {
                        int index = ReadIndex("Matrix index");
                        Console.WriteLine("Input minor:");
                        int row = ReadInt("Row");
                        int col = ReadInt("Column");
                        Matrix<IntValue> matrix = data[index - 1].GetMinor(row, col);
                        PrintResult("Minor of Matrix " + index + " = \n", matrix.ToString());
                    }
                    else if (cmd == "determinant")
                    {
                        int index = ReadIndex("Matrix index");
                        int determinant = data[index - 1].Determinant;
                        PrintResult("Determinant = ", determinant.ToString());
                    }
                    else if (cmd == "exit")
                        break;
                    else
                        throw new IncorrectCommand();
                }
                catch (IncorrectCommand)
                {
                    Console.WriteLine("Incorrect command");
                }
                catch (IncorrectMatrixIndex)
                {
                    Console.WriteLine("Incorrect index");
                }
                catch (MatrixNoSquare)
                {
                    Console.WriteLine("Matrix is not square");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Divide by Zero");
                }
            }
        }
    }
}