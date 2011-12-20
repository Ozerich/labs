using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1_Nechaev_Students
{
    public class BinaryFormatter : BaseFormatter
    {
        private BinaryWriter bw;
        private BinaryReader br;

        public override void WriteFaculty(Faculty faculty, Stream st)
        {
            bw = new BinaryWriter(st);

            bw.Write(faculty.Name);
            bw.Write(faculty.Groups.Count);

            foreach (Group group in faculty.Groups)
                WriteGroup(group);

            bw.Close();
        }

        protected override void WriteGroup(Group group)
        {
            bw.Write(group.Number);
            bw.Write(group.Students.Count);

            foreach (Student st in group.Students)
                WriteStudent(st);

        }

        protected override void WriteStudent(Student student)
        {
            bw.Write(student.StudentId);
            bw.Write(student.FirstName);
            bw.Write(student.SecondName);
            bw.Write(student.AverageMark);
        }

        public override Faculty ReadFaculty(Stream st)
        {
            Faculty faculty = new Faculty();
            br = new BinaryReader(st);

            for (int i = 0; i < br.ReadInt32(); i++)
                faculty.Groups.Add(ReadGroup());

            br.Close();
            return faculty;
        }

        protected override Group ReadGroup()
        {
            Group group = new Group();

            group.Number = br.ReadString() ;

            for (int i = 0; i < br.ReadInt32(); i++)
                group.Students.Add(ReadStudent());

            return group;
        }

        protected override Student ReadStudent()
        {
            Student student = new Student();

            student.StudentId = br.ReadString();
            student.FirstName = br.ReadString();
            student.SecondName = br.ReadString();
            student.AverageMark = br.ReadDouble();

            return student;
        }
    }
}
