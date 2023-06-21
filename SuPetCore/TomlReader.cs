using Nett;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuPetCore
{
    public class TomlReader : IConfigParser
    {
        private readonly List<ImageGroup> _imageGroupList = new();
        List<ImageGroup> IConfigParser.ImageGroupList { get { return _imageGroupList; } }

        bool IConfigParser.ReadConfig(in string path)
        {
            _imageGroupList.Clear();

            var config = Toml.ReadFile(path);
            if (config == null) { return  false; }

            var imageGroupTable = config.Get<TomlTableArray>("ImageGroup");
            foreach (var item in imageGroupTable.Items)
            {
                var imageGroup = new ImageGroup();
                try
                {
                    imageGroup.Name = item.Get<string>("name");
                    imageGroup.DefaultDir = item.Get<string>("defaultDir");
                    imageGroup.IsAll = item.Get<bool>("isAll");
                } catch { }

                if (imageGroup.IsAll != true)
                {
                    imageGroup.ImageList = new List<Image>();
                    var imageList = item.Get<TomlTableArray>("image");
                    foreach (var image in imageList.Items)
                    {
                        imageGroup.ImageList.Add(new Image { Name = image.Get<string>("name"), Path = image.Get<string>("path") });
                    }
                }

                _imageGroupList.Add(imageGroup);
            }

            return true;
        }
    }
}
