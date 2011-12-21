using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace Lab2_Nechaev_Planes
{
	public class Application
	{
		private List<Plane> planes;
		
		private Thread processThread;
		private object screenLock = new object();
		
		private int activeIndex = 0;
		
		public void AddPlane(Plane plane)
		{
			planes.Add(plane);
		}
		
		public Application ()
		{
			planes = new List<Plane>();
		}
		
		public void Start()
		{
			processThread = new Thread(() =>
			{
				while(true)
				{
					lock(screenLock)
						UpdateData();
					Thread.Sleep(100);
				}
			});
			processThread.Start();
		}
		
		public void UpdateData()
		{
			Screen.Write(0, 0, "Planes monitoring:");
			
			for(int i = 0; i < planes.Count; i++)
			{
				Plane plane = planes[i];

                StringBuilder planeStr = new StringBuilder();
                for (int j = 0; j < 100; j++)
                    planeStr.Append(' ');
                Screen.Write(0, i + 1, planeStr.ToString());

				planeStr = new StringBuilder((i == activeIndex) ? ">" : " ");
				
				planeStr.Append(plane.Id);
				planeStr.Append(" " + plane.Route);
				planeStr.Append(" in flight " + FormatTime(plane.TimeFromStart));
				planeStr.Append(". " + plane.Position + "km");
				planeStr.Append(", " + plane.Height + "km height");
				planeStr.Append("; now in " + plane.NowPoint);
				planeStr.Append("; Speed " + String.Format("{0:0.00} km/sec", plane.Speed));
				
				Screen.Write(0, i + 1, planeStr.ToString());
			}
            Screen.Write(0, 0, "Press [SPACE] for start flight or [Q] for crash plane:");
		}
		
		private string FormatTime(int time)
        {
            return String.Format("{0}:{1:00}", time / 60, time % 60);
        }

        public void HandleKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    activeIndex = (activeIndex == 0) ? planes.Count - 1 : activeIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    activeIndex = (activeIndex == planes.Count - 1) ? 0 : activeIndex + 1;
                    break;

                case ConsoleKey.C:
                    planes[activeIndex].Crash();
                    break;

                case ConsoleKey.Spacebar:
                    planes[activeIndex].Start();
                    break;
            }
        }
	}
}

