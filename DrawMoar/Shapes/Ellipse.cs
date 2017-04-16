using System.Windows;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using System.Drawing;
using System;
using System.Windows.Media;

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
        public DrawMoar.BaseElements.Color Color { get; set; }

        public Ellipse(System.Windows.Point center, System.Windows.Size size, double startAngle = 0,
                                                double endAngle = 360, double rotate = 0) {
            this.Center = center;
            this.Size = size;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
            this.Rotate = rotate;
            this.Thickness = GlobalState.BrushSize.Width;
            this.Color = new DrawMoar.BaseElements.Color(GlobalState.Color);
        }


        public void Draw(Canvas canvas) {
            var ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Width = Size.Width;
            ellipse.Height = Size.Height;
            ellipse.Stroke = Color.ToBrush();
            ellipse.StrokeThickness = GlobalState.BrushSize.Width;
            Canvas.SetLeft(ellipse, Center.X - Size.Width / 2);
            Canvas.SetTop(ellipse, Center.Y - Size.Height / 2);
            RotateTransform rotateTransform1 =
                new RotateTransform(Rotate);
            rotateTransform1.CenterX = Size.Width/2;
            rotateTransform1.CenterY = Size.Height/2;
            ellipse.RenderTransform = rotateTransform1;        
            canvas.Children.Add(ellipse); 
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
            g.DrawEllipse(new System.Drawing.Pen(Color.ToDrawingColor(), (float)this.Thickness), new RectangleF(new PointF(Convert.ToSingle(Center.X - Size.Width / 2), Convert.ToSingle(Center.Y - Size.Height / 2)), new SizeF(Convert.ToSingle(Size.Width), Convert.ToSingle(Size.Height))));
        }

        public object Clone()
        {
            var buf = new Ellipse(Center, Size, StartAngle, EndAngle, Rotate);
            buf.Alias = Alias;
            buf.Thickness = Thickness;
            buf.Color = Color;
            return buf;
        }
    }
}
