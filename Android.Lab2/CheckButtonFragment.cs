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
    public class CheckButtonFragment: Fragment
    {
        private LinearLayout linearLayout1;
        private LinearLayout linearLayout2;
        Choice Choice { get; set; }
        View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             view = inflater.Inflate(Resource.Layout.check_buttons_fragment, container, false);

            linearLayout1 = view.FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            linearLayout2 = view.FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            List<string> Manufacturers = Resources.GetStringArray(Resource.Array.Manufacturers).ToList();
            List<string> PriceRange = Resources.GetStringArray(Resource.Array.PriceRange).ToList();

            DataDisplay(ref linearLayout1,Manufacturers);
            DataDisplay(ref linearLayout2, PriceRange);

            Choice = new Choice();

            return view;
        }

        public Choice GetChoice()
        {
            return Choice;
        }

        void DataDisplay(ref LinearLayout linearLayout, List<string> Items)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                var checkBox = new CheckBox(view.Context);

                checkBox.Text = Items[i];
                checkBox.Id = i + (new Random().Next(1, 255));

                checkBox.LayoutParameters = new ViewGroup.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);

                checkBox.CheckedChange += CheckBox_CheckedChange;

                linearLayout.AddView(checkBox);
            }
        }

        private void CheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (sender is CheckBox)
            {
                var checkbox = (CheckBox)sender;

                string checkedName = checkbox.Text;

                if (linearLayout1.FindViewById(checkbox.Id) != null)
                {
                    CheckSelectedItems(ref Choice.SelectedManyfacturers, checkedName, checkbox);
                }
                else
                {
                    CheckSelectedItems(ref Choice.SelectedPriceRanges, checkedName, checkbox);
                }
            }
        }

        private void CheckSelectedItems(ref List<string> SelectedItem, string checkedName, CheckBox checkbox)
        {

            if (checkbox.Checked)
            {
                SelectedItem.Add(checkedName);
            }
            else
            {
                SelectedItem.Remove(checkedName);
            }
        }
    }

   

    
}