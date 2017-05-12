using System;

using System.Drawing;
using DrawMoar.Shapes;


namespace DrawMoar.Drawing
{
    public class GraphicsDrawer : IDrawer
    {
        private Graphics graphics;

        public GraphicsDrawer(Graphics graphics) {
            this.graphics = graphics;
        }

        public void DrawLine(Line line) {
            graphics.DrawLine(
                new Pen(line.Color.ToDrawingColor(), (float)line.Thickness),
                Convert.ToSingle(line.PointOne.X),
                Convert.ToSingle(line.PointOne.Y),
                Convert.ToSingle(line.PointTwo.X),
                Convert.ToSingle(line.PointTwo.Y)
            );
        }

        public void DrawEllipse(Ellipse el) {
            graphics.TranslateTransform((float)(el.Center.X), (float)(el.Center.Y));
            graphics.RotateTransform((float)el.Rotate);
            graphics.TranslateTransform((float)(-el.Center.X), (float)(-el.Center.Y));
            graphics.DrawEllipse(
                new Pen(
                    el.Color.ToDrawingColor(),
                    (float)el.Thickness
                ),
                new RectangleF(
                    new PointF(
                        Convert.ToSingle(el.Center.X - el.Size.Width / 2),
                        Convert.ToSingle(el.Center.Y - el.Size.Height / 2)
                    ),
                    new SizeF(Convert.ToSingle(el.Size.Width), Convert.ToSingle(el.Size.Height))
                )
            );
        }

        public void DrawRectangle(Shapes.Rectangle rect) {
            graphics.TranslateTransform((float)(rect.Center.X), (float)(rect.Center.Y));
            graphics.RotateTransform((float)rect.Rotate);
            graphics.TranslateTransform((float)(-rect.Center.X), (float)(-rect.Center.Y));
            graphics.DrawRectangle(
                new Pen(rect.Color.ToDrawingColor(), (float)rect.Thickness),
                new System.Drawing.Rectangle(
                    new Point(
                        Convert.ToInt32(rect.Center.X - rect.Size.Width / 2),
                        Convert.ToInt32(rect.Center.Y - rect.Size.Height / 2)),
                        new Size(Convert.ToInt32(rect.Size.Width), Convert.ToInt32(rect.Size.Height))
                )
            );
        }

        public void DrawImage(Image image, double x, double y) {
            graphics.DrawImage(image, new Point((int)x, (int)y));
        }
    }
}
