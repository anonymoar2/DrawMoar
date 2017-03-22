using System.Windows;
using System.Windows.Controls;

using DrawMoar.BaseElements;


namespace DrawMoar.Shapes
{
    public class Rectangle : IShape
    {
        public Point Center { get; private set; }
        public Size Size { get; private set; }
        public double StartAngle { get; private set; }
        public double EndAngle { get; private set; }
        public double Rotate { get; private set; }

        public string Alias { get; set; }


        public Rectangle(Point center, Size size, float startAngle = 0, 
                                                  float endAngle = 360, float rotate = 0) {
            this.Center = center;
            this.Size = size;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
            this.Rotate = rotate;
        }


        public void Draw(Canvas canvas) {
            var rect = new System.Windows.Shapes.Rectangle();
            rect.Width = Size.Width;
            rect.Height = Size.Height;
            rect.Stroke = GlobalState.Color;
            rect.StrokeThickness = GlobalState.BrushSize.Width;
            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, Center.X - Size.Width / 2);
            Canvas.SetTop(rect, Center.Y - Size.Height / 2);
        }


        public void Print() {

        }


        public void Transform(Transformation transformation) {
            Point translation, scale;
            double rotation;

            transformation.Decompose(out translation, out scale, out rotation);

            Center = transformation[Center];
            Size = new Size(Size.Width * scale.X, Size.Height * scale.Y);
            Rotate = (Rotate + rotation) % 360;
        }
    }
}
