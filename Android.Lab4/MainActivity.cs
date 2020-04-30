using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using Android.Net;
using Android.Views;
using Android.Content;

namespace Lab4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private RadioGroup RadioGroup;
        private Button button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            RadioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            button = FindViewById<Button>(Resource.Id.button1);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            int checkedItemId = RadioGroup.CheckedRadioButtonId;

            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            Intent intent;    

            switch (checkedRadioButton.Text)
            {
                case "Audio":
                {
                   intent = new Intent(this, typeof(MusicPlayer));
                   break;
                }

                case "Video":
                {
                    intent = new Intent(this, typeof(VideoPlayer));
                    break;
                }

                default:
                 {
                     Toast.MakeText(this, "Enter error data.", ToastLength.Short);
                     return;
                 }
            }

            StartActivity(intent);
        }
    }
}
