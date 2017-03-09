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
        }

        

        public RasterLayer(Image image, string workingDirectory) {
            this.image = image;
            //this.bitmap = (Bitmap)image; не уверена что так неявно приведется
            this.pathToPicture = Path.Combine(workingDirectory, $"{}");
        }
        
        /// <summary>
        /// Save layer
        /// </summary>
        /// <param name="WorkingDirectory"></param>
        /// <returns></returns>
        public override void Save(string WorkingDirectory) {
            string pathToFile = Path.Combine(WorkingDirectory, $"{Name}.png");
            image.Save(pathToFile);
        }
    }
}
