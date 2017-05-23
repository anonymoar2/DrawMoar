using System;

using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using DrawMoar.Drawing;
using System.Collections.Generic;

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
            this.Thickness = MainWindow.BrushSize.Width;
            this.Color = new BaseElements.Color(MainWindow.Color);
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


        public object Clone()
        {
            var buf = new Rectangle(Center, Size, StartAngle, EndAngle, Rotate);
            buf.Alias = Alias;
            buf.Thickness = Thickness;
            buf.Color = Color;
            return buf;
        }

        
        public List<string> SaveToFile(string pathToDrm) {
            return new List<string>() { $"\t\t\t\tShape**rectangle[{Center.X};{Center.Y};{Thickness};{Color.ToString()};{Size.Width};{Size.Height};{StartAngle};{EndAngle};{Rotate}]" };
        }
    }
}
