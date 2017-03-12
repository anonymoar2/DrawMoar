using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BaseElements
{
    internal class RasterLayer : Layer
    {
        // Конструктор из картинки будет в импортере, там делаем из картинки Image и сюда пихаем
        public Image Image { get; private set; }
        public Bitmap Bitmap { get; private set; } // нужно ли?

        public RasterLayer() {
            // TODO: Конструкторы
        }

        public RasterLayer(Image image) {
            Image = image;
        }

        /// <summary>
        /// Сохранение слоя.
        /// </summary>
        /// <param name="WorkingDirectory">Директория в которую сохраняется слой.</param>
        public override void Save(string WorkingDirectory) {
            string pathToFile = Path.Combine(WorkingDirectory, $"{Name}.png");
            Image.Save(pathToFile);
            // потом будет метод в экспорте, и будет вызываться он отсюда, а это как обертка такая просто будет
        }
    }
}
