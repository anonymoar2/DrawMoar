

using System.Drawing;

namespace Exporter.Photo
{
    public interface IPhotoExporter
    {
        void Save(Image image, string filename);
    }
}
