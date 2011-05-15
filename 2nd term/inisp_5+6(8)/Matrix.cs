using System;

namespace Lab6
{
    class IncorrectIndex : Exception
    {
        public IncorrectIndex() : base() { }
    }

    class IncorrectSize : Exception
    {
        public IncorrectSize() : base() { }
    }

    class MatrixNoSquare : Exception
    {
        public MatrixNoSquare() : base() { }
    }

    class Matrix<T> where T: IMatrixItem
    {
        private T[][] Data;
        
        public int RowCount{get;set;}
        public int ColCount{get;set;}

        public Matrix(int n, int m)
        {
            RowCount = n;
            ColCount = m;
            Data = new T[n][];
            for (int i = 0; i < n; i++)
                Data[i] = new T[m];
        }

        public Matrix(T val): this(1, 1)
        {
            Data[0][0] = val;
        }

        public Matrix():this(0, 0){}

        public T this[int row, int col]
        {
            get
            {
                if (row <= RowCount && row > 0 && col <= ColCount && col > 0)
                    return Data[row - 1][col - 1];
                else
                    throw new IncorrectIndex();
            }
            set
            {
                if (row <= RowCount && row > 0 && col <= ColCount && col > 0)
                    Data[row - 1][col - 1] = value;
                else
                    throw new IncorrectIndex();
            }
        }

        public bool IsZero
        {
            get
            {
                for (int i = 0; i < RowCount; i++)
                    for (int j = 0; j < ColCount; j++)
                        if(Data[i][j].IsZero())
                            return false;
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
                for(int i = 0; i < RowCount; i++)
                    for(int j = 0; j < ColCount; j++)
                        if( (i == j && this[i,j].IsZero()) || (i != j && !this[i,j].IsZero()))
                            return false;
                return true;
            }
        }

        public bool IsSingle
        {
            get
            {
                if (IsDiagonal == false)
                    return false;
                for (int i = 0; i < RowCount; i++)
                    if (!this[i, i].IsSingle())
                        return false;
                return true;
            }
        }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.RowCount != m2.RowCount || m1.ColCount != m2.ColCount)
                throw new IncorrectSize();
            Matrix<T> Result = new Matrix<T>(m1.RowCount, m1.ColCount);
           // for (int i = 0; i < m1.RowCount; i++)
              //  for (int j = 0; j < m1.ColCount; j++)
                 //   Result[i, j] = m1[i, j] + m2[i, j];
            return Result;
        }

        public override string ToString()
        {
            string txt = "";
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
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
            int cur_row = 0;
            int cur_col = 0;
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    if (i != row && j != col)
                        this[cur_row++, cur_col++] = this[i, j];
            return result;
        }

        public T Cofactor(int row, int col)
        {
            Matrix<T> minor = GetMinor(row, col);
            T determinat = minor.Determinant;
            if ((row + col + 2) % 2 == 1)
                determinat *= -1;
            return determinat;
        }

        public T Determinant
        {
            get
            {
                if (!IsSquare)
                    throw new MatrixNoSquare();
                if (RowCount == 1)
                    return this[0, 0];
                T result = default(T);
                for (int i = 0; i < ColCount; i++)
                    result += this[0, i] * Cofactor(0, i);
                return result;
            }
        }



    }
}
