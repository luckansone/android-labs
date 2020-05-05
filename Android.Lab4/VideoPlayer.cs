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
using Android.Content;

namespace Lab4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class VideoPlayer: AppCompatActivity
    {
        private VideoView videoPlayer;
        private Button startButton, stopButton, pauseButton, chooseButton1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_video);

            startButton = FindViewById<Button>(Resource.Id.play);
            stopButton = FindViewById<Button>(Resource.Id.stop);
            pauseButton = FindViewById<Button>(Resource.Id.pause);
            chooseButton1 = FindViewById<Button>(Resource.Id.choose);
            videoPlayer = (VideoView)FindViewById(Resource.Id.videoPlayer);

            startButton.Enabled = false;
            stopButton.Enabled = false;
            pauseButton.Enabled = false;
            
            startButton.Click += StartButton_Click;
            stopButton.Click += StopButton_Click;
            pauseButton.Click += PauseButton_Click;
            chooseButton1.Click += ChooseButton1_Click;
        }



        private void ChooseButton1_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionGetContent);
            intent.SetType("video/mp4");
            intent.AddCategory(Intent.CategoryOpenable);
            StartActivityForResult(intent, 1);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                try
                {
                    var videoPath = data.Data;
                    videoPlayer.SetVideoURI(videoPath);
                    MediaController mediaController = new MediaController(this);
                    mediaController.SetAnchorView(videoPlayer);
                    videoPlayer.SetMediaController(mediaController);
                    startButton.Enabled = true;
                }
                catch
                {
                    startButton.Enabled = false;
                    stopButton.Enabled = false;
                    pauseButton.Enabled = false;
                    Toast.MakeText(this, "You don't choose video to play.", ToastLength.Short).Show();
                }
            }
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
            videoPlayer.RequestFocus();
            videoPlayer.Start();
            startButton.Enabled = false;
            pauseButton.Enabled = true;
            stopButton.Enabled = true;
        }
    }
}