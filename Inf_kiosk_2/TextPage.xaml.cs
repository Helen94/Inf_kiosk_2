using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для TextPage.xaml
    /// </summary>
    public partial class TextPage
    {
        public TextPage()
        {
            InitializeComponent();

            TextRange tr = new TextRange(
            RichTextBox1.Document.ContentStart, RichTextBox1.Document.ContentEnd);

            using (FileStream fs = File.Open("1.rtf", FileMode.Open))
            {
                tr.Load(fs, DataFormats.Rtf);
            }

            Back.Click += Back_Click;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

       
    }
}
