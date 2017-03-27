using System.Windows;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using System.Drawing;
using System;

namespace DrawMoar.Shapes
{
    public class Ellipse : IShape
    {
        public System.Windows.Point Center { get; private set; }
        public System.Windows.Size Size { get; private set; }
        public double StartAngle { get; private set; }
        public double EndAngle { get; private set; }
        public double Rotate { get; private set; }

        public string Alias { get; set; }
        public double Thickness { get; set; }

        public Ellipse(System.Windows.Point center, System.Windows.Size size, double startAngle = 0,
                                                double endAngle = 360, double rotate = 0) {
            this.Center = center;
            this.Size = size;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
            this.Rotate = rotate;
            this.Thickness = GlobalState.BrushSize.Width;
        }


        public void Draw(Canvas canvas) {
            var ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Width = Size.Width;
            ellipse.Height = Size.Height;
            ellipse.Stroke = GlobalState.Color;
            ellipse.StrokeThickness = GlobalState.BrushSize.Width;
            canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, Center.X - Size.Width / 2);     //для добавления центра в место клика вместо верхнего левого угла
            Canvas.SetTop(ellipse, Center.Y - Size.Height / 2);     //СКОРЕЕ ВСЕГО ПРИДЕТСЯ ПЕРЕДЕЛЫВАТЬ ПОД ДРУГОЙ ЭЛЛИПС 
        }


        public void Print() {

        }


        public void Transform(Transformation transformation) {
            System.Windows.Point translation, scale;
            double rotation;

            transformation.Decompose(out translation, out scale, out rotation);

            Center = transformation.Apply(Center);
            Size = new System.Windows.Size(Size.Width * scale.X, Size.Height * scale.Y);
            Rotate = (Rotate + rotation) % 360;
        }


        public void Draw(Graphics g) {
            g.DrawEllipse(new Pen(System.Drawing.Color.Red), new RectangleF(new PointF(Convert.ToSingle(Center.X + Size.Width / 2), Convert.ToSingle(Center.Y + Size.Height / 2)), new SizeF(Convert.ToSingle(Size.Width), Convert.ToSingle(Size.Height))));
        }
    }
}
