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
            //var enc = new PngBitmapEncoder();
            //enc.Frames.Add(BitmapFrame.Create(frame.Bitmap));

            //using (FileStream stm = File.Create(filename)) {
            //    enc.Save(stm);
            //}
            image.Save("filemane", ImageFormat.Png);
        }
    }
}
