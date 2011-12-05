using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lab2_Ozierski_Train.Storage.Filters;

namespace Lab2_Ozierski_Train.Storage
{
    public class FileStorage
    {

        private List<IFilter> filters;
        public Stream out_stream{get; set;}



        public FileStorage()
        {
            filters = new List<IFilter>();
        }

        public void AddFilter(IFilter filter)
        {
            filters.Add(filter);
        }


        public void WriteTrain(string filename, Train train, BaseFormater formater)
        {
            Stream fs = new FileStream(filename, FileMode.Create);
            foreach (IFilter filter in filters)
                fs = filter.Apply(fs, FilterMode.Write);
            formater.WriteTrain(train, fs);
            fs.Close();
        }

        public Train ReadTrain(string filename, BaseFormater formater)
        {
            Stream fs = new FileStream(filename, FileMode.Open);
            foreach (IFilter filter in filters)
                fs = filter.Apply(fs, FilterMode.Read);
            Train train = formater.ReadTrain(fs);
            fs.Close();
            return train;
        }

    }
}
