using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Nechaev_Students
{
    public class Student : IEquatable<Student>
    {
        private string student_id;
        private double average_mark;

        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string StudentId
        {
            get
            {
                return student_id;
            }
            set
            {
                if (value.Length != 6)
                    throw new ArgumentException("Student's ID must be xxxxxx");
                int num;
                try
                {
                    num = Int32.Parse(value);
                }
                catch
                {
                    throw new ArgumentException("Student's ID must contains only digits");
                }

                student_id = num.ToString();
            }
        }

        public double AverageMark
        {
            get
            {
                return average_mark;
            }
            set
            {
                if (value > 10 || value < 0)
                    throw new ArgumentException("Average mark must be in [0..10]");

                average_mark = value;
            }
        }

        public Student()
        {
            average_mark = 0;
        }
        public Student(string studentId, string firstName, string secondName, double averageMark)
        {
            FirstName = firstName;
            SecondName = secondName;
            StudentId = studentId;
            AverageMark = averageMark;
        }

        public override int GetHashCode()
        {
            return student_id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (this == (Student)obj);
        }

        public bool Equals(Student other)
        {
            return this.student_id == other.student_id;
        }

        public override string ToString()
        {
            return String.Format("Student №{0} - {1} {2} ( {3} )\r\n", StudentId, FirstName, SecondName, AverageMark);
        }
    }
}
