using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;
using System.Diagnostics;

namespace Exporter.Video
{
    public class MP4 : IVideoExporter
    {
        // TODO: билдер для аргументов

        

        public static void Save(/*Cartoon cartoon, string pathToResult*/ string pathToResult, string[] pathToImages) {
            var ffmpegProcessInfo = new ProcessStartInfo("ffmpeg.exe");
            ffmpegProcessInfo.CreateNoWindow = true;
            ffmpegProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ffmpegProcessInfo.UseShellExecute = false;
            ffmpegProcessInfo.RedirectStandardError = true;
            ffmpegProcessInfo.RedirectStandardOutput = true;

            StringBuilder str = new StringBuilder("ffmpeg ");
            
            foreach(var path in pathToImages) {
                str.Append($" -i {path} ");
            }
            str.Append($@" {pathToResult}\out2.mp4");

            ffmpegProcessInfo.Arguments = str.ToString();
            Console.WriteLine(ffmpegProcessInfo.Arguments);
            using (var proc = Process.Start(ffmpegProcessInfo))
                proc.WaitForExit();
            // return ffmpegProcessInfo;
            // либо возвращать конкретные данные
            // либо не возвращать ничего
        }
    }
}
