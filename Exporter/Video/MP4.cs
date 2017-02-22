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
        public static void OpenMW(string file) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = file;
            Process.Start(startInfo);
        }

        public void Save(Cartoon cartoon, string path) {

        }
    }
}
