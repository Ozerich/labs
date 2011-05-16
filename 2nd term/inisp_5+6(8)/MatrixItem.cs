using System;

namespace Lab6
{
    interface IMatrixItem
    {
        bool IsZero();
        bool IsSingle();
        void Transpose();
        object Zero { get; }
        object One { get; }
        IMatrixItem Add(IMatrixItem b);
        IMatrixItem Multiply(IMatrixItem b);
        string ToString();
    }

    class IntValue: IMatrixItem
    {
        private int value;

        public IntValue(int val)
        {
            value = val;
        }

        public IntValue() : this(0) { }

        public object Zero
        {
            get
            {
                return 0;
            }
        }

        public object One
        {
            get
            {
                return 1;
            }
        }

        public bool IsSingle()
        {
            return value == 1;
        }

        public bool IsZero()
        {
            return value == 0;
        }

        public IMatrixItem Add(IMatrixItem b)
        {
            return new IntValue(value + ((IntValue)b).value);
        }

        public IMatrixItem Multiply(IMatrixItem b)
        {
            return new IntValue(value * ((IntValue)b).value);
        }

        public static implicit operator int(IntValue val)
        {
            return ((IntValue)val).value;
        }

        public static implicit operator IntValue(int val)
        {
            return new IntValue(val);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public void Transpose()
        {
            value = -value;
        }
    }
}