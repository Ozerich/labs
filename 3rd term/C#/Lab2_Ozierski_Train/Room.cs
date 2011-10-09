using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2_Ozierski_Train
{
    class Room : IEquatable<Room>, IDisposable
    {
        private int passengerCount;
		private int passengerLimit;
		
		public CoachType type{ get; private set; }
		
		public int PassengerLimit
		{
			get
			{
				return passengerLimit;
			}
			set
			{
				if(value < 0)
					throw new FormatException("Limit cannot be negative");
				else
					passengerLimit = value;
			}
		}

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
		
		public bool HasFreePlace()
		{
			return (PassengerLimit - PassengerCount > 0);
		}
		
		public void AddPassenger()
		{
			if(PassengerCount < PassengerLimit)
				PassengerCount++;
			else
				throw new InvalidOperationException("Room is full");
		}
		
		public void DeletePassenger()
		{
			if(passengerCount > 0)
				PassengerCount--;
			else
				throw new InvalidOperationException("Room is empty");
		}

        public Room(int passengerLimit)
        {
			PassengerLimit = passengerLimit;
			PassengerCount = 0;
        }

        public bool Equals(Room other)
        {
            return PassengerCount == other.PassengerCount && PassengerLimit == other.PassengerLimit;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Room))
                return false;
            return Equals((Room)obj);
        }

        public override int GetHashCode()
        {
            return PassengerCount;
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
			Console.WriteLine("Room dispose()");
        }
		
		public override string ToString ()
		{
			 return String.Format("{0} ({1})", PassengerCount, PassengerLimit);
		}
    }
}