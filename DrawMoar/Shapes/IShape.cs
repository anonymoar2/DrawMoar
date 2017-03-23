using System.Windows.Controls;
using System.Drawing;

using DrawMoar.BaseElements;


namespace DrawMoar.Shapes
{
    public interface IShape
    {
        string Alias { get; set; }

        void Transform(Transformation trans);

        void Draw(Canvas canvas);

        void Print();

        void Draw(Graphics g);
    }
}
