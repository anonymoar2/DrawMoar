using System;

using System.Drawing;
using System.Windows.Media;
using System.Windows.Controls;

using DrawMoar.BaseElements;
using DrawMoar.Drawing;
using System.Collections.Generic;
using System.Windows;

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

        public System.Windows.Point centre {
            get {
                return Center;
            }

            set {
                Center = value;
            }
        }

        public Line(System.Windows.Point pointOne, System.Windows.Point pointTwo) {
            this.PointOne = pointOne;
            this.PointTwo = pointTwo;
            this.Center = new System.Windows.Point(Math.Abs(PointOne.X - PointTwo.X) / 2, Math.Abs(PointOne.Y - PointTwo.Y) / 2);
            Thickness = MainWindow.BrushSize.Width;
            Color = new BaseElements.Color(MainWindow.Color);
        }


        public void Draw(IDrawer drawer) {
            drawer.DrawLine(this);
        }


        public void Transform(Transformation transformation) {
            PointOne = transformation.Apply(PointOne);
            PointTwo = transformation.Apply(PointTwo);
        }


        public object Clone()
        {
            var buf = new Line(PointOne, PointTwo);
            buf.Thickness = Thickness;
            buf.Alias = Alias;
            buf.Color = Color;
            return buf;
        }

        public List<string> SaveToFile(string pathToDrm) {
            return new List<string>() { $"Shape*line*{PointOne.X};{PointOne.Y};{PointTwo.X};{PointTwo.Y};{Thickness};{Color.ToString()}" };
        }
    }
}
