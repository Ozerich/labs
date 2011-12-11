using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab6_Player
{
    public class Program
    {

        static void Main(string[] args)
        {
           Track[] tracks = { new Track(1, "Song 1", 500, "Author 1", 5),
                                 new Track(2, "Song 2", 300),
                                 new Track(3, "Song 3", 400, "Author 3", 1)};

            PlayList pl = PlayList.Create("Test_PlayList");

            foreach (Track tr in tracks)
                pl.Add(tr);

            pl.Save();

            Application app = new Application();
        }
    }
}
