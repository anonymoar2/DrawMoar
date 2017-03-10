using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using BaseElements;
using System.IO;

namespace Exporter.Photo
{
    public class PngExporter : IPhotoExporter
    {
        // filename включая path до картинки
        public string Save(Frame frame, string filename) {
            // Допустим пока слоёв у нас нет, тогда на frame содержит image
            frame.image.Save(filename);
            return filename;
        }
        
    }
}
