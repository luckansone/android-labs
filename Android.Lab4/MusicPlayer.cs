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
        private MediaPlayer mPlayer;
        private Button playButton, pauseButton, stopButton, chooseButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_music);

            playButton = FindViewById<Button>(Resource.Id.play1);
            pauseButton = FindViewById<Button>(Resource.Id.pause1);
            stopButton = FindViewById<Button>(Resource.Id.stop1);
            chooseButton = FindViewById<Button>(Resource.Id.choose3);

            pauseButton.Enabled = false;
            stopButton.Enabled = false;
            playButton.Enabled = false;

            playButton.Click += PlayButton_Click;
            stopButton.Click += StopButton_Click;
            pauseButton.Click += PauseButton_Click;
            chooseButton.Click += ChooseButton_Click;
        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionGetContent);
            intent.SetType("audio/*");
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
                    var audioPath = data.Data;
                    mPlayer = MediaPlayer.Create(this, audioPath);
                    playButton.Enabled = true;
                }
                catch
                {
                    playButton.Enabled = false;
                    stopButton.Enabled = false;
                    pauseButton.Enabled = false;
                    Toast.MakeText(this, "You don't choose audio to play.", ToastLength.Short).Show();
                }
            }
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
            
            mPlayer.Start();
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