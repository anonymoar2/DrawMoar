using System;

using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using DrawMoar.Drawing;


namespace DrawMoar.Shapes
{
    public class Line : IShape
    {
        public System.Windows.Point PointOne { get; private set; }
        public System.Windows.Point PointTwo { get; private set; }
        public System.Windows.Point Center { get; private set; }
        public double Thickness { get; set; }

        public string Alias { get; set; }
        public DrawMoar.BaseElements.Color Color { get; set; }


        public Line(System.Windows.Point pointOne, System.Windows.Point pointTwo) {
            PointOne = pointOne;
            PointTwo = pointTwo;
            Center = new System.Windows.Point(
                Math.Abs(PointOne.X - PointTwo.X) / 2,
                Math.Abs(PointOne.Y - PointTwo.Y) / 2
            );
            Thickness = GlobalState.BrushSize.Width;
            Color = new BaseElements.Color(GlobalState.Color);
        }


        public void Draw(IDrawer drawer) {
            drawer.DrawLine(this);
        }


        public void Transform(Transformation transformation) {
            PointOne = transformation.Apply(PointOne);
            PointTwo = transformation.Apply(PointTwo);
        }


        public object Clone() {
            var buf = new Line(PointOne, PointTwo) {
                Thickness = Thickness,
                Alias = Alias,
                Color = Color
            };
            return buf;
        }
    }
}
