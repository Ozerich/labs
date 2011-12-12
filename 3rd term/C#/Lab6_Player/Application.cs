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
        private PlayList currentPlayList;
        private Thread thread;
        private object screenLock = new Object();
        private int activeIndex = 0;
        private Track currentTrack
        {
            get
            {
                return currentPlayList[activeIndex];
            }
            set { }
        }

        public Application()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "\\playlists\\";
            foreach (FileInfo file in new DirectoryInfo(path).GetFiles("*.pl"))
                playlists.Add(PlayList.Load(file.Name.Split('.')[0]));

            if (playlists.Count > 0)
                currentPlayList = playlists[0];

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
            int ind = 0;
            foreach (Track t in currentPlayList)
            {
                String s =(ind++ == activeIndex ? "> " : "  ") + t.Id + ". " + t.Name + " (" + t.Artist + ")\t" + " :  " + FormatTime(t.Position) + " / " + FormatTime(t.Length);
                double pos = ((double)t.Position / t.Length) * 30;
                StringBuilder outs = new StringBuilder(s);
                int i = 0;
                outs.Append(" |");

                for (; i < (int)pos; i++)
                    outs.Append("\u00BB");
                for (; i < 30; i++)
                    outs.Append(" ");
                outs.Append("|");
                
                Screen.Write(0, line++, outs.ToString());
            }
        }

        private string FormatTime(int time)
        {
            return String.Format("{0}:{1:00}", time / 60, time % 60);
        }

        public void HandleKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    activeIndex = (activeIndex == 0) ? playlists[0].Count - 1 : activeIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    activeIndex = (activeIndex == playlists[0].Count - 1) ? 0 : activeIndex + 1;
                    break;

                case ConsoleKey.Spacebar:
                    if (currentTrack.Playing)
                        currentTrack.Pause();
                    else
                        currentTrack.Play();
                    break;
            }
        }
    }
}
