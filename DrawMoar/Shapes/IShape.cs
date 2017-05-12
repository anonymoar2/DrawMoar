using System;
using DrawMoar.Drawing;
using DrawMoar.BaseElements;


namespace DrawMoar.Shapes
{
    public interface IShape : ICloneable
    {
        string Alias { get; set; }

        double Thickness { get; set; }

        BaseElements.Color Color { get; set; }

        void Draw(IDrawer drawer);

        void Transform(Transformation trans);
    }
}
