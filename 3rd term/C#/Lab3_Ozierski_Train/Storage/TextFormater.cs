using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage
{
    public class TextFormater : BaseFormater
    {
        private StreamWriter sw;
        private StreamReader sr;
        public override void WriteTrain(Train train, Stream stream)
        {
            sw = new StreamWriter(stream);
            sw.WriteLine(train.Number);
            sw.Write(train.Coaches.Count);
            foreach (Coach coach in train)
                WriteCoach(coach);
            sw.Close();
        }

        protected override void WriteCoach(Coach coach)
        {
            sw.Write(coach.Type);
            sw.Write(coach.Rooms.Count);
            foreach (Room room in coach)
                WriteRoom(room);
        }

        protected override void WriteRoom(Room room)
        {
            sw.Write(room.PassengerLimit);
            sw.Write(room.PassengerCount);
        }


        public override Train ReadTrain(Stream stream)
        {
            sr = new StreamReader(stream);
            Train train = new Train(Int32.Parse(sr.ReadLine()));
            for (int i = 0; i < sr.Read(); i++)
                train.AddCoach(ReadCoach());
            return train;
        }

        protected override Coach ReadCoach()
        {
            Coach coach = new Coach(sr.Read(), (CoachType)sr.Read());
            for (int i = 0; i < sr.Read(); i++)
                coach.AddRoom(ReadRoom());
            return coach;
        }

        protected override Room ReadRoom()
        {
            Room room = new Room(sr.Read());
            room.PassengerCount = sr.Read();
            return room;
        }
    }
}
