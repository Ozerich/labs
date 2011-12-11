using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Lab2_Ozierski_Train
{
    [Serializable]
    [XmlRoot("Train")]
    public class Train : IEquatable<Train>, IEnumerable<Coach>, IDisposable
    {
        [XmlArray("list")]
        [XmlArrayItem("Coach")]
        public List<Coach> Coaches { get; private set; }
        [XmlAttribute("number")]
        public int Number { get; private set; }

        public Train(int number)
        {
            Number = number;
            Coaches = new List<Coach>();
        }

        public bool Equals(Train other)
        {
            if (other.Coaches.Count() != Coaches.Count())
                return false;
            foreach (Coach truck in Coaches)
                if (other.Coaches.IndexOf(truck) == -1)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Train))
                return false;
            return Equals((Train)obj);
        }

        public Coach this[int index]
        {
            get
            {
                return Coaches[index];
            }
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public IEnumerator<Coach> GetEnumerator()
        {
            return Coaches.GetEnumerator();
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

        public void AddCoach(Coach coach)
        {
            Coaches.Add(coach);
        }

        public void Add(Object obj)
        {
            if (obj is Coach)
                AddCoach((Coach)obj);
        }

        public void AddRoom(int coach_id, Room room)
        {
            Coach coach = Coaches.Where<Coach>(x => x.Id == coach_id).First();
            coach.AddRoom(room);
        }

        public int GetCoachCount()
        {
            return Coaches.Count();
        }

        public int GetPassengerCount()
        {
            return Coaches.Sum(x => x.Sum(y => y.PassengerCount));
        }

        public IEnumerable<Coach> GetCoachesByType(CoachType coachType)
        {
            return Coaches.Where(x => coachType == x.Type);
        }

        public Coach AddPassenger(CoachType coachType)
        {
            foreach (Coach coach in GetCoachesByType(coachType))
                if (coach.HasFreePlace())
                {
                    coach.AddPassenger();
                    return coach;
                }
            throw new InvalidOperationException("No free coaches");
        }

        public override string ToString()
        {
            string res = "Train number: " + Number + "\n";
            foreach (Coach coach in Coaches)
                res += coach;
            return res;
        }

    }
}