using System;

using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using DrawMoar.Drawing;

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
            Center = center;
            Size = size;
            StartAngle = startAngle;
            EndAngle = endAngle;
            Rotate = rotate;
            Thickness = GlobalState.BrushSize.Width;
            Color = new BaseElements.Color(GlobalState.Color);
        }

        public void Draw(IDrawer drawer) {
            drawer.DrawRectangle(this);
        }


        public void Transform(Transformation transformation) {
            System.Windows.Point translation, scale;
            double rotation;

            transformation.Decompose(out translation, out scale, out rotation);

            Center = transformation.Apply(Center);
            Size = new System.Windows.Size(Size.Width * scale.X, Size.Height * scale.Y);
            Rotate = (Rotate + rotation) % 360;
        }


        public object Clone() {
            var buf = new Rectangle(Center, Size, StartAngle, EndAngle, Rotate) {
                Alias = Alias,
                Thickness = Thickness,
                Color = Color
            };
            return buf;
        }
    }
}
