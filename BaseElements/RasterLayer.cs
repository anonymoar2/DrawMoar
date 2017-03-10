using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace BaseElements
{
    internal class RasterLayer: Layer
    {
        // Конструктор из картинки будет в импортере, там делаем из картинки Image и сюда пихаем
        private Image image;
        private Bitmap bitmap; // нужно ли?
        

        public RasterLayer() {
            /// TODO: Конструкторы
        }
        
        
        public RasterLayer(Image image) {
            this.image = image;
        }
        
        /// <summary>
        /// Save layer
        /// </summary>
        /// <param name="WorkingDirectory"></param>
        /// <returns></returns>
        public override void Save(string WorkingDirectory) {
            string pathToFile = Path.Combine(WorkingDirectory, $"{Name}.png");
            image.Save(pathToFile);
            // потом будет метод в экспорте, и будет вызываться он отсюда, а это как обертка такая просто будет
        }
    }
}
