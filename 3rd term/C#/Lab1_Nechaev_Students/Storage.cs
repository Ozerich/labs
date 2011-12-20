using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1_Nechaev_Students
{
    public class Storage
    {
        public static void SaveToFile(Faculty faculty, string filename, BaseFormatter formatter)
        {
            Stream fs = new FileStream(filename, FileMode.Create);

            formatter.WriteFaculty(faculty, fs);

            fs.Close();
        }

        public static Faculty LoadFromFile(string filename, BaseFormatter formatter)
        {
            Stream fs = new FileStream(filename, FileMode.Open);

            Faculty faculty = formatter.ReadFaculty(fs);

            fs.Close();
            return faculty;
        }
    }
}
