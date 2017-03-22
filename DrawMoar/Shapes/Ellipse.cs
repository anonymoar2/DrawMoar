using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using DrawMoar.BaseElements;

namespace DrawMoar.Shapes
{
    public class Ellipse : IShape
    {
        public Point Center { get; private set; }
        public Size Size { get; private set; }
        public double StartAngle { get; private set; }
        public double EndAngle { get; private set; }
        public double Rotate { get; private set; }

        public string Alias { get; set; }

        public Ellipse(Point center, Size size,
            float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            this.Center = center;
            this.Size = size;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
            this.Rotate = rotate;
        }

        public void Draw(Canvas canvas)
        {
            var ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Width = Size.Width;
            ellipse.Height = Size.Height;
            ellipse.Stroke = GlobalState.Color;
            ellipse.StrokeThickness = GlobalState.BrushSize.Width;
            canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, Center.X-Size.Width/2);     //для добавления центра в место клика вместо верхнего левого угла
            Canvas.SetTop(ellipse, Center.Y-Size.Height/2);     //СКОРЕЕ ВСЕГО ПРИДЕТСЯ ПЕРЕДЕЛЫВАТЬ ПОД ДРУГОЙ ЭЛЛИПС 
        }

        public void Print()
        {

        }

        public void Transform(Transformation transformation)
        {
            Point translation, scale;
            double rotation;

            transformation.Decompose(out translation, out scale, out rotation);

            Center = transformation[Center];
            Size = new Size(Size.Width * scale.X, Size.Height * scale.Y);
            Rotate = (Rotate + rotation) % 360;
        }
    }
}
