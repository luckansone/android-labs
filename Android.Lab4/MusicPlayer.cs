using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Lab4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MusicPlayer: AppCompatActivity
    {
        private List<int> audioId = new List<int> { Resource.Raw.MozartMusic, Resource.Raw.BeethovenMusic, Resource.Raw.StraussMusic};
        private List<string> audioName;
        private Dictionary<string, int> audioDict = new Dictionary<string, int>();
        private string LastPlayVideo { get; set; }

        private MediaPlayer mPlayer;
        private Button playButton, pauseButton, stopButton;
        private Spinner spinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_music);

            LastPlayVideo = String.Empty;

            audioName = Resources.GetStringArray(Resource.Array.Name).ToList();

            InitializeSpinner();

            playButton = FindViewById<Button>(Resource.Id.play1);
            pauseButton = FindViewById<Button>(Resource.Id.pause1);
            stopButton = FindViewById<Button>(Resource.Id.stop1);

            pauseButton.Enabled = false;
            stopButton.Enabled = false;

            playButton.Click += PlayButton_Click;
            stopButton.Click += StopButton_Click;
            pauseButton.Click += PauseButton_Click;
            spinner.ItemSelected += Spinner_ItemSelected;
          
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            playButton.Enabled = true;
        }

        private void InitializeSpinner()
        {
            for (int i = 0; i < audioName.Count; i++)
            {
                audioDict.Add(audioName[i], audioId[i]);
            }

            spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Name, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void InitializeAudioPlayer(string songName)
        {
            mPlayer = MediaPlayer.Create(this, audioDict[songName]);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            mPlayer.Pause();
            playButton.Enabled = true;
            pauseButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopPlay();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            string audioName = spinner.SelectedItem.ToString();

            if (!audioName.Equals(LastPlayVideo) && !LastPlayVideo.Equals(String.Empty))
            {
                StopPlay();
                
            }

            InitializeAudioPlayer(audioName);
            mPlayer.Start();
            LastPlayVideo = audioName;
            playButton.Enabled = false;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
        }

        private void StopPlay()
        {
            mPlayer.Stop();
            pauseButton.Enabled = false;
            stopButton.Enabled = false;
            try
            {
                mPlayer.Prepare();
                mPlayer.SeekTo(0);
                playButton.Enabled = true;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}