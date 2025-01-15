using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        static bool isPng = false;

        static string path;
        static string[] files;

        public void Start()
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Cache\Cache_Data";
            try
            {
                files = Directory.GetFiles(path);

                foreach (string f in files)
                {
                    if (f.Contains(".png"))
                    {
                        isPng = true;
                        break;
                    }
                }
                TextBox_FolderPath.Text = path;
                Test_Copy.Content = isPng.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to find discord's cache folder!.");
            }
        }

        public void ChangeToPNG()
        {
            path = TextBox_FolderPath.Text;

            if (!isPng)
            {
                foreach (var f in files)
                {
                    File.Move(f.ToString(), f.ToString() + ".png");
                    isPng = true;
                }
                files = Directory.GetFiles(path);
            }
        }

        public void ChangeToFile()
        {
            path = TextBox_FolderPath.Text;

            if (isPng)
            {
                foreach (var f in files)
                {
                    File.Move(f.ToString(), f.Remove(f.IndexOf('.')));
                    isPng = false;
                }
                files = Directory.GetFiles(path);
            }
        }

        private void OpenFolder(string folderPath)
        {
            path = TextBox_FolderPath.Text;

            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }
        }
    


        private void Button_File_Click(object sender, RoutedEventArgs e)
        {

            ChangeToFile();
            Test_Copy.Content = isPng.ToString();
        }

        private void Button_PNG_Click(object sender, RoutedEventArgs e)
        {
            ChangeToPNG();
            Test_Copy.Content = isPng.ToString();
        }

        private void Button_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder(path);   
        }
    }
}
