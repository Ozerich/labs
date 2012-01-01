using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3_Nechaev_RentOffice
{
    public class RentOffice : Singleton<RentOffice>
    {
        List<RentItem> items;
          
        private int lastRentId = 1;

        private RentItem FindRentItem(int id)
        {
            foreach (RentItem item in items)
                if (item.Id == id)
                    return item;

            return null;
        }

        public void DoRent(int id, int daysCount, User user)
        {
            RentItem item = FindRentItem(id);
            
            if (item.InRent)
                throw new Exception("This item is busy");

            item.InRent = true;

            user.ItemsCount++;
            item.Status = new RentStatus { startDate = DateTime.Now, user = user, daysCount = daysCount };
        }
    }
}
