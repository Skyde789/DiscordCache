using System;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DiscordCache
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        static string path;
        static string[] files;

        
        public void Start()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Cache\Cache_Data";
            FileEndTextBox.Text = ".png";
            try
            {
                files = Directory.GetFiles(path);
                
                TextBox_FolderPath.Text = path;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to find discord's cache folder!.");
            }
            MajorityExtension();
        }

        // Changes the file extension of all the files
        // 1 = converts to user chosen extension
        // 2 = converts to a plain file
        public void ChangeExtension(int buttonNum)
        {
            path = TextBox_FolderPath.Text;

            foreach (var f in files)
            {
                switch (buttonNum)
                {
                    case 1:
                        File.Move(f.ToString(), f.IndexOf('.') != -1 ? f.Remove(f.IndexOf('.')) + FileEndTextBox.Text : f.ToString() + FileEndTextBox.Text);
                        break;
                    case 2:
                        File.Move(f.ToString(), f.IndexOf('.') != -1 ? f.Remove(f.IndexOf('.')) : f.ToString());
                        break;
                }
            }
            files = Directory.GetFiles(path);

            MajorityExtension();
        }

        private void OpenFolder()
        {
            path = TextBox_FolderPath.Text;

            if (Directory.Exists(path))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = path,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", path));
            }
        }
        
        public void LoadFolder()
        {
            path = TextBox_FolderPath.Text;

            try
            {
                files = Directory.GetFiles(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load folder!.");
            }

            MajorityExtension();
        }

        // Loops through every file extension in the folder and checks what is the most frequent
        public void MajorityExtension()
        {
            Dictionary<string, int> test = new Dictionary<string, int>();
            string ext = "";

            foreach (string filename in files)
            {
                ext = filename.Remove(0,filename.LastIndexOf("\\"));

                if(ext.IndexOf(".") == -1)
                    ext = "None";
                else
                    ext = filename.Remove(0,filename.IndexOf("."));

                if (!test.ContainsKey(ext))
                    test.Add(ext, 1);
                else
                    test[ext]++;
            }

            FileFormatText.Content = "";

            foreach(KeyValuePair<string,int> t in test)
            {

                FileFormatText.Content += t.Key + " ";
            }
        }
        private void FileEndButton1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Convert to " + FileEndTextBox.Text, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                ChangeExtension(1);
        }

        private void FileEndButton2_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Convert to plain files", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                ChangeExtension(2);
        }

        private void FolderOpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder();        
        }

        private void LoadFolderButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFolder();
        }
    }
}
