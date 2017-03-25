using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Exporter.Video
{
    public interface IVideoExporter
    {
        void Save(List<Image> images, string path);
    }
}
