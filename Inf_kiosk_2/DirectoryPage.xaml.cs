using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для DirectoryPage.xaml
    /// </summary>
    public partial class DirectoryPage
    {
        public DirectoryPage()
        {
            InitializeComponent();
            Back.Click +=Back_Click;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
