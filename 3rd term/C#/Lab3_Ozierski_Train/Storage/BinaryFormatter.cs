using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab2_Ozierski_Train.Storage
{
    class BinaryFormatter : BaseFormater
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
            sw.Write(coach.Id);
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
            Train train = new Train(sr.ReadInt32());
            int count = sr.ReadInt32();
            for (int i = 0; i < count; i++)
                train.AddCoach(ReadCoach());
            return train;
        }

        protected override Coach ReadCoach()
        {
            Coach coach = new Coach(sr.ReadInt32(), (CoachType)sr.ReadByte());
            int count = sr.ReadInt32();
            for (int i = 0; i < count; i++)
                coach.AddRoom(ReadRoom());
            return coach;
        }

        protected override Room ReadRoom()
        {
            Room room = new Room(sr.ReadInt32(), sr.ReadInt32());
            return room;
        }
    }
}
