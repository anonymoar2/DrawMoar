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

        // TODO: и про процесс ещё что-то
        public static void OpenMW(string file) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = file;
            Process.Start(startInfo);
            // TODO: не запускать новое окошко
        }

        public void Save(Cartoon cartoon, string path) {

        }
    }
}
