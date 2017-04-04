using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Drawing;

using DrawMoar.BaseElements;
using System;

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
            this.PointOne = pointOne;
            this.PointTwo = pointTwo;
            this.Center = new System.Windows.Point(Math.Abs(PointOne.X - PointTwo.X) / 2, Math.Abs(PointOne.Y - PointTwo.Y) / 2);
            Thickness = GlobalState.BrushSize.Width;
            Color = new BaseElements.Color(GlobalState.Color);
        }


        public void Draw(Canvas canvas) {
            canvas.Children.Add(new System.Windows.Shapes.Line {
                Stroke = Color.ToBrush(),
                StrokeThickness = Thickness,
                X1 = PointOne.X,
                Y1 = PointOne.Y,
                X2 = PointTwo.X,
                Y2 = PointTwo.Y,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round
            });
        }


        public void Print() {

        }


        public void Transform(Transformation transformation) {
            PointOne = transformation.Apply(PointOne);
            PointTwo = transformation.Apply(PointTwo);
        }

        public void Draw(Graphics g) {
            g.DrawLine(new System.Drawing.Pen(Color.ToDrawingColor(), (float)this.Thickness), Convert.ToSingle(PointOne.X), Convert.ToSingle(PointOne.Y), Convert.ToSingle(PointTwo.X), Convert.ToSingle(PointTwo.Y));
        }

        public object Clone()
        {
            var buf = new Line(PointOne, PointTwo);
            buf.Thickness = Thickness;
            buf.Alias = Alias;
            buf.Color = Color;
            return buf;
        }
    }
}
