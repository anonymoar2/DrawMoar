﻿using System;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System.Collections.Generic;
using Exporter.Photo;

namespace DrawMoar {
    public class RasterLayer : ILayer {
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


        public System.Windows.Point Position {
            get {
                return Picture.Position;
            }

            set {
                Picture.Position = value;
            }
        }

        public List<Text> Text { get; private set; }


        public RasterLayer() {
            name = $"RasterLayer_{GlobalState.CurrentFrame.layers.Count}";
            Visible = true;
            Picture = new Picture();
            Text = new List<Text>();
        }


        public RasterLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new Picture();
            Text = new List<Text>();
        }


        /// <summary>
        /// Тут пока только на самом Image рисует, надо ещё с canvas связать
        /// </summary>
        public void Draw(Graphics g) {
            Picture.Draw(g);
            foreach (var element in Text) {
                element.Draw(g);
            }
        }


        //При переключении слоёв рисует содержимое Image на экране
        public void Print(Canvas canvas) {
            var rlc = new RasterLayerControl();
            DrawRasterLayerImage(rlc);
            canvas.Children.Add(rlc);
            Canvas.SetLeft(rlc, Position.X);
            Canvas.SetTop(rlc, Position.Y);
        }


        private void DrawRasterLayerImage(RasterLayerControl rlc) {       //из-за некоторых вещей нет возможности потестить, работает ли это
            var bmp = ((RasterLayer)GlobalState.CurrentLayer.Item1).Picture.Image;  //если работает, положим в RasterLayer
            using (var ms = new MemoryStream()) {
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
            if(Picture.Image !=null)Picture.Image.Dispose();
            // needed otherwise the image output is black
            canvas.Measure(new System.Windows.Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new System.Windows.Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);      

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            using (Stream stm = File.Create(Path.Combine(GlobalState.WorkingDirectory, $"{GlobalState.CurrentScene.Name}_{GlobalState.CurrentFrame.Name}_{Name}.png"))) {
                encoder.Save(stm);
            }

            Picture.Image = System.Drawing.Image.FromFile(Path.Combine(GlobalState.WorkingDirectory, $"{GlobalState.CurrentScene.Name}_{GlobalState.CurrentFrame.Name}_{Name}.png"));
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

        Bitmap ILayer.GetImage(double height, double width) {
            throw new NotImplementedException();
        }

        public object Clone() {
            var buf = new RasterLayer();
            buf.Visible = Visible;
            buf.Picture = (Picture)Picture.Clone();
            buf.Name = Name;
            buf.Position = Position;

            foreach (var element in Text) {
                buf.Text.Add(element);
            }

            return buf;
        }
    }
}

