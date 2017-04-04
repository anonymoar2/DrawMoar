using System.Windows.Controls;
using System.Drawing;
using System;
using DrawMoar.BaseElements;


namespace DrawMoar.Shapes
{
    public interface IShape : ICloneable
    {
        string Alias { get; set; }

        DrawMoar.BaseElements.Color Color { get; set; }

        void Transform(Transformation trans);

        double Thickness { get; set; }

        void Draw(Canvas canvas);

        void Print();

        void Draw(Graphics g);
    }
}
