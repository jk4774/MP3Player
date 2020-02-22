﻿using MP3Player.ViewModels;
using NAudio.Wave;
using System;
using System.Timers;
//using System.Windows.Forms;

namespace MP3Player.Models
{
    public class Song : BaseViewModel
    {
        private float volume;
        private double positionMax;
        private string timeText;
        private double positionValue;

        public Timer Timer { get; set; }
        public double PositionMax
        {
            get { return positionMax; }
            set
            {
                positionMax = value;
                OnPropertyChanged("PositionMax");
            }
        }
        public string TimeText
        {
            get { return timeText; }
            set
            {
                timeText = value;
                OnPropertyChanged("TimeText");
            }
        }
        public double PositionValue
        {
            get { return positionValue; }
            set
            {
                positionValue = value;
                ChangePosition();
                OnPropertyChanged("PositionValue");
            }
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsPausing { get; set; }
        public AudioFileReader MP3 { get; set; }
        public float Volume
        {
            get { return volume; }
            set
            {
                volume = float.Parse(value.ToString("0"));
                if (MP3 != null)
                    MP3.Volume = volume / 100;
                OnPropertyChanged("Volume");
            }
        }

        internal void ChangePosition() =>
            MP3.CurrentTime = TimeSpan.FromSeconds(PositionValue);

        internal void CountTime(EventHandler eventHandler)
        {
            Timer.Elapsed += new ElapsedEventHandler(eventHandler);
            Timer.Interval = 1000;
            Timer.Start();
            
            //System.Timers.Timer a = new System.Timers.Timer();

            //a.Interval = 1000;
            //a.Start();

            

            //Timer.Tick += new EventHandler(eventHandler);
            //Timer.Interval = 1000;
            //Timer.Start();
        }

        public Song(string path, float volume = 0f)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                MP3 = new AudioFileReader(path) { Volume = Volume };
                Name = System.IO.Path.GetFileName(path);
            }
            Path = path;
            Volume = volume;
            IsPlaying = false;
            IsPausing = false;
            Timer = new Timer();
        }
    }
}