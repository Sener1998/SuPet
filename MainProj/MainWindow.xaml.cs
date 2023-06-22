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
        private readonly SuPetCore.ImageManager _imageManagerAll = new();
        private readonly SuPetCore.QuickStartManager _quickStartManager = new();

        public MainWindow()
        {
            InitializeComponent();

            // 设置窗口
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;

            // 读取配置
            SuPetCore.IConfigParser tomlReader = new SuPetCore.TomlReader();
            if (tomlReader.ReadConfig("SuPetConfig.toml"))
            {
                _imageManagerAll = new(tomlReader.ImageGroupList[0]);
                _quickStartManager = new(tomlReader.MenuList);
            }

            // 初始化界面
            InitImage(ref myImage);
        }

        private void InitImage(ref Image image)
        {
            SetImageAnimation(ref image, _imageManagerAll.GetRandomImage().FullName);
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
            image.ContextMenu = new ContextMenu();
            foreach (var menu in _quickStartManager.MenuList)
            {
                var menuItem = GetMenuItem(menu);
                image.ContextMenu.Items.Add(menuItem);
            }
            _quickStartManager.ClearMenuList();

            var CloseMenu = new MenuItem { Header = "关闭" };
            CloseMenu.Click += (object sender, RoutedEventArgs e) => Close();
            image.ContextMenu.Items.Add(CloseMenu);

            
        }
        private MenuItem? GetMenuItem(SuPetCore.Menu menu)
        {
            if (menu.Name == null || menu.Text == null)
            {
                return null;
            }

            var menuItem = new MenuItem { Name = "MenuItem" + menu.Name, Header = menu.Text };
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
