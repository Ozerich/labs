
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_Ozierski_Phone
{
    public class Program
    {
        static void Main(string[] args)
        {
            PhoneCreator creator = new PhoneCreator();

            PhoneBook phones = new PhoneBook();

            phones.Add(creator.CreatePhone("0296704790", "Ivan Petrov"));
            phones.Add(creator.CreatePhone("0447235235", "Petya Sidorov"));
            phones.Add(creator.CreatePhone("0252435345", "Borzenkov"));
            phones.Add(creator.CreatePhone("0446423424", "Student Bezfamilniy"));

            Console.WriteLine("Text format: \n");

            phones.SaveToTextfile("data.txt");

            foreach (Phone phone in phones)
                Console.WriteLine(phone.ToString());

            phones.LoadFromTextfile("data.txt");

            Console.WriteLine("\nBinary format: \n");

            phones.SaveToBinaryFile("data.bin");

            foreach (Phone phone in phones)
                Console.WriteLine(phone.ToString());
            
            phones.LoadFromBinaryFile("data.bin");

            Console.WriteLine("\nXML format: \n");

            phones.SaveToXmlFile("data.xml");

            foreach (Phone phone in phones)
                Console.WriteLine(phone.ToString());

            phones.LoadFromXmlFile("data.xml");
            
            Console.Read();
        }
    }
}
