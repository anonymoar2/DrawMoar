using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace BaseElements
{
    // I don't yet know why it's internal 
    internal class RasterLayer: Layer
    {
        private string pathToPicture;
        private Image image;
        private Bitmap bitmap;
        
        
        public RasterLayer(string pathToPicture) {
            MemoryStream mstream = new MemoryStream(File.ReadAllBytes(pathToPicture));
            image = Image.FromStream(mstream);
            bitmap = new Bitmap(image);
            save = true;
        }

        // ещё один конструктор который создаёт изображение из чего-то другого
        // и вызывает метод Save сразу же

        
        /// <summary>
        /// Save layer
        /// </summary>
        /// <param name="WorkingDirectory"></param>
        /// <returns></returns>
        public override void Save(string WorkingDirectory) {
            string pathToFile = Path.Combine(WorkingDirectory, $"{Name}.png");
            image.Save(pathToFile);
            save = true;
        }
    }
}
