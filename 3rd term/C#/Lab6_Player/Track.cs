using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab6_Player
{
    public enum TrackGenre
    {
        Shanson = 1,
        Pop = 2,
        Rap = 3,
        Rock = 4,
        Classic = 5
    }

    public enum TrackStatus
    {
        Playing = 1,
        Stopped = 2,
        Paused = 3
    }

    [Serializable]
    public class Track : IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public TrackGenre Genre { get; set; }
        public int Rating { get; set; }

        public int Position { get; set; }
        public int Length { get; set; }

        private TrackStatus status;

        public bool Playing
        {
            get
            {
                return status == TrackStatus.Playing;
            }
            private set { }
        }

        [NonSerialized]
        private Thread thread;

        public Track()
        {
            status = TrackStatus.Stopped;
            Position = 0;
        }

        private void Process()
        {
            while (true)
            {
                if (Position >= Length)
                    Pause();
                else if (status == TrackStatus.Playing)
                    Position++;
                Thread.Sleep(150);
            }
        }

        public Track(int id, string name, int length, string artist = "Unknown", int rating = 0)
            : this()
        {
            Id = id;
            Name = name;
            Length = length;
            Artist = artist;
            Rating = rating;

            thread = new Thread(new ThreadStart(Process));
            thread.Start();
        }

        public void Play()
        {
            status = TrackStatus.Playing;
        }

        public void Pause()
        {
            status = TrackStatus.Paused;
        }

        public void Stop()
        {
            status = TrackStatus.Stopped;
            Position = 0;
        }

        public void Dispose()
        {
            Stop();
            thread.Join();
        }
    }
}
