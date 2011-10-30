using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lab2_Ozierski_Train.Storage.Filters;


namespace Lab2_Ozierski_Train.Storage
{
    public class BaseStorage
    {
        private List<IFilter> filters;

        public BaseStorage()
        {
            filters = new List<IFilter>();
        }

        public void AddFilter(IFilter filter)
        {
            filters.Add(filter);
        }

        public void WriteTrain(Train train, Stream stream, BaseFormater formater)
        {
            foreach (IFilter filter in filters)
                stream = filter.Apply(stream, FilterMode.Write);
            formater.WriteTrain(train, stream);
        }

        public Train ReadTrain(Stream stream, BaseFormater formater)
        {
            foreach (IFilter filter in filters)
                stream = filter.Apply(stream, FilterMode.Read);
            return formater.ReadTrain(stream); 
        }
      
       
    }
}
