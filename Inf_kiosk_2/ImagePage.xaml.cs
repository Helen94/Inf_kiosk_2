using System;
using System.Windows.Navigation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для ImagePage.xaml
    /// </summary>
    public partial class ImagePage
    {
        public ImagePage()
        {
            InitializeComponent();
            Back.Click += Back_Click;
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
        
    }
}
