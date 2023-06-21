using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuPetCore
{
    public interface IConfigParser
    {
        public List<ImageGroup> ImageGroupList { get; }

        public bool ReadConfig(in string path);
    }

    public class ImageGroup
    {
        public string? Name { get; set; }
        public string? DefaultDir { get; set; }
        public bool? IsAll { get; set; }
        public List<Image>? ImageList { get; set; }
    }
    public class Image
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
    }
}
