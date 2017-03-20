using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawMoar;

namespace Exporter.Video
{
    public class AviExporter : IVideoExporter
    {
        // TODO: *Add ffmpeg arguments related to this format as arguments to constructor.
        public AviExporter() {

        }

        public void Save(Cartoon cartoon, string path) {
            throw new NotImplementedException();
        }
    }
}
