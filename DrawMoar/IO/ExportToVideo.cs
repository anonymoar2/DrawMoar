using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawMoar.BaseElements;
using System.IO;
using System.Diagnostics;


namespace DrawMoar.IO
{
    public static class ExportToVideo
    {
        public static void SaveToVideo(Cartoon cartoon, string outFileFormat) {
            if (outFileFormat != "mp4" && outFileFormat != "avi") {
                throw new ArgumentException("Недопустимый формат файла");
            }
            string pathToImages = Path.Combine(cartoon.WorkingDirectory, "Images");
            Directory.CreateDirectory(pathToImages);
            var concatFilename = CreateTemporaryFiles(cartoon, pathToImages);

            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = pathToImages;
            process.StartInfo.Arguments = $"-y -loglevel panic -f concat -i {concatFilename} out.{outFileFormat}";
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
            File.Move(Path.Combine(pathToImages, $"out.{outFileFormat}"), Path.Combine(cartoon.WorkingDirectory, $"out.{outFileFormat}"));
        }


        private static string CreateTemporaryFiles(Cartoon cartoon, string path) {
            string imagesDirectory = path;
            string imagesListFilename = Path.Combine(imagesDirectory, "images.txt");

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectory);
            int count = 0;
            using (var writer = new StreamWriter(imagesListFilename)) {
                cartoon.scenes.ForEach(
                    scene => scene.frames.ForEach(
                    frame => {
                        frame.Join().Save(Path.Combine(imagesDirectory, $"img{count}.png"));
                        writer.WriteLine("file " + $"img{count}.png");
                        writer.WriteLine($"duration 0.04");
                        count++;
                    }
                ));
            }
            return imagesListFilename;
        }


        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            Console.WriteLine(outLine.Data);
        }
    }
}
