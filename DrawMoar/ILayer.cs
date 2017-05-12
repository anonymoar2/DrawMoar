using System;

using DrawMoar.BaseElements;
using DrawMoar.Drawing;


namespace DrawMoar
{
    public interface ILayer : ICloneable
    {
        string Name { get; set; }

        bool Visible { get; set; }

        void Draw(IDrawer drawer);

        void Transform(Transformation transformation);
    }
}
