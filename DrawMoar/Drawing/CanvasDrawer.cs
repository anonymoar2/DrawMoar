using System;

using System.IO;
using DrawMoar.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace DrawMoar.Drawing
{
    public class CanvasDrawer : IDrawer
    {
        private Canvas canvas;


        public CanvasDrawer(Canvas canv) {
            canvas = canv;
        }


        public void DrawLine(Line line) {
            canvas.Children.Add(new System.Windows.Shapes.Line {
                Stroke = line.Color.ToBrush(),
                StrokeThickness = line.Thickness,
                X1 = line.PointOne.X,
                Y1 = line.PointOne.Y,
                X2 = line.PointTwo.X,
                Y2 = line.PointTwo.Y,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                IsEnabled = false
            });
        }


        public void DrawEllipse(Ellipse el) {
            var ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Width = el.Size.Width;
            ellipse.Height = el.Size.Height;
            ellipse.Stroke = el.Color.ToBrush();
            ellipse.IsEnabled = false;
            ellipse.StrokeThickness = MainWindow.BrushSize.Width;
            Canvas.SetLeft(ellipse, el.Center.X - el.Size.Width / 2);
            Canvas.SetTop(ellipse, el.Center.Y - el.Size.Height / 2);
            RotateTransform rotateTransform1 = new RotateTransform(el.Rotate) {
                CenterX = el.Size.Width / 2,
                CenterY = el.Size.Height / 2
            };
            ellipse.RenderTransform = rotateTransform1;
            canvas.Children.Add(ellipse);
        }


        public void DrawRectangle(Rectangle rect) {
            var rectangle = new System.Windows.Shapes.Rectangle() {
                Width = rect.Size.Width,
                Height = rect.Size.Height,
                Stroke = rect.Color.ToBrush(),
                StrokeThickness = rect.Thickness,
                IsEnabled = false
            };
            Canvas.SetLeft(rectangle, rect.Center.X - rect.Size.Width / 2);
            Canvas.SetTop(rectangle, rect.Center.Y - rect.Size.Height / 2);
            RotateTransform rotateTransform1 = new RotateTransform(rect.Rotate) {
                CenterX = rect.Size.Width / 2,
                CenterY = rect.Size.Height / 2
            };
            rectangle.RenderTransform = rotateTransform1;
            canvas.Children.Add(rectangle);
        }


        public void DrawImage(System.Drawing.Image image, double x, double y) {
            var rlc = new RasterLayerControl();
            DrawRasterLayerImage(image, rlc);
            canvas.Children.Add(rlc);
            Canvas.SetLeft(rlc, x);
            Canvas.SetTop(rlc, y);
        }


        private void DrawRasterLayerImage(System.Drawing.Image image, RasterLayerControl rlc) {
            var bmp = image;
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
    }
}
