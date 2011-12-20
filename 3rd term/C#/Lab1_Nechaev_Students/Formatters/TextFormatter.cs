using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1_Nechaev_Students
{
    public class TextFormatter : BaseFormatter
    {
        private StreamReader sr;
        private StreamWriter sw;

        public override void WriteFaculty(Faculty faculty, Stream st)
        {
            sw = new StreamWriter(st);

            sw.WriteLine(faculty.Name);
            sw.WriteLine(faculty.Groups.Count);

            foreach (Group group in faculty.Groups)
                WriteGroup(group);

            sw.Close();
        }

        protected override void WriteGroup(Group group)
        {
            sw.WriteLine(group.Number);
            sw.WriteLine(group.Students.Count);

            foreach (Student st in group.Students)
                WriteStudent(st);

        }

        protected override void WriteStudent(Student student)
        {
            sw.WriteLine(student.StudentId);
            sw.WriteLine(student.FirstName);
            sw.WriteLine(student.SecondName);
            sw.WriteLine(student.AverageMark);
        }

        public override Faculty ReadFaculty(Stream st)
        {
            sr = new StreamReader(st);

            Faculty faculty = new Faculty();

            faculty.Name = sr.ReadLine();
            int count = Int32.Parse(sr.ReadLine());

            for (int i = 0; i < count; i++)
                faculty.Groups.Add(ReadGroup());

            sr.Close();
            return faculty;
        }

        protected override Group ReadGroup()
        {
            Group group = new Group();

            group.Number = sr.ReadLine();

            int count = Int32.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
                group.Students.Add(ReadStudent());

            return group;
        }

        protected override Student ReadStudent()
        {
            Student student = new Student();

            student.StudentId = sr.ReadLine();
            student.FirstName = sr.ReadLine();
            student.SecondName = sr.ReadLine();
            student.AverageMark = Double.Parse(sr.ReadLine());

            return student;
        }
    }
}
