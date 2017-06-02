using DrawMoar.Shapes;
using DrawMoar.BaseElements;


namespace DrawMoar.Drawing {

    public interface IDrawer {

        void DrawLine(Line line);

        void DrawEllipse(Ellipse ellipse);

        void DrawRectangle(Shapes.Rectangle rectangle);

        void DrawPicture(Picture pic, double x, double y);
    }
}
