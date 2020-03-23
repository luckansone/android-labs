using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Android.Lab2
{
    public class Choice
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