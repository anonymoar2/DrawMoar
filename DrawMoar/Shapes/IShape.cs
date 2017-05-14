using System.Windows.Controls;
using System.Drawing;
using System;
using DrawMoar.BaseElements;
using DrawMoar.Drawing;

namespace DrawMoar.Shapes
{
    public interface IShape : ICloneable
    {
        string Alias { get; set; }

        DrawMoar.BaseElements.Color Color { get; set; }

        void Transform(Transformation trans);

        double Thickness { get; set; }

        void Draw(IDrawer drawer);
    }
}
