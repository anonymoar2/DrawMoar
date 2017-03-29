using System.Windows;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using System.Drawing;
using System;

namespace DrawMoar.Shapes
{
    public class Rectangle : IShape
    {
        public System.Windows.Point Center { get; private set; }
        public System.Windows.Size Size { get; private set; }
        public double StartAngle { get; private set; }
        public double EndAngle { get; private set; }
        public double Rotate { get; private set; }

        public string Alias { get; set; }
        public double Thickness { get; set; }
        public DrawMoar.BaseElements.Color Color { get; set; }

        public Rectangle(System.Windows.Point center, System.Windows.Size size, double startAngle = 0, 
                                                  double endAngle = 360, double rotate = 0) {
            this.Center = center;
            this.Size = size;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
            this.Rotate = rotate;
            this.Thickness = GlobalState.BrushSize.Width;
            this.Color = new BaseElements.Color(GlobalState.Color);
        }


        public void Draw(Canvas canvas) {
            var rect = new System.Windows.Shapes.Rectangle();
            rect.Width = Size.Width;
            rect.Height = Size.Height;
            rect.Stroke = Color.ToBrush();
            rect.StrokeThickness = Thickness;
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, Center.X - Size.Width / 2);
            Canvas.SetTop(rect, Center.Y - Size.Height / 2);
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
            g.DrawRectangle(new Pen(Color.ToDrawingColor(), (float)this.Thickness), new System.Drawing.Rectangle(new System.Drawing.Point(Convert.ToInt32(Center.X-Size.Width/2), Convert.ToInt32(Center.Y-Size.Height/2)), new System.Drawing.Size(Convert.ToInt32(Size.Width), Convert.ToInt32(Size.Height))));
        }
    }
}
