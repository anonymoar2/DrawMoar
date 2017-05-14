using System;

using System.Drawing;

using DrawMoar.BaseElements;


using System.Windows.Controls;
using DrawMoar.Drawing;

namespace DrawMoar
{
    public interface ILayer : ICloneable
    {
        bool Visible { get; set; }

        string Name { get; set; }

        void Draw(IDrawer drawer);

        void Transform(Transformation transformation);
    }
}
