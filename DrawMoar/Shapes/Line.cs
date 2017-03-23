using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

using DrawMoar.BaseElements;


namespace DrawMoar.Shapes
{
    public class Line : IShape
    {
        public Point PointOne { get; private set; }
        public Point PointTwo { get; private set; }
        public System.Windows.Media.Brush Stroke { get; set; }
        public double Thickness { get; set; }

        public string Alias { get; set; }


        public Line(Point pointOne, Point pointTwo) {
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
    }
}
