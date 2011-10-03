using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Lab2_Ozierski_Train
{
    class Train : IEquatable<Train>, IEnumerable<Truck>, IDisposable
    {
        public List<Truck> Trucks { get; private set; }

        public bool Equals(Train other)
        {
            if (other.Trucks.Count() != Trucks.Count())
                return false;
            foreach (Truck truck in Trucks)
                if (other.Trucks.IndexOf(truck) == -1)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Train))
                return false;
            return Equals((Truck)obj);
        }

        public Truck this[int index]
        {
            get
            {
                return Trucks[index];
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IEnumerator<Truck> GetEnumerator()
        {
            return Trucks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(Train a, Train b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Train a, Train b)
        {
            return !(a == b);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose method for Train");
        }
    }
}
