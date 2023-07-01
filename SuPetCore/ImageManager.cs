using System;
using System.IO;
using System.Collections.Generic;

namespace SuPetCore
{
    public class ImageManager
    {
        private readonly List<string> _imageList = new();

        public int ImageNum { get => _imageList.Count; }
        public IEnumerable<string> ImageList { get => _imageList; }

        public ImageManager() { }
        public ImageManager(in ImageGroup imageGroup)
        {
            ImportData(imageGroup);
        }

        public void ImportData(in ImageGroup imageGroup)
        {
            string dirPath = imageGroup.DefaultDir ?? "";

            if (imageGroup.IsAll)
            {
                DirectoryInfo dirInfo = new(dirPath);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if (IsImage(file))
                    {
                        _imageList.Add(file.FullName);
                    }
                }
            }
            else
            {
                if (imageGroup.ImageList == null) { return; }
                foreach (var image in imageGroup.ImageList)
                {
                    FileInfo file = new((dirPath == "" ? dirPath : (dirPath + "\\") ) + image);
                    if (file.Exists && IsImage(file))
                    {
                        _imageList.Add(file.FullName);
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

        public string GetRandomImage()
        {
            return _imageList[new Random().Next(0, ImageNum)];
        }

        public string GetImage(int idx = 0)
        {
            idx  = idx >= ImageNum ? idx : 0;
            return _imageList[idx];
        }
    }
}
