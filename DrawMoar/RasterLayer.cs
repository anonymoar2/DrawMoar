using System;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using DrawMoar.Extensions;

namespace DrawMoar
{
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


        public RasterLayer() {
            name = "newRasterLayer";
            Visible = true;
            Picture = new Picture();
        }


        public RasterLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new Picture();
        }


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
        /// Трансформация содержимого растрового слоя
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

            using (FileStream file = File.Create(GlobalState.WorkingDirectory + $"\\{Name}.png")) {
                encoder.Save(file);
            }

            var img = System.Drawing.Image.FromFile(GlobalState.WorkingDirectory+ $"\\{Name}.png");
            Picture.Image=ObjectCopier.Clone(img);
            img = null;
        }


        public System.Windows.Controls.Image ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg) {

            if (gdiImg == null) return null;
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            Bitmap bmp = new Bitmap(gdiImg);
            IntPtr hBitmap = bmp.GetHbitmap();
            ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, 
                                    Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            img.Source = WpfBitmap;
            img.Width = gdiImg.Width;
            img.Height = gdiImg.Height;
            img.Stretch = Stretch.Fill;
            return img;
        }

        public void AddShape(IShape shape) {
            var g = Graphics.FromImage(Picture.Image);
            // как-то создаем графикс
            //shape.метод кот приним гр и нарис нужн фиг с нужными параметрами
            // а этот метод есть в любой IShape
        }
    }
}

