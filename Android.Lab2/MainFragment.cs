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
    public class MainFragment : Fragment, ButtonListener
    {
      private TextView result;
      private Choice Choice { get; set; }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_main, container, true);
            result = view.FindViewById<TextView>(Resource.Id.textView1);
            Choice = new Choice();
            return view;
        }
        public void OkClick()
        {

          AuthorFragment authorFragment =(AuthorFragment)ChildFragmentManager.FindFragmentById(Resource.Id.authors);
          CheckButtonFragment checkBoxFragment = (CheckButtonFragment)ChildFragmentManager.FindFragmentById(Resource.Id.checksfragment);
          Choice = checkBoxFragment.GetChoice();
          Choice.SelectedAuthor = authorFragment.GetAuthor();

          result.Text = CheckChoise();
        }

        private string CheckChoise()
        {
            string res = String.Empty;
            if (Choice.SelectedYears.Count != 0)
            {
                res += $"Author:{Choice.SelectedAuthor}\n" +
                    $"Years of publication:{GetResultFromCheckBoxGroup(ref Choice.SelectedYears)}\n";
            }
            else
            {
                res = "Enter all data.";
            }
            return res;
        }

        private string GetResultFromCheckBoxGroup(ref List<string> SelectedItems)
        {
            string result = String.Empty;

            foreach (var el in SelectedItems)
            {
                result += $"{el}, ";
            }

            return result.Remove(result.Length-2);
        }


    }
}