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

          DishFragment dishFragment =(DishFragment)ChildFragmentManager.FindFragmentById(Resource.Id.dishes);
          CheckButtonFragment checkBoxFragment = (CheckButtonFragment)ChildFragmentManager.FindFragmentById(Resource.Id.checksfragment);
          Choice = checkBoxFragment.GetChoice();
          Choice.SelectedDish = dishFragment.GetDish();

          result.Text = CheckChoise();
        }

        private string CheckChoise()
        {
            string res = String.Empty;
            if (Choice.SelectedManyfacturers.Count != 0 && Choice.SelectedPriceRanges.Count != 0)
            {
                res += $"Dish:{Choice.SelectedDish}\n" +
                    $"Manufacturers:{GetResultFromCheckBoxGroup(ref Choice.SelectedManyfacturers)}\n" +
                    $"Price ranges:{GetResultFromCheckBoxGroup(ref Choice.SelectedPriceRanges)} \n";
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