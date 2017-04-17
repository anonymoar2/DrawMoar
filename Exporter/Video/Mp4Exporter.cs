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
        // TODO: *Add ffmpeg arguments related to this format as arguments to constructor.
        public Mp4Exporter() {

        }

        /// <summary>
        /// !!! ATTENTION  !!!
        /// !!! DUMMY CODE !!!
        /// http://coub.com/view/4thgf
        /// 
        /// ffmpeg args:
        /// -y - say "yes" to overwrite file.
        /// -loglevel panic - output becomes less verbose.
        /// -f concat - activates concat protocol.
        /// </summary>
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
            // Set your output and error (asynchronous) handlers.
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            // Start process and handlers.
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            File.Move(Path.Combine(path, "out.mp4"), Path.Combine(pathVideo, "out.mp4")); //images.txt находится в одной папке с картинками, а видео отдельно
         
           
        }

        /// <summary>
        /// Создаёт файл с именами картинок соответствующих каждому кадру, 
        /// а также с продолжительностью каждого кадра.
        /// </summary>
        /// <returns>Имя сформированного файла.</returns>
        private static string CreateConcatFile(List<Bitmap> images, string path) {
            string imagesDirectory = path;
            string imagesListFilename = "images.txt";
            string imagesListFilenameRelative = Path.Combine(imagesDirectory, imagesListFilename);

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectory);
            using (var writer = new StreamWriter(imagesListFilenameRelative)) {
                //PngExporter pngExporter = new PngExporter();              
                SaveAllBitmapToPNG(images, path);
                
                foreach (var image in images) {
                    //pngExporter.Save(frame, Path.Combine(cartoon.WorkingDirectory, $"img{frames.IndexOf(frame)}.png"));
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
            // Do your stuff with the output (write to console/log/StringBuilder).
            Console.WriteLine(outLine.Data);
        }
    }
}
