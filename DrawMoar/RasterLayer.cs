using System;

using System.Drawing;
using System.Text.RegularExpressions;

using DrawMoar.BaseElements;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace DrawMoar
{
    /// TODO: Реализовать растровый слой
    public class RasterLayer : ILayer
    {
        /// <summary>
        /// true - слой видимый, false - невидимый
        /// </summary>
        public bool Visible { get; set; }


        /// <summary>
        /// Содержимое слоя, TODO: Подумать, в чем хранить, потому что пока это очень плохо
        /// </summary>
        public Picture Picture { get; set; }


        /// <summary>
        /// Название (имя) слоя
        /// </summary>
        private string name;
        public string Name {
            get { return name; }
            set {
                // TODO: Изменить регулярное выражение на более подходящее
                if (Regex.IsMatch(value, @"\w")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Название слоя должно состоять только " +
                                                "из латинских букв и цифр.");
                }
            }
        }


        //TODO: Конструктор


        /// <summary>
        /// Тут пока только на самом Image рисует, надо ещё с canvas связать
        /// </summary>
        public void Draw(Graphics g) {
            Picture.Draw(g);
        }


        //При переключении слоёв рисует содержимое Image на экране
        public void Print() {

        }


        /// <summary>
        /// TODO: хранить в растрово слое какой-то свой класс Picture, который и будет 
        /// трансформироваться и Image в себе содержать или ещё что-то такое
        /// </summary>
        /// <param name="transformation"></param>
        public void Transform(Transformation transformation) {
            Picture = transformation.Apply(Picture);
        }


        public void Save(Canvas canvas) {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
            (int)canvas.Width, (int)canvas.Height,
            96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new System.Windows.Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new System.Windows.Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(/*your path + filename.png*/"")) {
                encoder.Save(file);
            }

            Picture.Image = System.Drawing.Image.FromFile(/*your path + filename.png again*/"");
        }
    }
}

