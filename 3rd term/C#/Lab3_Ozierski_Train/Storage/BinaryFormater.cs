using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage
{
    class BinaryFormater : BaseFormater
    {
        private BinaryWriter sw;
        private BinaryReader sr;
        public override void WriteTrain(Train train, Stream stream)
        {
            sw = new BinaryWriter(stream);
            sw.Write(train.Number);
            sw.Write(train.Coaches.Count);
            foreach (Coach coach in train)
                WriteCoach(coach);
            sw.Close();
        }

        protected override void WriteCoach(Coach coach)
        {
            sw.Write((byte)coach.Type);
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
            sr = new BinaryReader(stream);
            Train train = new Train(sr.Read());
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
