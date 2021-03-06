﻿using System;

using System.Drawing;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;


namespace DrawMoar.Drawing {
    public class GraphicsDrawer : IDrawer {

        Graphics g;

        public GraphicsDrawer(Graphics graph) {
            g = graph;
        }


        public void DrawLine(Line line) {
            g.DrawLine(new System.Drawing.Pen(line.Color.ToDrawingColor(), (float)line.Thickness),
                      Convert.ToSingle(line.PointOne.X),
                      Convert.ToSingle(line.PointOne.Y),
                      Convert.ToSingle(line.PointTwo.X),
                      Convert.ToSingle(line.PointTwo.Y));
        }


        public void DrawEllipse(Ellipse el) {
            g.TranslateTransform((float)(el.Center.X), (float)(el.Center.Y));
            g.RotateTransform((float)el.Rotate);
            g.TranslateTransform((float)(-el.Center.X), (float)(-el.Center.Y));
            g.DrawEllipse(new System.Drawing.Pen(el.Color.ToDrawingColor(), (float)el.Thickness), new RectangleF(new PointF(Convert.ToSingle(el.Center.X - el.Size.Width / 2), Convert.ToSingle(el.Center.Y - el.Size.Height / 2)), new SizeF(Convert.ToSingle(el.Size.Width), Convert.ToSingle(el.Size.Height))));
            g.ResetTransform();
        }


        public void DrawRectangle(Shapes.Rectangle rect) {
            g.TranslateTransform((float)(rect.Center.X), (float)(rect.Center.Y));
            g.RotateTransform((float)rect.Rotate);
            g.TranslateTransform((float)(-rect.Center.X), (float)(-rect.Center.Y));
            g.DrawRectangle(new System.Drawing.Pen(rect.Color.ToDrawingColor(), (float)rect.Thickness),
                            new System.Drawing.Rectangle(new Point(Convert.ToInt32(rect.Center.X - rect.Size.Width / 2),
                                                         Convert.ToInt32(rect.Center.Y - rect.Size.Height / 2)),
                                                         new Size(Convert.ToInt32(rect.Size.Width),
                                                         Convert.ToInt32(rect.Size.Height))
                                                         )
                           );
            g.ResetTransform();
        }


        public void DrawPicture(Picture pic, double x, double y) {
            g.RotateTransform(pic.Angle);
            g.DrawImage(pic.Image, new Point((int)x, (int)y));
            g.ResetTransform();
        }
    }
}
