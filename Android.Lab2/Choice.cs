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
        public List<string> SelectedYears;
   
        public string SelectedAuthor { get; set; }

        public Choice()
        {
            SelectedYears = new List<string>();
            SelectedAuthor = String.Empty;
        }
    }
}