using System;

using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

using DrawMoar.BaseElements;


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
            rect.IsEnabled = false;   
            Canvas.SetLeft(rect, Center.X - Size.Width / 2);
            Canvas.SetTop(rect, Center.Y - Size.Height / 2);
            RotateTransform rotateTransform1 =
                new RotateTransform(Rotate);
            rotateTransform1.CenterX = Size.Width / 2;
            rotateTransform1.CenterY = Size.Height / 2;
            rect.RenderTransform = rotateTransform1;
            canvas.Children.Add(rect);
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
            g.TranslateTransform((float)(Center.X), (float)(Center.Y));
            g.RotateTransform((float)Rotate);
            g.TranslateTransform((float)(-Center.X), (float)(-Center.Y));
            g.DrawRectangle(new System.Drawing.Pen(Color.ToDrawingColor(), (float)this.Thickness), 
                            new System.Drawing.Rectangle(new Point(Convert.ToInt32(Center.X-Size.Width/2), 
                                                         Convert.ToInt32(Center.Y-Size.Height/2)), 
                                                         new Size(Convert.ToInt32(Size.Width), 
                                                         Convert.ToInt32(Size.Height))
                                                         )
                           );
        }

        public object Clone()
        {
            var buf = new Rectangle(Center, Size, StartAngle, EndAngle, Rotate);
            buf.Alias = Alias;
            buf.Thickness = Thickness;
            buf.Color = Color;
            return buf;
        }
    }
}
