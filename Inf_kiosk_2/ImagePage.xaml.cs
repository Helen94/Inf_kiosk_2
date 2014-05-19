using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для ImagePage.xaml
    /// </summary>
    public partial class ImagePage
    {
        private readonly string[] _fileNames;
        private int _currentPosition;

        public ImagePage()
        {
            InitializeComponent();
            Back.Click += Back_Click;
        }
        
        public ImagePage(string[] fileNames, int startPosition):this()
        {
            if (!fileNames.Any()) return;
            foreach (string fileName in fileNames)
            {
                if (String.IsNullOrWhiteSpace(fileName)) return;
                if (!File.Exists(fileName)) return;
            }

            if (startPosition < 0) _currentPosition = 0;
            else if (startPosition >= fileNames.Length) _currentPosition = 0;
            else _currentPosition = startPosition;

            _fileNames = fileNames;

            DisplayImage();
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainPage.OpenPage(new MainPage(), this);
        }

        private void LeftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_currentPosition == 0) _currentPosition = _fileNames.Length - 1;
            else _currentPosition--;

            DisplayImage();
        }

        private void RightButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_currentPosition == (_fileNames.Length - 1)) _currentPosition = 0;
            else _currentPosition++;

            DisplayImage();
        }

        private void DisplayImage()
        {
            ImageBoard.Source = new BitmapImage(new Uri(Path.GetFullPath(_fileNames[_currentPosition])));
        }
    }
}
