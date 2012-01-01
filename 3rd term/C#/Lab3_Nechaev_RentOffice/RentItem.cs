using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3_Nechaev_RentOffice
{
    struct RentStatus
    {
        public DateTime startDate;
        public int daysCount;
        public User user;
    }

    public abstract class RentItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int DayPrice { get; set; }

        public bool InRent { get; set; }

        public RentStatus Status { get; set; }

        public RentItem(int _id)
        {
            Id = _id;
        }
    }
}
