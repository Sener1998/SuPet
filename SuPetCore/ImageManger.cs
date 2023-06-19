using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using System.Runtime;

namespace SuPetCore
{
    public class ImageManager
    {
        private readonly List<FileInfo> _imageList;

        public int ImageNum { get; private set; }
        public string ImageDirPath { get; private set; }
        public IEnumerable<FileInfo> ImageList { get => _imageList; }

        public ImageManager(string dirPath, string extension)
        {
            ImageDirPath = dirPath;
            _imageList = new List<FileInfo>();
            UpdateImageList(dirPath, extension);
        }

        public int UpdateImageList(string dirPath, string extension)
        {
            ImageDirPath = dirPath;
            _imageList.Clear();
            var dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Extension == extension)
                {
                    _imageList.Add(file);
                }
            }
            ImageNum = _imageList.Count;
            return ImageNum;
        }

        public FileInfo GetRandomImage()
        {
            return _imageList[new Random().Next(0, ImageNum)];
        }

    }

}
