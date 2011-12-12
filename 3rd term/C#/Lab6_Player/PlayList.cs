using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Lab6_Player
{
    [Serializable]
    public class PlayList : List<Track>, IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static string GetFilePath(string name)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + "\\playlists\\" + name + ".pl";
        }

        public PlayList(string name)
        {
            Name = name;
        }

        public int Length
        {
            get
            {
                int result = 0;
                foreach (Track t in this)
                    result += t.Length;
                return result;
            }
            private set { }
        }

        public double Rating
        {
            get
            {
                int result = 0;
                foreach(Track t in this)
                    result += t.Rating;
                return result / this.Count;
            }
            private set { }
        }

        public void Dispose()
        {
            foreach (Track t in this)
                t.Dispose();
        }

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream f = new FileStream(GetFilePath(Name), FileMode.OpenOrCreate))
                bf.Serialize(f, this);
        }

        public static PlayList Load(string name)
        {
            PlayList result;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream f = new FileStream(GetFilePath(name), FileMode.OpenOrCreate))
                result = (PlayList)bf.Deserialize(f);
            return result;
        }

        public static PlayList Create(string name)
        {
            PlayList pl = new PlayList(name);
            pl.Save();
            return pl;
        }
    }
}
