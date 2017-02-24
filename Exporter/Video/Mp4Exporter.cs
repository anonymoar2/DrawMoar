using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;

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
        public void Save(Cartoon cartoon, string path) {
            var concatFilename = CreateConcatFile();

            Process process = new Process();
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Dipper");
            process.StartInfo.Arguments = $"-y -loglevel panic -f concat -i {concatFilename} out.mp4";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            // Set your output and error (asynchronous) handlers.
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            // Start process and handlers.
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        /// <summary>
        /// !!! ATTENTION  !!!
        /// !!! DUMMY CODE !!!
        /// </summary>
        /// <returns>Name of file which will be used in ffmpeg concat protocol.</returns>
        private static string CreateConcatFile() {
            string imagesDirectory = "Dipper";
            string imagesListFilename = "images.txt";
            string imagesListFilenameRelative = Path.Combine(imagesDirectory, imagesListFilename);

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectory);
            using (var writer = new StreamWriter(imagesListFilenameRelative)) {
                foreach (var file in directoryInfo.GetFiles("img*.png")) {
                    writer.WriteLine("file " + file.Name);
                    writer.WriteLine("duration 1");
                }
            }

            return imagesListFilename;
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
            // Do your stuff with the output (write to console/log/StringBuilder).
            Console.WriteLine(outLine.Data);
        }
    }
}
