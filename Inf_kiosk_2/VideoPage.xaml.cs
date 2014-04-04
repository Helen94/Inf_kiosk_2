using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для VideoPage.xaml
    /// </summary>
    public partial class VideoPage
    {
        public VideoPage()
        {
            InitializeComponent();
            Back.Click +=Back_Click;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
        
        private void Video_Loaded(object sender, RoutedEventArgs e)
        {
            //Video.Source = new Uri(@"C:\Users\Елена\Desktop\Inf_kiosk_2\Inf_kiosk_2\bin\Debug\1.avi");
            //Video.Play();

        }

        private void Video_MediaOpened(object sender, RoutedEventArgs e)
        {
          
            TimerSlider.Maximum = Video.NaturalDuration.TimeSpan.TotalSeconds;
        }


        private bool _goPlayer;

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_goPlayer) return;

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
