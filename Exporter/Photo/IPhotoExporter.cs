using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;

namespace Exporter.Photo
{
    public interface IPhotoExporter
    {
        void Save(Frame frame, string path);
    }
}
