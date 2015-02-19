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

        private const string RootDir = @".\Kiosk_direc";
        private static string _currentDir = @".\Kiosk_direc";

        public bool IsRootDir
        {
            get { return RootDir == _currentDir; }
        }

        public void StepUp()
        {
            if (IsRootDir)
            {
                throw new Exception("Нельзя подняться выше корневого каталога");
            }
            _currentDir = Regex.Match(_currentDir, @".*(?=\\.*$)").Value;
        }

        public Button Button;

        public MainPage()
        {
            InitializeComponent();
            InitializeContent();
            Back.Click += ButtonBack;
        }

        private static readonly string[] TextExt = {".txt", ".rtf"};
        private static readonly string[] VideoExt = {".wmv", ".wma", ".m2t", ".avi", ".mpg", ".mpeg", ".mp4"};
        private static readonly string[] ImageExt = {".jpg", ".jpeg", ".gif", ".png", ".tiff"};

        public void InitializeContent()
        {
            Title = "Информационный киоск";

            var childItems = new List<string>();
            childItems.AddRange(GetDirectories(_currentDir));
            childItems.AddRange(GetFiles(_currentDir));

            int dirCount = childItems.Count;
            int colCount, rowCount;
            _tablesize.NumberCell(dirCount, out colCount, out rowCount);

            UniformGrid1.Rows = rowCount;
            UniformGrid1.Columns = colCount;

            UniformGrid1.Children.Clear();

            foreach (string s in childItems)
            {
                Button = new Button { Content = Path.GetFileNameWithoutExtension(s), Tag = Path.GetFileName(s)}; 
                var fi = new FileInfo(s);
                string ext = fi.Extension;
                UniformGrid1.Children.Add(Button);

                if (TextExt.Contains(ext)) Button.Click += ButtonText_Click;
                else if (VideoExt.Contains(ext)) Button.Click += ButtonVideo_Click;
                else if (ImageExt.Contains(ext)) Button.Click += ButtonImage_Click;
                else Button.Click += ButtonDirectory;
            }

            MainColumn1.Width = new GridLength(IsRootDir ? 0 : 1, GridUnitType.Star);
        }

        private static IEnumerable<string> GetDirectories(string rootFolder)
        {
            return Directory.GetDirectories(rootFolder);
        }
        private static IEnumerable<string> GetFiles(string rootFolder)
        {
            string[] files = Directory.GetFiles(rootFolder);

            return files.Where(file => (new FileInfo(file).Attributes & FileAttributes.Hidden) == 0);
        }
        
        internal static void OpenPage(Page navPage, DependencyObject page, Action preNavigateAction = null)
        {
            NavigationService nav = NavigationService.GetNavigationService(page);
            if (nav == null) return;

            if (preNavigateAction != null) preNavigateAction();

            nav.Navigate(navPage);
        }

        private void ButtonDirectory(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null) _currentDir += "\\" + button.Tag;

            OpenPage(new MainPage(), this, InitializeContent);
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            int intLocation = _currentDir.LastIndexOf("\\", StringComparison.Ordinal);
            _currentDir = _currentDir.Substring(0, intLocation);

            OpenPage(new MainPage(), this, InitializeContent);
        }

        private static string GetPathFromContent(ContentControl contentControl)
        {
            return contentControl == null ? null : Path.Combine(_currentDir, contentControl.Tag.ToString());
        }

        private void ButtonText_Click(object sender, RoutedEventArgs e)
        {
            string fileName = GetPathFromContent(sender as Button);
            OpenPage(new TextPage(fileName), this);
        }
        private void ButtonVideo_Click(object sender, RoutedEventArgs e)
        {
            string fileName = GetPathFromContent(sender as Button);
            OpenPage(new VideoPage(fileName), this);
        }
        private void ButtonImage_Click(object sender, RoutedEventArgs e)
        {
            string fullCurrentDirName = Path.GetFullPath(_currentDir);
            var imageFiles = new List<string>();
            foreach (string extension in ImageExt)
            {
                string[] curExtFiles = Directory.GetFiles(fullCurrentDirName, "*" + extension, SearchOption.TopDirectoryOnly);
                imageFiles.AddRange(curExtFiles);
            }

            string fileName = GetPathFromContent(sender as Button);
            int idexOfCurrent = imageFiles.IndexOf(Path.GetFullPath(fileName));

            OpenPage(new ImagePage(imageFiles.ToArray(), idexOfCurrent), this);
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var img = sender as Image;
            if (img == null) return;

            string ext = Path.GetExtension(img.Tag.ToString());
            
            string image;
            if (TextExt.Contains(ext)) image = "new_text.png";
            else if (ImageExt.Contains(ext)) image = "image.png";
            else if (VideoExt.Contains(ext)) image = "video.png";
            else image = "new_directory.png";

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string unionPath = Path.Combine(baseDirectory, image);
            var uri = new Uri(unionPath);
            ImageSource imageSource = new BitmapImage(uri);
            img.Source = imageSource;

        }
       
    }
}

