using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using System.Runtime;
using System.Windows.Media.Media3D;

namespace SuPetCore
{
    public class ImageManager
    {
        private readonly List<FileInfo> _imageList = new();

        public int ImageNum { get => _imageList.Count; }
        public IEnumerable<FileInfo> ImageList { get => _imageList; }

        public ImageManager()
        {
            var imageGroup = new SuPetCore.ImageGroup
            {
                Name = "DefaultImageGroup",
                ImageList = new List<SuPetCore.Image>
                    {
                        new SuPetCore.Image
                        {
                            Name = "DefaultImage",
                            Path = @"Resources\happy.gif"
                        }
                    }
            };

            ImportData(imageGroup);
        }

        public ImageManager(in ImageGroup imageGroup)
        {
            ImportData(imageGroup);
        }

        public void ImportData(in ImageGroup imageGroup)
        {
            string dirPath = imageGroup.DefaultDir ?? "";

            if (imageGroup.IsAll)
            {
                var dirInfo = new DirectoryInfo(dirPath);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if (IsImage(file))
                    {
                        _imageList.Add(file);
                    }
                }
            }
            else
            {
                if (imageGroup.ImageList == null) { return; }
                foreach (var image in imageGroup.ImageList)
                {
                    image.Path = dirPath + image.Path;
                    var file = new FileInfo(image.Path);
                    if (file.Exists && IsImage(file))
                    {
                        _imageList.Add(file);
                    }
                }
            }
        }

        public static bool IsImage(in FileInfo file)
        {
            return file.Extension switch
            {
                ".gif" => true,
                _ => false,
            };
        }

        public FileInfo GetRandomImage()
        {
            return _imageList[new Random().Next(0, ImageNum)];
        }

        public FileInfo GetImage(int idx)
        {
            idx  = idx >= ImageNum ? idx : 0;
            return _imageList[idx];
        }


    }
}
