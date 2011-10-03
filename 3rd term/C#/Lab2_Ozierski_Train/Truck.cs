using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace Lab2_Ozierski_Train
{
    class Truck : IEquatable<Truck>, IEnumerable<Room>, IDisposable
    {
        public List<Room> Rooms { get; private set; }

        public bool Equals(Truck other)
        {
            if (other.Rooms.Count() != Rooms.Count())
                return false;
            foreach (Room room in Rooms)
                if (other.Rooms.IndexOf(room) == -1)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Truck))
                return false;
            return Equals((Truck)obj);
        }

        public Room this[int index]
        {
            get
            {
                return Rooms[index];
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return Rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(Truck a, Truck b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Truck a, Truck b)
        {
            return !(a == b);
        }

        public void Dispose()
        {
            Console.WriteLine("!!");
        }
    }

}