using System;
using System.Threading;

namespace Lab2_Nechaev_Planes
{
	public class Plane
	{
		public int Height{get; private set;}
		public int Position{get; private set;}
		public double Speed{get; private set;}
		public string NowPoint{get; private set;}
		
		public string Id{get; set;}
		
		public Flight Flight{get; set;}
		
		public int TimeFromStart{get; private set;}
		
		private Thread flightThread;
		private int lastTime;

        public bool IsCrashed { get; private set; }

		public string Route
		{
			get
			{
				return Flight.From + " ---> " + Flight.To;
			}
		}
		
		
		public Plane (string filename = "default.txt")
		{
			Flight = new Flight(filename);
		}
		
		public void Start()
		{
			TimeFromStart = 0;
			lastTime = 0;
			flightThread = new Thread(FlightProcess);
			flightThread.Start();
		}

        public void Crash()
        {
            IsCrashed = true;
        }
		
		public void FlightProcess()
		{
			while(true)
			{
				TimeFromStart++;
                if (!UpdatePosition())
                    break;
				Thread.Sleep(1000);
			}
		}
		
		public bool UpdatePosition()
		{
            if (IsCrashed)
                return false;

			foreach(FlightItem fi in Flight.Program)
				if(fi.Time == TimeFromStart)
				{
					int deltaPos = fi.Pos - Position;
					int deltaTime = fi.Time - lastTime;
				
					Position = fi.Pos;
					Height = fi.Height;
				
					NowPoint = fi.Caption;
				
					lastTime = fi.Time;
					
					Speed = (double)deltaPos / deltaTime;

                    if (lastTime == Flight.LastPoint)
                        return false;

					break;
				}
            return true;
		}
	}
}

