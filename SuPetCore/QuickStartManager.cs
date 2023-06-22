using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuPetCore
{
    public class QuickStartManager
    {
        private readonly List<Menu> _menuList = new();
        private readonly List<Menu> _endMenuList = new();
        public IEnumerable<Menu> MenuList { get => _menuList; }
        public IEnumerable<Menu> EndMenuList { get => _endMenuList; }

        public QuickStartManager() { }
        public QuickStartManager(in List<Menu> menuList)
        {
            _menuList = menuList;
            ImportData(menuList);
        }

        public void ImportData(in List<Menu> menuList)
        {
            foreach (var menu in menuList)
            {
                AddEndMenu(menu);
            }
        }
        public void AddEndMenu(Menu menu)
        {
            if(menu.SubMenu == null)
            {
                _endMenuList.Add(menu);
            }
            else
            {
                foreach (var subMenu in menu.SubMenu)
                {
                    _endMenuList.Add(subMenu);
                }
            }
        }

        public void StartMenuByName(in string name)
        {
            var menu = FindMenu(name);
            if (menu != null)
            {
                StartMenu(menu);
            }
        }
        public Menu? FindMenu(string name)
        {
            foreach (var menu in _endMenuList)
            {
                if (menu.Name == name)
                {
                    return menu;
                }
            }
            return null;
        }
        public static void StartMenu(Menu menu)
        {
            if (menu.SubMenu == null && menu.Path != null)
            {
                menu.Program ??= "";
                if (menu.Program == "")
                {
                    Process.Start(menu.Path);
                }
                else
                {
                    Process.Start(menu.Program, menu.Path);
                }
            }
        }

        public void ClearMenuList()
        {
            _menuList.Clear();
        }

    }

}
