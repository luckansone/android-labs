using SQLite;
using System;
using System.Collections.Generic;

namespace Test.Mobile
{

    public class Choice
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string SelectedManufacturers { get; set; }
        public string SelectedPriceRanges { get; set; }
        public string SelectedDish { get; set; }
    }

}