using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Lab6_Player
{
    public class Application
    {
        private List<PlayList> playlists = new List<PlayList>();
        private Track currentTrack;
        private PlayList currentPlayList;
        private Thread thread;
        private object screenLock = new Object();

        public Application()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "\\playlists\\";
            foreach (FileInfo file in new DirectoryInfo(path).GetFiles("*.pl"))
                playlists.Add(PlayList.Load(file.Name.Split('.')[0]));

            if (playlists.Count > 0)
                currentPlayList = playlists[0];
            if (currentPlayList.Count > 0)
                currentTrack = currentPlayList[0];


            thread = new Thread(new ThreadStart(Process));
            thread.Start();

            currentTrack.Play();
        }

        private void Process()
        {
            while (true)
            {
                lock (screenLock)
                    UpdateData();
                Thread.Sleep(100);
            }
        }

        private void UpdateData()
        {
            Screen.Write(0, 0, "Songs: ");

            int line = 1;
            foreach (Track t in currentPlayList)
            {
                string s = (t.Playing ? "*" : "") +  t.Id + ". " + t.Name + " (" + t.Artist + ") " + " :    " + FormatTime(t.Position) + " / " + FormatTime(t.Length);
                Screen.Write(0, line++, s);
            }
        }

        private string FormatTime(int time)
        {
            return String.Format("{0}:{1:00}", time / 60, time % 60);
        }
                    
    }
}
