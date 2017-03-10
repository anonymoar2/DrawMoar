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
        public Image ImageFromBytes(byte[] bytes) {
            using (var ms = new MemoryStream(bytes)) {
                return Image.FromStream(ms);
            }
        }

        // filename включая path до картинки
        public string Save(Frame frame, string filename) {
            // Допустим пока слоёв у нас нет, тогда на frame содержит image
            frame.image = ImageFromBytes(frame.bytes);
            frame.image.Save(filename);
            return filename;
        }
        
    }
}
