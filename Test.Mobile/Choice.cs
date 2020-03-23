using System;
using System.Collections.Generic;

namespace Test.Mobile
{

    class Choice
    {
        public List<string> SelectedManyfacturers;
        public List<string> SelectedPriceRanges;
        public string SelectedDish { get; set; }

        public Choice()
        {
            SelectedManyfacturers = new List<string>();
            SelectedPriceRanges = new List<string>();
            SelectedDish = String.Empty;
        }
    }

}