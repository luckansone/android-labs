using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Test.Mobile.Resources.layout
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class OutputActivity : AppCompatActivity
    {
        ListView listView;
        TextView TextView;
        List<Choice> Choices = new List<Choice>();
        Database database;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.output_activity);

            TextView = FindViewById<TextView>(Resource.Id.textView7);
            listView = FindViewById<ListView>(Resource.Id.listView1);
            database = new Database();
            LoadData();
        }

        private void LoadData()
        {
            Choices = database.GetChoices();
            if (Choices.Count == 0)
            {
                TextView.Text = "There is no data in database.";
            }
            else
            {
                var adapter = new ListViewAdapter(this, Choices);
                listView.Adapter = adapter;
            }
            
        }
    }
}