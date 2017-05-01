using System;
using System.IO;
using System.Windows.Media.Imaging;

using System.Drawing;
using System.Drawing.Imaging;

namespace Exporter.Photo
{
    public class PngExporter : IPhotoExporter
    {
        public void Save(Image image, string filename) {
            image.Save("filemane", ImageFormat.Png);
        }
    }
}
