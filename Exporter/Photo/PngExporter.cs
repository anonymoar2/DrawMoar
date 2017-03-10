using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;

namespace Exporter.Photo
{
    public class PngExporter : IPhotoExporter
    {
        public void Save(Frame frame, string path) {
            // Допустим пока слоёв у нас нет, тогда на frame содержит image
        }
    }
}
