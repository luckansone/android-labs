using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using Android.Net;
using Android.Views;
using System.Collections.Generic;
using System.Linq;
using System;
using Uri = Android.Net.Uri;

namespace Lab4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class VideoPlayer: AppCompatActivity
    {
        private List<int> videoId = new List<int> { Resource.Raw.Mozart, Resource.Raw.Beethoven, Resource.Raw.Strauss};
        private List<string> videoName;
        private Dictionary<string, int> videoDict = new Dictionary<string, int>();
        private string LastPlayVideo { get; set; }


        private Spinner spinner;
        private VideoView videoPlayer;
        private Button startButton, stopButton, pauseButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_video);

            startButton = FindViewById<Button>(Resource.Id.play);
            stopButton = FindViewById<Button>(Resource.Id.stop);
            pauseButton = FindViewById<Button>(Resource.Id.pause);

            LastPlayVideo = String.Empty;

            videoName = Resources.GetStringArray(Resource.Array.Name).ToList();

            InitializeSpinner();
            stopButton.Enabled = false;
            pauseButton.Enabled = false;


            startButton.Click += StartButton_Click;
            stopButton.Click += StopButton_Click;
            pauseButton.Click += PauseButton_Click;
        }

        private void InitializeSpinner()
        {
            for(int i = 0; i < videoName.Count; i++)
            {
                videoDict.Add(videoName[i], videoId[i]);
            }

            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.Name, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void InitializeVideoPlayer(string videoName)
        {
            videoPlayer = (VideoView)FindViewById(Resource.Id.videoPlayer);
            Android.Net.Uri videoUri = Uri.Parse("android.resource://" + Application.PackageName + "/" + videoDict[videoName]);
            videoPlayer.SetVideoURI(videoUri);
            MediaController mediaController = new MediaController(this);
            mediaController.SetAnchorView(videoPlayer);
            videoPlayer.SetMediaController(mediaController);
            spinner.ItemSelected += Spinner_ItemSelected;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            startButton.Enabled = true;
        }

        private void PauseButton_Click(object sender, System.EventArgs e)
        {
            videoPlayer.Pause();
            startButton.Enabled = true;
            stopButton.Enabled = true;
            pauseButton.Enabled = false;
        }

        private void StopButton_Click(object sender, System.EventArgs e)
        {
            videoPlayer.StopPlayback();
            videoPlayer.Resume();
            startButton.Enabled = true;
            stopButton.Enabled = false;
            pauseButton.Enabled = false;
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            string videoName = spinner.SelectedItem.ToString();

            if(!videoName.Equals(LastPlayVideo) && !LastPlayVideo.Equals(String.Empty))
            {
                videoPlayer.StopPlayback();
            }

            InitializeVideoPlayer(videoName);
            videoPlayer.RequestFocus();
            videoPlayer.Start();

            LastPlayVideo = videoName;

            startButton.Enabled = false;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
            
        }
    }
}