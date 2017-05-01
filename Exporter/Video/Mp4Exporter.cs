using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exporter.Photo;
using System.Drawing;

namespace Exporter.Video
{
    public class Mp4Exporter : IVideoExporter
    {
        public void Save(List<Bitmap> images, string path) {
            string pathVideo = path;
            path = Path.Combine(path, "Image");
            Directory.CreateDirectory(path);
            var concatFilename = CreateConcatFile(images, path);

            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.Arguments = $"-y -loglevel panic -f concat -i {concatFilename} out.mp4";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
           
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
      
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            File.Move(Path.Combine(path, "out.mp4"), Path.Combine(pathVideo, "out.mp4")); 
        }

        private static string CreateConcatFile(List<Bitmap> images, string path) {
            string imagesDirectory = path;
            string imagesListFilename = "images.txt";
            string imagesListFilenameRelative = Path.Combine(imagesDirectory, imagesListFilename);

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectory);
            using (var writer = new StreamWriter(imagesListFilenameRelative)) {
           
                SaveAllBitmapToPNG(images, path);
                
                foreach (var image in images) {
                   
                    writer.WriteLine("file " + $"img{images.IndexOf(image)}.png");
                    writer.WriteLine($"duration 0.04");
                }
            }
            return imagesListFilename;
        }

        private static void SaveAllBitmapToPNG(List<Bitmap> images, string path) {
            foreach (var image in images) {
                image.Save(Path.Combine(path, $"img{images.IndexOf(image)}.png"), System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
  
            Console.WriteLine(outLine.Data);
        }
    }
}
