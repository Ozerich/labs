using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Linq;

namespace Lab1_Nechaev_Students
{
    public class Faculty : IEquatable<Faculty>, IEnumerable<Group>
    {
        public string Name { get; set; }

        public List<Group> Groups { get; private set; }

        public Faculty() 
        {
            Groups = new List<Group>();
        }

        public Faculty(string name) : this()
        {
            Name = name;
        }

        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }

        public override bool Equals(object obj)
        {
            return (this == (Faculty)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            string res = "Faculty: " + Name + "\n";

            foreach (Group group in Groups)
                res += group.ToString();

            return res;
        }

        public bool Equals(Faculty other)
        {
            if (Name != other.Name)
                return false;

            foreach (Group group in this)
                if (other.Groups.IndexOf(group) == -1)
                    return false;

            return true;
        }

        public IEnumerator<Group> GetEnumerator()
        {
            return Groups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int GetStudentsCount()
        {
            return Groups.Sum(x => x.Students.Count);
        }

        public int GetStudentCountWhereMark(int mark, bool isAbove)
        {
            return Groups.Sum(x => x.Where(y => (isAbove ? y.AverageMark >= mark : y.AverageMark < mark)).Count());
        }

        public double GetAverageMark()
        {
            return Groups.Average(x => x.Average(y => y.AverageMark));
        }
    }
}
