using System.Drawing;


namespace DrawMoar.BaseElements
{
    public interface ITransformation
    {
        Point this[Point p] { get; }
    }
}