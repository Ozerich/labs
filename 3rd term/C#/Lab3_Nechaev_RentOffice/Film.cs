using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3_Nechaev_RentOffice
{
    public enum FilmGenre
    {
        Comedy,
        Serial,
        Action
    }

    public class Film : RentItem
    {
        public int Length { get; set; }
        public FilmGenre Genre { get; set; }
    }
}
