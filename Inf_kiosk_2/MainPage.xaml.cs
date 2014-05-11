using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage
    {
        private readonly TableSize _tablesize = new TableSize();

        public string RootDir = @".\Kiosk_direc";
        public string CurrentDir = @".\Kiosk_direc";

        public bool IsRootDir
        {
            get { return RootDir == CurrentDir; }
        }

        public void StepUp()
        {
            if (IsRootDir)
            {
                throw new Exception("Нельзя подняться выше корневого каталога");
            }
            CurrentDir = Regex.Match(CurrentDir, @".*(?=\\.*$)").Value;
        }

        public Button Button;

        public MainPage()
        {
            InitializeComponent();
            InitializeContent();
            Back.Click += ButtonBack;
        }

        public void InitializeContent()
        {
            Title = "Информационный киоск";

            string cur = Directory.GetCurrentDirectory();

            string[] directories = Directory.GetDirectories(CurrentDir);

            DirectoryInfo directory = new DirectoryInfo(CurrentDir);
            FileInfo[] files = directory.GetFiles();

            var filtered = files.Select(f => f).Where(f => (f.Attributes & FileAttributes.Hidden) == 0);

            List<string> res = new List<string>();
            foreach (string f in directories)
            {
                res.Add(f);
            }
            foreach (var f in filtered)
            {
                res.Add(f.ToString());
            }

            directories = res.ToArray();

            int dirCount = directories.Length;
            int colCount;
            int rowCount;
            _tablesize.NumberCell(dirCount, out colCount, out rowCount);
            //if (!IsRootDir)
            //{
            //    colCount += 3;
            //}
            UniformGrid1.Rows = rowCount;
            UniformGrid1.Columns = colCount;

            UniformGrid1.Children.Clear();

            foreach (string s in directories)
            {


                Button = new Button { Content = Path.GetFileName(s) };

                FileInfo fi = new FileInfo(s);
                string ext = fi.Extension;
                UniformGrid1.Children.Add(Button);
                if (ext == ".txt")
                {
                    Button.Click += ButtonText_Click;
                }
                else if (ext == ".rtf")
                {
                    Button.Click += ButtonText_Click;
                }
                else if (ext == ".wmv")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".wma")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".m2t")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".avi")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".mpg")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".mpeg")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".mp4")
                {
                    Button.Click += ButtonVideo_Click;
                }
                else if (ext == ".jpg")
                {
                    Button.Click += ButtonImage_Click;

                }
                else if (ext == ".gif")
                {
                    Button.Click += ButtonImage_Click;
                }
                else if (ext == ".png")
                {
                    Button.Click += ButtonImage_Click;
                }
                else if (ext == ".tiff")
                {
                    Button.Click += ButtonImage_Click;
                }
                else
                {
                    Button.Click += ButtonDirectory;
                }
            }

            if (IsRootDir) MainColumn1.Width = new GridLength(0, GridUnitType.Star);
            else
            {
                MainColumn1.Width = new GridLength(1, GridUnitType.Star);    
            }

        }

        private void ButtonDirectory(object sender, RoutedEventArgs e)
        {
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //if (nav != null) nav.Navigate(new Uri("DirectoryPage.xaml", UriKind.RelativeOrAbsolute));

            NavigationService nav = NavigationService.GetNavigationService(this);
            CurrentDir += "\\" + (sender as Button).Content;
            if (nav != null)
            {
                InitializeContent();
                nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
            }

        }


        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            int intLocation = CurrentDir.LastIndexOf("\\");
            CurrentDir = CurrentDir.Substring(0, intLocation);
            if (nav != null)
            {
                InitializeContent();
                nav.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void ButtonText_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("TextPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ButtonVideo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("VideoPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ButtonImage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("ImagePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var img = sender as Image;
            if (img != null)
            {
                var iconBitmapImage = new BitmapImage();
                iconBitmapImage.BeginInit();
                iconBitmapImage.UriSource = new Uri(RootDir + @"\directory.png", UriKind.Relative);
                iconBitmapImage.EndInit();

                img.Source = iconBitmapImage;
            }
        }
       
    }
}

