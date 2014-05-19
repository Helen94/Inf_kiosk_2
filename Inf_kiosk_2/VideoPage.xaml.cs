using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для VideoPage.xaml
    /// </summary>
    public partial class VideoPage
    {
        public Uri VideoFile { get; private set; }

        public VideoPage()
        {
            InitializeComponent();
            Back.Click += Back_Click;
        }

        public VideoPage(string videoFile):this()
        {
            if (String.IsNullOrWhiteSpace(videoFile)) return;
            if (!File.Exists(videoFile)) return;

            VideoFile = new Uri(videoFile, UriKind.Relative);
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage.OpenPage(new MainPage(), this);
        }
        
        private void Video_MediaOpened(object sender, RoutedEventArgs e)
        {
          
            TimerSlider.Maximum = Video.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private bool _goPlayer;

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_goPlayer) return;

            //all ok
            mediaStoryboard.Storyboard.Seek(VPage, TimeSpan.FromSeconds(TimerSlider.Value), TimeSeekOrigin.BeginTime); 
        }



        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            var storyboardClock = (Clock)sender;

            if (storyboardClock.CurrentProgress == null) return;
            _goPlayer = true;
            if (storyboardClock.CurrentTime != null)
                TimerSlider.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
            _goPlayer = false;
        }
       
    }
}
