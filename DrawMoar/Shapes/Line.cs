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
        public System.Windows.Media.Brush Stroke { get; set; }
        public double Thickness { get; set; }

        public string Alias { get; set; }


        public Line(System.Windows.Point pointOne, System.Windows.Point pointTwo) {
            this.PointOne = pointOne;
            this.PointTwo = pointTwo;
            Thickness = GlobalState.BrushSize.Width;
            Stroke = GlobalState.Color;
        }


        public void Draw(Canvas canvas) {
            canvas.Children.Add(new System.Windows.Shapes.Line {
                Stroke = Stroke,
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
            g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Red), Convert.ToSingle(PointOne.X), Convert.ToSingle(PointOne.Y), Convert.ToSingle(PointTwo.X), Convert.ToSingle(PointTwo.Y));
        }
    }
}
