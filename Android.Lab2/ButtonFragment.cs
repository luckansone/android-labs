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
    
    class ButtonFragment : Fragment, View.IOnClickListener
    {
        private ButtonListener listener;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.button_fragment, container, false);

            var button = view.FindViewById<Button>(Resource.Id.buttonOk);
            button.SetOnClickListener(this);

            listener = (ButtonListener)ParentFragment;

            return view;
        }

        public void OnClick(View v)
        {
            listener.OkClick();
        }
    }
}