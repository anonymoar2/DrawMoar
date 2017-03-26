using System;

using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;


namespace DrawMoar.BaseElements.Shapes
{
    /// <summary>
    /// You know that must be here
    /// </summary>
    class Line
    {
        public string Alias { get; set; }
        public System.Windows.Point PointOne { get; private set; }
        public System.Windows.Point PointTwo { get; private set; }
        public System.Windows.Media.Brush Stroke { get; set; }
        public double Thickness { get; set; }

        public Line(System.Windows.Point pointOne, System.Windows.Point pointTwo) {
            this.PointOne = pointOne;
            this.PointTwo = pointTwo;
        }

        public Line(System.Windows.Point pointOne, System.Windows.Point pointTwo, System.Windows.Media.Brush stroke, double thickness) {
            this.PointOne = pointOne;
            this.PointTwo = pointTwo;
            this.Stroke = stroke;
            this.Thickness = thickness;
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
            PointOne = transformation[PointOne];
            PointTwo = transformation[PointTwo];
        }
    }
}
