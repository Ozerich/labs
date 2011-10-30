using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lab2_Ozierski_Train.Storage;

namespace Lab2_Ozierski_Train
{
    public class Program
    {
        static void Main(string[] args)
        {
            Train train = new Train(5);

            BaseStorage storage = new BaseStorage();
            storage.AddFilter(new Storage.Filters.ZipFilter());

            FileStream fs = new FileStream("data.txt", FileMode.Create);
            storage.WriteTrain(train, fs, new TextFormater());    
            fs.Close();

            fs = new FileStream("data.txt", FileMode.Open);
            train = storage.ReadTrain(fs, new TextFormater());
            fs.Close();
        }
    }
}
