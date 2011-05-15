using System;

namespace Lab6
{
    interface IMatrixItem
    {
        bool IsSingle();
        bool IsZero();
    }

    class IntValue: IMatrixItem
    {
        private int value;

        IntValue(int val)
        {
            value = val;
        }

        public bool IsSingle()
        {
            return value == 1;
        }

        public bool IsZero()
        {
            return value == 0;
        }
    }
}