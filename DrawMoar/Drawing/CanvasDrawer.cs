using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawMoar.Drawing {
    public class CanvasDrawer : IDrawer {

        Canvas canvas;
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
            ellipse.StrokeThickness = GlobalState.BrushSize.Width;
            Canvas.SetLeft(ellipse, el.Center.X - el.Size.Width / 2);
            Canvas.SetTop(ellipse, el.Center.Y - el.Size.Height / 2);
            RotateTransform rotateTransform1 =
                new RotateTransform(el.Rotate);
            rotateTransform1.CenterX = el.Size.Width / 2;
            rotateTransform1.CenterY = el.Size.Height / 2;
            ellipse.RenderTransform = rotateTransform1;
            canvas.Children.Add(ellipse);
        }

        public void DrawRectangle(Shapes.Rectangle rect) {
            var rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.Width = rect.Size.Width;
            rectangle.Height = rect.Size.Height;
            rectangle.Stroke = rect.Color.ToBrush();
            rectangle.StrokeThickness = rect.Thickness;
            rectangle.IsEnabled = false;
            Canvas.SetLeft(rectangle, rect.Center.X - rect.Size.Width / 2);
            Canvas.SetTop(rectangle, rect.Center.Y - rect.Size.Height / 2);
            RotateTransform rotateTransform1 =
                new RotateTransform(rect.Rotate);
            rotateTransform1.CenterX = rect.Size.Width / 2;
            rotateTransform1.CenterY = rect.Size.Height / 2;
            rectangle.RenderTransform = rotateTransform1;
            canvas.Children.Add(rectangle);
        }

        public void DrawImage(System.Drawing.Image image, double x, double y) {

        }
    }
}
