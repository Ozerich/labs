using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Lab1_Nechaev_Students
{
    public class Group : IEnumerable<Student>, IEquatable<Group>
    {
        public List<Student> Students { get; private set; }

        public string Number { get; set; }

        public Group()
            : this("Noname")
        {
        }

        public Group(string number)
        {
            Number = number;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return Students.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Student this[int index]
        {
            get
            {
                return Students[index];
            }
        }

        public override bool Equals(object obj)
        {
            return (this == (Group)obj);
        }

        public override int GetHashCode()
        {
            return Int32.Parse(Number);
        }

        public override string ToString()
        {
            string res = "\nGroup: " + Number + "\n\n";
            foreach (Student student in this)
                res += student.ToString();
            return res;
        }

        public bool Equals(Group other)
        {
            if (Number != other.Number)
                return false;

            foreach (Student student in this)
                if (other.Students.IndexOf(student) == -1)
                    return false;

            return true;
        }
    }
}
