using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrawMoar;

namespace Exporter.Video
{
    public interface IVideoExporter
    {
        void Save(Cartoon cartoon, string path);
    }
}
