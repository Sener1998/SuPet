using Nett;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SuPetCore
{
    public class TomlReader : IConfigParser
    {
        private readonly List<ImageGroup> _imageGroupList = new();
        private readonly List<Menu> _menuList = new();
        List<ImageGroup> IConfigParser.ImageGroupList { get => _imageGroupList; }
        List<Menu> IConfigParser.MenuList { get => _menuList; }

        bool IConfigParser.ReadConfig(in string path)
        {
            try
            {
                var config = Toml.ReadFile(path);
                GetImageGroupList(config);
                GetMenuList(config);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void GetImageGroupList(in TomlTable configTable)
        {
            _imageGroupList.Clear();

            var imageGroupTables = configTable.Get<TomlTableArray>("ImageGroup");
            foreach (var item in imageGroupTables.Items)
            {
                var imageGroup = new ImageGroup();
                try
                {
                    imageGroup.Name = item.Get<string>("name");
                    imageGroup.DefaultDir = item.Get<string>("defaultDir");
                    imageGroup.IsAll = item.Get<bool>("isAll");
                }
                catch { }

                if (!imageGroup.IsAll)
                {
                    imageGroup.ImageList = new List<string>();
                    var imageList = item.Get<TomlTableArray>("image");
                    foreach (var image in imageList.Items)
                    {
                        imageGroup.ImageList.Add(image.Get<string>("path"));
                    }
                }

                _imageGroupList.Add(imageGroup);
            }
        }

        private void GetMenuList(in TomlTable configTable)
        {
            _menuList.Clear();
            var menuTables = configTable.Get<TomlTableArray>("Menu");
            foreach (var table in menuTables.Items)
            {
                _menuList.Add(GetMenu(table));
            }
        }

        private Menu GetMenu(in TomlTable configTable)
        {
            var menu = new Menu();
            TomlTableArray? subMenuTables = null;
            try
            {
                menu.Name = configTable.Get<string>("name");
                menu.Text = configTable.Get<string>("text");
                menu.IsNeedAP = configTable.Get<bool>("isNeedAP");
                menu.Program = configTable.Get<string>("program");
                menu.Path = configTable.Get<string>("path");
                subMenuTables = configTable.Get<TomlTableArray>("Menu");
            }
            catch { }

            if (subMenuTables != null)
            {
                menu.SubMenu = new List<Menu>();
                foreach (var table in subMenuTables.Items)
                {
                    menu.SubMenu.Add(GetMenu(table));
                }
            }

            return menu;
        }
    }
}
