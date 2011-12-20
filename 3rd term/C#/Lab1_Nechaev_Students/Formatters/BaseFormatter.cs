using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1_Nechaev_Students
{
    public abstract class BaseFormatter
    {
        public abstract void WriteFaculty(Faculty faculty, Stream st);
        protected abstract void WriteGroup(Group group);
        protected abstract void WriteStudent(Student student);

        public abstract Faculty ReadFaculty(Stream st);
        protected abstract Group ReadGroup();
        protected abstract Student ReadStudent();
    }
}
