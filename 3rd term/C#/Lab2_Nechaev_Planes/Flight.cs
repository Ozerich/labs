using System;
using System.IO;
using System.Collections.Generic;

namespace Lab2_Nechaev_Planes
{
	public class FlightItem
	{
		public int Time{get; private set;}
		public int Pos{get; private set;}
		public int Height{get; private set;}
		public string Caption{get; private set;}
		
		public FlightItem(int time, int pos, int height, string caption)
		{
			Time = time;
			Pos = pos;
			Height = height;
			Caption = caption;
		}
	}
	
	public class Flight
	{
		public int Seconds{get; private set;}
		public string From{get; private set;}
		public string To{get; private set;}

        public int LastPoint { get; private set; }
		
		public List<FlightItem> Program{ get; private set; }
		
		public Flight (string filename)
		{
			Program = new List<FlightItem>();
			LoadFromFile(filename);
		}
		
		private void LoadFromFile(string filename)
		{
			if(File.Exists(filename) == false)
				throw new FileNotFoundException("No flight plan file");
			
			FileStream fs = new FileStream(filename, FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			
			string line;
			string[] data;
			
			line = sr.ReadLine();
			data = line.Split(' ');
			
			From = data[0];
			To = data[1];
			
			while((line = sr.ReadLine())!=null)
			{
				data = line.Split(' ');
				Program.Add(new FlightItem(Int32.Parse(data[0]), Int32.Parse(data[1]), Int32.Parse(data[2]), data[3]));
                LastPoint = Math.Max(LastPoint, Int32.Parse(data[0]));
			}
			
			sr.Close();
			fs.Close();
		}
	}
}

