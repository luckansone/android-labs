using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Views;
using System.Linq;
using Android.Content;
using Test.Mobile.Resources.layout;

namespace Test.Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<string> Manufacturers { get; set; }
        List<string> PriceRange { get; set; }

        List<string> SelectedManufacturers;
        List<string> SelectedPriceRanges;
        Choice Choice { get; set; }

        LinearLayout linearLayout1;
        LinearLayout linearLayout2;

        Database database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SelectedManufacturers = new List<string>();
            SelectedPriceRanges = new List<string>();

            Manufacturers = Resources.GetStringArray(Resource.Array.Manufacturers).ToList();
            PriceRange = Resources.GetStringArray(Resource.Array.PriceRange).ToList();

            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Dishes, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            linearLayout2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            DataDisplay(ref linearLayout1, Manufacturers);
            DataDisplay(ref linearLayout2, PriceRange);

            Choice = new Choice();
            database = new Database();
            database.Create();

            var saveButton = FindViewById<Button>(Resource.Id.button1);

            saveButton.Click += Button_Click;

            var openButton = FindViewById<Button>(Resource.Id.button2);

            openButton.Click += OpenButton_Click;
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OutputActivity));
            StartActivity(intent);
        }

        private void Button_Click(object sender, EventArgs e)
        {
          
            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = alertDialog.Create();

            if (SelectedManufacturers.Count == 0 || SelectedPriceRanges.Count == 0)
            {
                alert.SetTitle("Error");
                alert.SetMessage("Enter all data");
            }
            else
            {
                Choice.SelectedManufacturers = GetResultFromCheckBoxGroup(ref SelectedManufacturers);
                Choice.SelectedPriceRanges = GetResultFromCheckBoxGroup(ref SelectedPriceRanges);
                bool result =database.Add(Choice);

                if (result)
                {
                    alert.SetMessage("Your choice is save.");
                }
                else
                {
                    alert.SetMessage("Your choice is not save.");
                }
            
                alert.SetTitle("Save");
            }
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Dispose();
            });
            alert.Show();
        }

        private string GetResultFromCheckBoxGroup(ref List<string> SelectedItems)
        {
            string result = String.Empty;
           
                foreach(var el in SelectedItems)
                {
                    result += $"{el} ";
                }

            return result;
        }

        void DataDisplay(ref LinearLayout  linearLayout, List<string> Items)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                var checkBox = new CheckBox(this);

                checkBox.Text =Items[i];
                checkBox.Id = i+ (new Random().Next(1,255));
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

                string test = checkbox.Id.ToString();
                string checkedName = checkbox.Text;

                if (linearLayout1.FindViewById(checkbox.Id) != null)
                {
                    CheckSelectedItems(ref SelectedManufacturers, checkedName, checkbox);
                }
                else
                {
                    CheckSelectedItems(ref SelectedPriceRanges, checkedName, checkbox);
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

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Choice.SelectedDish =  spinner.GetItemAtPosition(e.Position).ToString();
        }

    }
}