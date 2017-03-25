using System;

using System.Text.RegularExpressions;
using System.Drawing;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System.Windows;

namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        /// <summary>
        /// true - видимый слой, false - невидимый
        /// </summary>
        public bool Visible { get; set; }


        /// <summary>
        /// Совокупность всех наших фигур и есть пикча - содержимое слоя в общем
        /// </summary>
        public CompoundShape Picture { get; set; }


        /// <summary>
        /// Название (имя) слоя
        /// </summary>
        private string name;
        public string Name {
            get { return name; }
            set {
                // TODO: Изменить регулярное выражение на более подходящее
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Название слоя должно состоять только " +
                                                "из латинских букв и цифр.");
                }
            }
        }


        private System.Windows.Point position = new System.Windows.Point(0, 0);
        public System.Windows.Point Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }


        public VectorLayer() {
            name = "newVectorLayer";
            Visible = true;
            Picture = new CompoundShape();
        }


        public VectorLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new CompoundShape();
        }

        
        public void Draw(Graphics g) {
            Picture.Draw(g);
        }


        /// <summary>
        /// Тупо вывод на экране при переключении между слоями, но его не будет, если канвасы накладываем друг на друга
        /// </summary>
        /// <param name="bitmap"></param>
        public void Print() {
            // Проходим по фигурам, отрисовывая их на экране
        }


        public void Transform(Transformation transformation) {
            Picture.Transform(transformation);
        }


        public void AddShape(IShape shape) {
            Picture.shapes.Add(shape);
            // взм тут отрисовка на канвасе 
        }


        public RasterLayer ToRasterLayer() {
            var newLayer = new RasterLayer();
            var g = Graphics.FromImage(newLayer.Picture.Image);
            Picture.Draw(g);
            return newLayer;
        }


        public bool ThumbnailCallback() {
            return false;
        }


        public System.Drawing.Image Miniature(int width, int height) {
            var rasterLayer = ToRasterLayer();
            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            var newImage = rasterLayer.Picture.Image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            return newImage;
        }

        public Bitmap GetImage() {
            Bitmap b = new Bitmap(450, 450);
            b.
            var g = Graphics.FromImage(b);
            foreach(var sh in Picture.shapes) {
                sh.Draw(g);
            }
            return b;
        }
    }
}
