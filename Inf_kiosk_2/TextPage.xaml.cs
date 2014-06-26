using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace Inf_kiosk_2
{
    /// <summary>
    /// Логика взаимодействия для TextPage.xaml
    /// </summary>
    public partial class TextPage
    {
        private static Dictionary<string, string> formats = new Dictionary<string, string>
        {
            {".txt", DataFormats.Text},
            {".rtf", DataFormats.Rtf}
        };

        public TextPage(string fileName)
        {
            InitializeComponent();
            
            Back.Click += Back_Click;


            if (!File.Exists(fileName)) return;

            var tr = new TextRange(RichTextBox1.Document.ContentStart, RichTextBox1.Document.ContentEnd);

            string ext = Path.GetExtension(fileName);
            if (String.IsNullOrWhiteSpace(ext)) return;
            
            string format;
            bool valueFound = formats.TryGetValue(ext, out format);
            if (!valueFound) return;

            using (FileStream fs = File.Open(fileName, FileMode.Open))
            {
                tr.Load(fs, format);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage.OpenPage(new MainPage(), this);
        }
    }
}
