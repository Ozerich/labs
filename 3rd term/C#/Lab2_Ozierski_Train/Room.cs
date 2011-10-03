using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2_Ozierski_Train
{
    class Room : IEquatable<Room>, IDisposable
    {
        private int passengerCount;
        private int position = 0;

        public int PassengerCount
        {
            get
            {
                return passengerCount;
            }
            set
            {
                if (value < 0)
                    throw new FormatException("Passengers cannot be negative");
                else
                    passengerCount = value;
            }
        }

        public int Position
        {
            get
            {
                return position;
            }
            set{
                if(value < 0)
                    throw new FormatException("Position cannot be negative");
                else
                    position = value;
            }
        }

        public Room(int position)
        {
            Position = position;
        }

        public bool Equals(Room other)
        {
            return Position == other.Position;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Room))
                return false;
            return Equals((Room)obj);
        }

        public override int GetHashCode()
        {
            return Position;
        }

        public static bool operator ==(Room a, Room b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Room a, Room b)
        {
            return !a.Equals(b);
        }

        public void Dispose()
        {

        }
    }
}
