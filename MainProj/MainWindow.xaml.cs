using Microsoft.Win32;
using SuPetCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace MainProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SuPetCore.ImageManager _imageManager;


        public MainWindow()
        {
            InitializeComponent();

            // 设置窗口
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;

            // 加载Gif
            _imageManager = new(@"D:\Projects\SuPet\Resources", ".gif");
            LoadImage(_imageManager.GetRandomImage().FullName);
        }
        private void LoadImage(string filePath)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filePath);
            bitmapImage.EndInit();
            ImageBehavior.SetAnimatedSource(myImage, bitmapImage);
        }
        private void OpenDir(string dirPath)
        {
            System.Diagnostics.Process.Start("Explorer.exe", dirPath);
        }

        private void myImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void myImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenDir(_imageManager.ImageDirPath);
        }

    }
}
