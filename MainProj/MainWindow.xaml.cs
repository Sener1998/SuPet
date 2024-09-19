using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

static class ConstConfig
{
    public const string WindowsConfigPath = "WindowsConfig.log";
    public const string SuPetConfigPath = "SuPetConfig.toml";
}

namespace MainProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private readonly SuPetCore.ImageManager _imageManagerAll = new();
        private readonly SuPetCore.QuickStartManager _quickStartManager = new();

        public MainWindow()
        {
            double width = 250;
            double height = 250;

            // 设置窗口
            try
            {
                StreamReader streamReader = new(ConstConfig.WindowsConfigPath);
                WindowStartupLocation = WindowStartupLocation.Manual;
                Left = Double.Parse(streamReader.ReadLine() ?? "0");
                Top = Double.Parse(streamReader.ReadLine() ?? "0");
                width = Double.Parse(streamReader.ReadLine() ?? "250");
                height = Double.Parse(streamReader.ReadLine() ?? "250");
                streamReader.Close();
            }
            catch
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();

            Width = width;
            Height = height;
            MainImage.Width = width;
            MainImage.Height = height;

            // 读取配置
            SuPetCore.IConfigParser tomlReader = new SuPetCore.TomlReader();
            if (tomlReader.ReadConfig(ConstConfig.SuPetConfigPath))
            {
                _imageManagerAll = new(tomlReader.ImageGroupList[0]);
                _quickStartManager = new(tomlReader.MenuList);
            }
            else
            {
                ExitWithError();
            }



            // 初始化界面
            InitImage(ref MainImage);
        }

        private void InitImage(ref Image image)
        {
            SetImageAnimation(ref image, _imageManagerAll.GetRandomImage());
            SetImageContextMenu(ref image);
        }
        private static void SetImageAnimation(ref Image image, string filePath)
        {
            BitmapImage bitmapImage = new();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filePath);
            bitmapImage.EndInit();
            ImageBehavior.SetAnimatedSource(image, bitmapImage);
        }
        private void SetImageContextMenu(ref Image image)
        {
            image.ContextMenu = new ContextMenu();
            foreach (var menu in _quickStartManager.MenuList)
            {
                var menuItem = GetMenuItem(menu);
                image.ContextMenu.Items.Add(menuItem);
            }
            _quickStartManager.ClearMenuList();

            MenuItem CloseMenu = new() { Header = "关闭" };
            CloseMenu.Click += (object sender, RoutedEventArgs e) => Close();
            image.ContextMenu.Items.Add(CloseMenu);
        }
        private MenuItem? GetMenuItem(SuPetCore.Menu menu)
        {
            if (menu.Name == null || menu.Text == null)
            {
                return null;
            }

            MenuItem menuItem = new() { Name = "MenuItem" + menu.Name, Header = menu.Text };
            if (menu.SubMenu == null)
            {
                if (File.Exists(menu.Path) || Directory.Exists(menu.Path))
                {
                    menuItem.Click += QuickStart_Click;
                }
            }
            else
            {
                foreach (var subMenu in menu.SubMenu)
                {
                    var subMenuItem = GetMenuItem(subMenu);
                    menuItem.Items.Add(subMenuItem);
                }
            }

            return menuItem;
        }
        private void QuickStart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem menuItem)
            {
                return;
            }
            string name = menuItem.Name["MenuItem".Length..];
            _quickStartManager.StartMenuByName(name);
        }

        private void MainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 交互逻辑
        }

        private void MainImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StreamWriter streamWrite = new(ConstConfig.WindowsConfigPath);
            streamWrite.WriteLine(Left);
            streamWrite.WriteLine(Top);
            streamWrite.WriteLine(Width);
            streamWrite.WriteLine(Height);
            streamWrite.Close();
        }

        private static void ExitWithError()
        {
            MessageBox.Show("配置读取失败！");
            Environment.Exit(0);
        }
    }
}
