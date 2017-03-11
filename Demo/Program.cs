using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using BaseElements;
using Exporter.Video;

namespace Demo
{
    class Program
    {
        static void Main(string[] args) {

            Image downImage = Image.FromFile(@"C:\Users\Home\Desktop\temp\img0.png");
            Image upImage = Image.FromFile(@"C:\Users\Home\Desktop\Dipper.png");
            using (Graphics gr = Graphics.FromImage(downImage)) {
                gr.DrawImage(upImage, new Point(0, 0));
            }
            downImage.Save(@"C:\Users\Home\Desktop\temp\out.png", ImageFormat.Png);

            //var cartoonName = "Gravity Falls";
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var cartoonPath = Path.Combine(documentsPath, cartoonName);

            //var cartoon = new Cartoon(cartoonName,
            //                          width: 1280,
            //                          height: 720,
            //                          workingDirectory: documentsPath);

            ////var destinationFilenameAvi = Path.Combine(documentsPath, cartoonName + ".avi");
            ////var destinationFilenameMkv = Path.Combine(documentsPath, cartoonName + ".mkv");
            //var destinationFilenameMp4 = Path.Combine(documentsPath, cartoonName + ".mp4");

            //IVideoExporter exporter = new Mp4Exporter();

            //// Catch exceptions
            //exporter.Save(cartoon, destinationFilenameMp4);
        }
    }
}
