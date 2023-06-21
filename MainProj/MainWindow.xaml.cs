using Microsoft.Win32;
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
using System.Xml.Serialization;
using WpfAnimatedGif;

namespace MainProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SuPetCore.ImageManager _ImageManagerMain;

        public MainWindow()
        {
            InitializeComponent();

            // 设置窗口
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;

            // 加载图片
            SuPetCore.IConfigParser tomlReader = new SuPetCore.TomlReader();
            tomlReader.ReadConfig("SuPetConfig.toml");
            _ImageManagerMain = new(tomlReader.ImageGroupList[0]);

            // 初始化界面
            InitImage(ref myImage, _ImageManagerMain);

            // 测试
            //ImageManager testManager = new(@"C:\Users\Sener\Desktop\游戏", "");
        }
        private void InitImage(ref Image image, in SuPetCore.ImageManager imageManager)
        {
            SetImageAnimation(ref image, imageManager.GetImage(0).FullName);
            SetImageContextMenu(ref image);
        }
        private static void SetImageAnimation(ref Image image, string filePath)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filePath);
            bitmapImage.EndInit();
            ImageBehavior.SetAnimatedSource(image, bitmapImage);
        }
        private void SetImageContextMenu(ref Image image)
        {
            // 开发中
            var GameMenu = new MenuItem { Header = "游戏" };
            GameMenu.Items.Add(new MenuItem { Header = "Steam"});
            GameMenu.Items.Add(new MenuItem { Header = "Epic" });
            foreach (MenuItem item in GameMenu.Items)
            {
                item.Click += QuickStart_Click;
            }
            image.ContextMenu.Items.Add(GameMenu);

            var CloseMenu = new MenuItem { Header = "关闭" };
            CloseMenu.Click += (object sender, RoutedEventArgs e) => Close();
            image.ContextMenu.Items.Add(CloseMenu);
        }
        private void QuickStart_Click(object sender, RoutedEventArgs e)
        {
            // 开发中
            if (sender is not MenuItem menuItem) { return; }
            string? itemName = menuItem.Header.ToString();
            if (itemName == "Steam")
            {
                Process.Start(@"E:\Games\Steam\Steam.exe");
            }
            else if (itemName == "Epic")
            {
                Process.Start(@"E:\Games\Epic Games\Launcher\Portal\Binaries\Win32\EpicGamesLauncher.exe");
            }
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
            // 开发中
            // 互动逻辑
        }
    }
}
