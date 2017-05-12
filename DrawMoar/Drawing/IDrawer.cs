using DrawMoar.Shapes;
using System.Drawing;

namespace DrawMoar.Drawing
{
    public interface IDrawer {
        void DrawLine(Line line);
        void DrawEllipse(Ellipse ellipse);
        void DrawRectangle(Shapes.Rectangle rectangle);
        void DrawImage(Image image, double x, double y);
    }
}
