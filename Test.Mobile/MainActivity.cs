using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Views;

namespace Test.Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<string> Dishes { get; set; }
        List<string> Manufacturers { get; set; }
        List<string> PriceRange { get; set; }
        Choice Choice { get; set; }

        LinearLayout linearLayout1;
        LinearLayout linearLayout2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitializeData();

            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);

            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, Dishes);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            linearLayout1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            linearLayout2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            DataDisplay(ref linearLayout1, Manufacturers);
            DataDisplay(ref linearLayout2, PriceRange);

            Choice = new Choice();

            var button = FindViewById<Button>(Resource.Id.button1);

            button.Click += Button_Click;
     

        }

     

        private void Button_Click(object sender, EventArgs e)
        {
            string result = String.Empty;

            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = alertDialog.Create();

            if (Choice.SelectedManyfacturers.Count == 0 || Choice.SelectedPriceRanges.Count == 0)
            {
                alert.SetTitle("Error");
                alert.SetMessage("Enter all data");
            }
            else
            {
                result += $"Dish:{Choice.SelectedDish}," +
                    $" Manufacturers: {GetResultFromCheckBoxGroup(ref Choice.SelectedManyfacturers)}," +
                    $" Price ranges: {GetResultFromCheckBoxGroup(ref Choice.SelectedPriceRanges)} ";
                    alert.SetTitle("Your choice");
                     alert.SetMessage(result);
               
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

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            Choice.SelectedDish =  spinner.GetItemAtPosition(e.Position).ToString();
        }

        void InitializeData()
        {
            Dishes = new List<string>
            {
                "Cups",
                "Spoons",
                "Forks"
            };

            Manufacturers = new List<string>
            {
                "Gold age",
                "Sweet home",
                "Good times"
            };

            PriceRange = new List<string>
            {
                "400-500",
                "500-600",
                "600-700"
            };

        }
    }
}