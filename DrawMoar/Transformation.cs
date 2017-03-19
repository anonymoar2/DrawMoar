using System;
using System.Drawing;

namespace DrawMoar
{
    public interface ITransformation
    {
        Point this [Point p] { get; }
    }
}
