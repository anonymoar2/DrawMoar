//using System;
//using System.IO;
//using System.Windows.Media.Imaging;

//using BaseElements;

//namespace Exporter.Photo
//{
//    public class PngExporter : IPhotoExporter
//    {
//        public void Save(Frame frame, string filename) {
//            var enc = new PngBitmapEncoder();
//            enc.Frames.Add(BitmapFrame.Create(frame.Bitmap));

//            using (FileStream stm = File.Create(filename))
//            {
//                enc.Save(stm);
//            }
//        }
//    }
//}
