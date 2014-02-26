using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly TableSize _tablesize = new TableSize();


        public Button Button;
        public MainWindow()
        {
            InitializeComponent();

            Width = 950;
            Height = 650;
            Left = Top = 100;
            Title = "Информационный киоск";

            string cur = Directory.GetCurrentDirectory();
            UniformGrid1.Background = new ImageBrush(new BitmapImage(new Uri(cur + @"\bgmain.jpg")));

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
                UniformGrid1.Children.Add(Button);
                
            }
        }
    }
}
