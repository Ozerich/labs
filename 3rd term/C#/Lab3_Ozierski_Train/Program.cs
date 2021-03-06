﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lab2_Ozierski_Train.Storage;
using System.Security.Cryptography;
using System.Threading;
using Lab2_Ozierski_Train.XML;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lab2_Ozierski_Train.Serialization;

namespace Lab2_Ozierski_Train
{
    public class Program
    {
        static void Main(string[] args)
        {
          //  FileWatcher.Init(".", "*.*");

            Train train = new Train(5);

            train.AddCoach(new Coach(1, CoachType.Coupe));
            train.AddCoach(new Coach(2, CoachType.Coupe));

            train.AddRoom(1, new Room(4, 2));
            train.AddRoom(1, new Room(4, 1));
            train.AddRoom(2, new Room(2, 2));
            train.AddRoom(2, new Room(2));


            Rijndael rijndael = Rijndael.Create();

            FileStorage storage = new FileStorage();

            storage.AddFilter(new Storage.Filters.CryptFilter(rijndael.IV, rijndael.Key));
            storage.AddFilter(new Storage.Filters.ZipFilter());

            Console.WriteLine("Before writing text file: \n");
            Console.WriteLine(train);

            storage.WriteTrain("train.txt", train, new TextFormatter());
            train = storage.ReadTrain("train.txt", new TextFormatter());

            Console.WriteLine("After reading text file: \n");
            Console.WriteLine(train);

            Console.WriteLine("Before writing binary file: \n");
            Console.WriteLine(train);

            storage.WriteTrain("train.dat", train, new BinaryFormatter());
            train = storage.ReadTrain("train.dat", new BinaryFormatter());

            Console.WriteLine("After reading binary file: \n");
            Console.WriteLine(train);

            Thread.Sleep(200);

            Console.WriteLine("\nDelete train.txt...");
            File.Delete("train.txt");

            Thread.Sleep(200);

            Console.WriteLine("\nRename train.dat...");
            try
            {
                File.Move("train.dat", "train_renamed.dat");
            }
            catch (IOException e)
            {
                File.Delete("train_renamed.dat");
                Thread.Sleep(200);
                File.Move("train.dat", "train_renamed.dat");
            }

            DomXml domxml = new DomXml();
            XmlDocument xml = domxml.SaveTrain(train);
            XDocument doc = XDocument.Parse(xml.OuterXml);
            TextWriter writer = new StreamWriter("train_dom.xml");
            writer.Write(doc.ToString());
            writer.Close();

            LinqXml linqxml = new LinqXml();
            doc = linqxml.SaveTrain(train);
            writer = new StreamWriter("train_linq.xml");
            writer.Write(doc.ToString());
            writer.Close();

            BinarySerialization bs = new BinarySerialization();
            FileStream fs = new FileStream("binary.dat", FileMode.Create);
            bs.Serialize(fs, train);
            fs.Close();

            fs = new FileStream("binary.dat", FileMode.Open);
            Console.WriteLine("\nBinary deserializaton:");
            Console.WriteLine(bs.Deserialize(fs));
            fs.Close();


            XmlSerialization xs = new XmlSerialization();
            fs = new FileStream("xml_s.xml", FileMode.Create);
            xs.Serialize(fs, train);
            fs.Close();

            fs = new FileStream("xml_s.xml", FileMode.Open);
            Console.WriteLine("\nXML deserializaton:");
            Console.WriteLine(xs.Deserialize(fs));
            fs.Close();


            Console.ReadLine();
        }
    }
}
