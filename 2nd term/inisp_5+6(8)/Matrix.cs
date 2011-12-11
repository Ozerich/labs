using System;

namespace Lab6
{
    class IncorrectCellIndex : Exception
    {
        public IncorrectCellIndex() : base() { }
    }

    class IncorrectSize : Exception
    {
        public IncorrectSize() : base() { }
    }

    class MatrixNoSquare : Exception
    {
        public MatrixNoSquare() : base() { }
    }

    class Matrix<T> where T: IMatrixItem, new()
    {
        private T[][] Data;

        public delegate T CellModDelegate(T value);
        
        public int RowCount{get;set;}
        public int ColCount{get;set;}

        public Matrix(int n, int m)
        {
            RowCount = n;
            ColCount = m;
            Data = new T[n][];
            for (int i = 0; i < n; i++)
            {
                Data[i] = new T[m];
                for(int j = 0; j < m; j++)
                    Data[i][j] = new T();
            }
        }

        public Matrix(T val): this(1, 1)
        {
            Data[0][0] = val;
        }

        public Matrix():this(0, 0){}

        public void ForEach(CellModDelegate func)
        {
            for (int i = 1; i <= RowCount; i++)
                for (int j = 1; j <= ColCount; j++)
                    this[i, j] = func(this[i, j]);
        }

        public T this[int row, int col]
        {
            get
            {
                if (row <= RowCount && row > 0 && col <= ColCount && col > 0)
                    return Data[row - 1][col - 1];
                else
                    throw new IncorrectCellIndex();
            }
            set
            {
                if (row <= RowCount && row > 0 && col <= ColCount && col > 0)
                    Data[row - 1][col - 1] = value;
                else
                    throw new IncorrectCellIndex();
            }
        }

        public bool IsZero
        {
            get
            {
               // for (int i = 0; i < RowCount; i++)
                 //   for (int j = 0; j < ColCount; j++)
                   //     if(Data[i][j].IsZero())
                     //       return false;
                return true;
            }
        }

        public bool IsSquare
        {
            get
            {
                return (ColCount == RowCount && ColCount > 0);
            }
        }

        public bool IsDiagonal
        {
            get
            {
               // for(int i = 0; i < RowCount; i++)
                 //   for(int j = 0; j < ColCount; j++)
                   //     if( (i == j && this[i,j].IsZero()) || (i != j && !this[i,j].IsZero()))
                     //       return false;
                return true;
            }
        }

        public bool IsSingle
        {
            get
            {
                if (IsDiagonal == false)
                    return false;
            //    for (int i = 0; i < RowCount; i++)
              //      if (!this[i, i].IsSingle())
                //        return false;
                return true;
            }
        }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColCount != m2.ColCount)
                throw new IncorrectSize();
            Matrix<T> Result = new Matrix<T>(m1.RowCount, m1.ColCount);
            for (int i = 1; i <= m1.RowCount; i++)
                for (int j = 1; j <= m1.ColCount; j++)
                    Result[i, j] = (T)m1[i, j].Add(m2[i, j]);
            return Result;
        }

        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.ColCount != m2.RowCount)
                return null;

            T value = new T();
            Matrix<T> result = new Matrix<T>(m1.RowCount, m2.ColCount);
            for (int i = 1; i <= result.RowCount; i++)
                for (int k = 1; k <= result.ColCount; k++)
                {
                    value = (T)value.Zero;
                    for (int j = 1; j <= m1.ColCount; j++)
                        value = (T)value.Add(m1[i, j].Multiply(m2[j, k]));

                    result[i, k] = value;
                }
            return result;
        }

        public static Matrix<T> operator *(Matrix<T> m1, T val)
        {
            m1.ForEach(n => (T)n.Multiply(val));
            return m1;
        }

        public override string ToString()
        {
            string txt = "";
            for (int i = 1; i <= RowCount; i++)
            {
                for (int j = 1; j <= ColCount; j++)
                    txt += this[i, j] + " ";
                txt += "\n";
            }
            return txt;
        }

        public Matrix<T> GetMinor(int row, int col)
        {
            if (!IsSquare || RowCount == 1)
                return null;

            Matrix<T> result = new Matrix<T>(RowCount - 1, ColCount - 1);
            int cur_row = 1, cur_col = 1;
            for (int i = 1; i <= RowCount; i++)
                for (int j = 1; j <= ColCount; j++)
                    if (i != row && j != col)
                        result[cur_row++, cur_col++] = this[i, j];
            return result;
        }

        public T Cofactor(int row, int col)
        {
            Matrix<T> minor = GetMinor(row, col);
            T determinat = minor.Determinant;
            if ((row + col + 2) % 2 == 1)
                determinat.Transpose();
            return determinat;
        }

        public T Determinant
        {
            get
            {
                if (!IsSquare)
                    throw new MatrixNoSquare();
                if (RowCount == 1)
                    return this[1, 1];
                T result = new T();
                for (int i = 1; i <= ColCount; i++)
                    result = (T)result.Add(this[1, i].Multiply(Cofactor(1, i)));
                return result;
            }
        }


    }
}
