using System.Collections.Generic;

namespace SuPetCore
{
    public interface IConfigParser
    {
        public List<ImageGroup> ImageGroupList { get; }
        public List<Menu> MenuList { get; }

        public bool ReadConfig(in string path);
    }

    public class ImageGroup
    {
        public string? Name { get; set; }
        public string? DefaultDir { get; set; }
        public bool IsAll { get; set; }
        public List<string>? ImageList { get; set; }
    }

    public class Menu
    {
        public string? Name { get; set; }
        public string? Text { get; set; }
        public bool IsNeedAP { get; set; }
        public string? Program { get; set; }
        public string? Path { get; set; }
        public List<Menu>? SubMenu { get; set; }
    }
}
