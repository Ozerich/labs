using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Nechaev_Students
{
    class Program
    {
        static void Main(string[] args)
        {
            Student[] students = {
                                     new Student("111111", "Vasya", "Petrov", 5.7), 
                                     new Student("222222", "Vanya", "Ivanov", 6.4),
                                     new Student("333333", "Oleg", "Zubarev", 8.6),
                                     new Student("444444", "Vasya", "Vovochkin", 3.4),
                                     new Student("555555", "Mark", "Bezfamilniy", 9),
                                     new Student("666666", "Nikita", "Tolsty", 1),
                                 };

            Faculty faculty = new Faculty("FKSIS");

            Group group1 = new Group("052004");
            group1.AddStudent(students[0]);
            group1.AddStudent(students[1]);
            group1.AddStudent(students[3]);

            Group group2 = new Group("052001");
            group2.AddStudent(students[2]);
            group2.AddStudent(students[4]);
            group2.AddStudent(students[5]);

            faculty.AddGroup(group1);
            faculty.AddGroup(group2);

            Console.WriteLine(faculty);

            Console.WriteLine("\nWriting to text file data.txt");
            Storage.SaveToFile(faculty, "data.txt", new TextFormatter());

            Console.WriteLine("Reading from text file data.txt\n");
            faculty = Storage.LoadFromFile("data.txt", new TextFormatter());

            Console.WriteLine(faculty);


            Console.WriteLine("\nWriting to binary file data.bin");
            Storage.SaveToFile(faculty, "data.txt", new TextFormatter());

            Console.WriteLine("Reading from binarytext file data.bin\n");
            faculty = Storage.LoadFromFile("data.txt", new TextFormatter());

            Console.WriteLine(faculty);

            Console.WriteLine(String.Format("Student's count: {0}\n", faculty.GetStudentsCount()));
            Console.WriteLine(String.Format("Students with average mark above 5: {0}", faculty.GetStudentCountWhereMark(5, true)));
            Console.WriteLine(String.Format("Students with average mark below 5: {0}", faculty.GetStudentCountWhereMark(5, false)));
            Console.WriteLine(String.Format("\nAverage mark: {0:0.00}", faculty.GetAverageMark()));


            Console.Read();
        }
    }
}
