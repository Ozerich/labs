using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage
{
    public class TextFormatter : BaseFormater
    {
        private StreamWriter sw;
        private StreamReader sr;
        public override void WriteTrain(Train train, Stream stream)
        {
            sw = new StreamWriter(stream);
            sw.WriteLine(train.Number);
            sw.WriteLine(train.Coaches.Count);
            foreach (Coach coach in train)
                WriteCoach(coach);
            sw.Close();
        }

        protected override void WriteCoach(Coach coach)
        {
            sw.WriteLine(coach.Id);
            sw.WriteLine((int)coach.Type);
            sw.WriteLine(coach.Rooms.Count());
            foreach (Room room in coach)
                WriteRoom(room);
        }

        protected override void WriteRoom(Room room)
        {
            sw.WriteLine(room.PassengerLimit);
            sw.WriteLine(room.PassengerCount);
        }


        public override Train ReadTrain(Stream stream)
        {
            sr = new StreamReader(stream);
            Train train = new Train(Int32.Parse(sr.ReadLine()));
            int count = Int32.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
                train.AddCoach(ReadCoach());
            return train;
        }

        protected override Coach ReadCoach()
        {
            Coach coach = new Coach(Int32.Parse(sr.ReadLine()), (CoachType)Int32.Parse(sr.ReadLine()));
            int count = Int32.Parse(sr.ReadLine());
            for (int i = 0; i < count; i++)
                coach.AddRoom(ReadRoom());
            return coach;
        }

        protected override Room ReadRoom()
        {
            Room room = new Room(Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine()));
            return room;
        }
    }
}
