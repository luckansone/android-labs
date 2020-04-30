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
    public class AuthorFragment : Fragment
    {
        private Spinner Spinner;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.author_fragment, container, false);

            Spinner = view.FindViewById<Spinner>(Resource.Id.spinner1);
            var adapter = ArrayAdapter.CreateFromResource(view.Context, Resource.Array.Authors, Android.Resource.Layout.SimpleSpinnerItem);
            
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerItem);
            Spinner.Adapter = adapter;


            return view;
        }

        public string GetAuthor()
        {
            return Spinner.SelectedItem.ToString();
        }
    }
}