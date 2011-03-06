using System;

namespace Lab3
{
    class Matrix
    {
        private int[][] data;

        public int RowCount { get; set; }
        public int ColCount { get; set; }

        public static int ERROR_VALUE = -999999;

        public Matrix(int n, int m)
        {
            RowCount = n;
            ColCount = m;
            data = new int[n][];
            for (int i = 0; i < n; i++)
                data[i] = new int[m];
        }

        public Matrix(int one_value)
            : this(1, 1)
        {
            data[0][0] = one_value;
        }

        public Matrix()
            : this(0, 0)
        {
        }

        public void Set(int row, int col, int value)
        {
            if (row >= 0 && row < RowCount && col >= 0 && col < ColCount)
                data[row][col] = value;
        }

        public int Get(int row, int col)
        {
            if (row >= 0 && row < RowCount && col >= 0 && col < ColCount)
                return data[row][col];
            else
                return int.MinValue;
        }

        public bool IsZero()
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (data[i][j] != 0)
                        return false;
            return true;
        }

        public bool IsEqual(Matrix b)
        {
            if (RowCount != b.RowCount || ColCount != b.ColCount)
                return false;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (Get(i, j) != b.Get(i, j))
                        return false;
            return true;
        }

        public bool IsSquare()
        {
            return RowCount == ColCount;
        }

        public bool IsDiagonal()
        {
            if (!IsSquare())
                return false;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                {
                    if (i == j && Get(i, j) == 0)
                        return false;
                    if (i != j && Get(i, j) != 0)
                        return false;
                }
            return true;
        }

        public bool IsSingle()
        {
            if (!IsDiagonal())
                return false;
            for (int i = 0; i < RowCount; i++)
                if (Get(i, i) != 1)
                    return false;
            return true;
        }

        public Matrix Transpose()
        {
            Matrix matrix = new Matrix(ColCount, RowCount);

            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    matrix.Set(j, i, Get(i, j));

            return matrix;
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.RowCount != b.RowCount || a.ColCount != b.ColCount)
                return null;
            Matrix result = new Matrix(a.RowCount, a.ColCount);
            for (int i = 0; i < a.RowCount; i++)
                for (int j = 0; j < a.ColCount; j++)
                    result.Set(i, j, a.Get(i, j) + b.Get(i, j));
            return result;
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.RowCount != b.RowCount || a.ColCount != b.ColCount)
                return null;
            Matrix result = new Matrix(a.RowCount, a.ColCount);
            for (int i = 0; i < a.RowCount; i++)
                for (int j = 0; j < a.ColCount; j++)
                    result.Set(i, j, a.Get(i, j) - b.Get(i, j));
            return result;
        }


        public Matrix Multiply(int factor)
        {
            Matrix Result = new Matrix(RowCount, ColCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    Result.Set(i, j, Get(i, j) * factor);
            return Result;
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.ColCount != b.RowCount)
                return null;

            int value;
            Matrix result = new Matrix(a.RowCount, b.ColCount);
            for (int i = 0; i < result.RowCount; i++)
                for (int k = 0; k < result.ColCount; k++)
                {
                    value = 0;

                    for (int j = 0; j < a.ColCount; j++)
                        value += a.Get(i, j) * b.Get(j, k);

                    result.Set(i, k, value);
                }
            return result;
        }

        public Matrix GetFederal()
        {
            if (!IsSquare())
                return null;
            Matrix result = new Matrix(RowCount, ColCount);
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    result.Set(i, j, Cofactor(j, i));
            return result;
        }

        public Matrix GetMinor(int row, int col)
        {
            if (!IsSquare() || RowCount == 1)
                return null;

            Matrix result = new Matrix(RowCount - 1, ColCount - 1);
            int cur_row = 0;
            int cur_col = 0;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (i != row && j != col)
                        result.Set(cur_row++, cur_col++, Get(i, j));
            return result;
        }

        public Matrix GetInverted()
        {
            Matrix matrix = GetFederal();
            return matrix.Multiply(1 / Determinant());
        }

        public int Cofactor(int row, int col)
        {
            Matrix minor = GetMinor(row, col);
            int determinate = minor.Determinant();
            if ((row + col + 2) % 2 == 1)
                determinate *= -1;
            return determinate;
        }

        public int Determinant()
        {
            if (!IsSquare())
                return ERROR_VALUE;
            if (RowCount == 1)
                return Get(0, 0);
            int result = 0;
            for (int i = 0; i < ColCount; i++)
                result += Get(0, i) * Cofactor(0, i);
            return result;
        }

        public override string ToString()
        {
            string txt = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                    txt += Get(i, j) + " ";
                txt += "\n";
            }
            return txt;
        }

    }
}
