using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        readonly TableSize _tablesize = new TableSize();

        public Button Button;
        public MainPage()
        {
            InitializeComponent();


            Title = "Информационный киоск";

            string cur = Directory.GetCurrentDirectory();

            UniformGrid1.Background = Utils.Utils.GetBrushFromBitmap(Properties.Resources.bgmain);
            string[] directories = Directory.GetDirectories(@".\Kiosk_direc");

            DirectoryInfo directory = new DirectoryInfo(@".\Kiosk_direc");
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

            UniformGrid1.Rows = rowCount;
            UniformGrid1.Columns = colCount;
           

            foreach (string s in directories)
            {

                Button = new Button { Content = Path.GetFileName(s)};
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

           }

        private void ButtonDirectory(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("DirectoryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        void ButtonText_Click(object sender, RoutedEventArgs e)
        {
            //if (NavigationService != null)
           //NavigationService.Navigate(new Uri("TextPage.xaml", UriKind.Relative));

            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("TextPage.xaml", UriKind.RelativeOrAbsolute));
        }

         void ButtonVideo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            if (nav != null) nav.Navigate(new Uri("VideoPage.xaml", UriKind.RelativeOrAbsolute));
        }

         void ButtonImage_Click(object sender, RoutedEventArgs e)
         {

             NavigationService nav = NavigationService.GetNavigationService(this);
             if (nav != null) nav.Navigate(new Uri("ImagePage.xaml", UriKind.RelativeOrAbsolute));
         }
        private void setImage(Image img)
        {
            if (img != null)
            {

                var iconBitmapImage = new BitmapImage();
                iconBitmapImage.BeginInit();
                string cur = Directory.GetCurrentDirectory();

                iconBitmapImage.UriSource = new Uri(cur + @"\directory.png");

                iconBitmapImage.EndInit();
                img.Source = iconBitmapImage;

            }  
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            setImage(sender as Image);
        }
      
    }
    }

