using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace Lab2_Ozierski_Train
{
    [Serializable]
    public class Coach : IEquatable<Coach>, IEnumerable<Room>, IDisposable
    {
        [XmlArray("Rooms")]
        [XmlArrayItem("Room")]
        public List<Room> Rooms { get; private set; }
        [XmlAttribute("id")]
        public int Id { get; private set; }
        [XmlIgnore]
        public CoachType Type;

        public Coach(int id, CoachType ct = CoachType.Coupe)
        {
            Id = id;
            Type = ct;
            Rooms = new List<Room>();
        }

        public Coach()
            : this(0)
        {
        }

        public bool Equals(Coach other)
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
            if (!(obj is Coach))
                return false;
            return Equals((Coach)obj);
        }

        public Room this[int index]
        {
            get
            {
                return Rooms[index];
            }
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void Add(Object obj)
        {
            if (obj is Room)
                AddRoom((Room)obj);
        }

        public int GetFreePlaces()
        {
            return Rooms.Sum(x => (x.PassengerLimit - x.PassengerCount));
        }

        public bool HasFreePlace()
        {
            foreach (Room room in Rooms)
                if (room.HasFreePlace())
                    return true;
            return false;
        }

        public Room AddPassenger()
        {
            foreach (Room room in Rooms)
                if (room.HasFreePlace())
                {
                    room.AddPassenger();
                    return room;
                }
            throw new InvalidOperationException("There is no free places");
        }

        public void DeletePassenger()
        {
            foreach (Room room in Rooms)
                if (room.PassengerCount > 0)
                {
                    room.DeletePassenger();
                    return;
                }
            throw new InvalidOperationException("No passengers");
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return Rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(Coach a, Coach b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coach a, Coach b)
        {
            return !(a == b);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose for Truck");
        }

        public override string ToString()
        {
            string res = "Coach id: " + Id;
            res += "\nCoach type: " + Type + "\n";
            foreach (Room room in Rooms)
                res += room;
            return res + "\n";
        }
    }

}