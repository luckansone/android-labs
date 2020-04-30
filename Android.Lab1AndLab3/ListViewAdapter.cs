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

namespace Test.Mobile
{
    public class ListViewAdapter : BaseAdapter
    {
        private Activity Activity;
        private List<Choice> Choices;

        public ListViewAdapter(Activity Activity, List<Choice> Choices)
        {
            this.Activity = Activity;
            this.Choices = Choices;
        }
        public override int Count
        {
            get { return Choices.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return Choices[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? Activity.LayoutInflater.Inflate(Resource.Layout.view_list, parent, false);
            var txtDish = view.FindViewById<TextView>(Resource.Id.textView_Dish);
            var txtManufacturer = view.FindViewById<TextView>(Resource.Id.textView_Manufacturer);
            var txtPrice = view.FindViewById<TextView>(Resource.Id.textView_Price);
            txtDish.Text =  $"Dish: {Choices[position].SelectedDish}";
            txtManufacturer.Text = $"Manufacturers: {Choices[position].SelectedManufacturers}";
            txtPrice.Text = $"Price ranges: {Choices[position].SelectedPriceRanges}";
            return view;
        }
    }
}