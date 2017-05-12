using System;

using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;
using DrawMoar.Drawing;


namespace DrawMoar {
    public class RasterLayer : ILayer {

        public bool Visible { get; set; }

        public Picture Picture { get; set; }

        private string name;
        public string Name {
            get { return name; }
            set {
                
                if (Regex.IsMatch(value, @"\w")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Название слоя должно состоять только " +
                                                "из латинских букв и цифр.");
                }
            }
        }


        public System.Windows.Point Position {
            get {
                return Picture.Position;
            }

            set {
                Picture.Position = value;
            }
        }


        public RasterLayer() {
            name = $"RasterLayer_{Cartoon.CurrentFrame.layers.Count}";
            Visible = true;
            Picture = new Picture();
        }


        public RasterLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new Picture();
        }


        public void Draw(IDrawer drawer) {
            Picture.Draw(drawer);
        }


        public void Transform(Transformation transformation) {
            Picture = transformation.Apply(Picture);
        }


        public void Save(Canvas canvas) {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)canvas.Width, (int)canvas.Height,
                96d, 96d,
                PixelFormats.Pbgra32
            );

            if (Picture.Image != null) {
                Picture.Image.Dispose();
            }
            canvas.Measure(new System.Windows.Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new System.Windows.Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);      

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            var filename = Path.Combine(
                GlobalState.WorkingDirectory, 
                $"{GlobalState.CurrentScene.Name}_{GlobalState.CurrentFrame.Name}_{Name}.png"
            );
            using (Stream stm = File.Create(filename)) {
                encoder.Save(stm);
            }
            Picture.Image = System.Drawing.Image.FromFile(filename);
        }


        public System.Windows.Controls.Image ConvertDrawingImageToWPFImage(System.Drawing.Image gdiImg) {

            if (gdiImg == null) return null;
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

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

        }


        public bool ThumbnailCallback() {
            return false;
        }


        public System.Drawing.Image Miniature(int width, int height) {
            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            var newImage = Picture.Image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            return newImage;
        }


        public System.Drawing.Image GetImage(double height, double width) {
            throw new NotImplementedException();
        }


        public object Clone() {
            var buf = new RasterLayer()
            {
                Visible = Visible,
                Picture = (Picture)Picture.Clone(),
                Name = Name,
                Position = Position
            };
            return buf;
        }


        private void DrawRasterLayerImage(RasterLayerControl rlc)
        {
            var bmp = Picture.Image;
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
                rlc.Image.Source = bi;
            }
        }
    }
}

