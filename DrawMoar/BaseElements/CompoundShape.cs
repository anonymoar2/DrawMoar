using System.Collections.Generic;

using System.Windows.Controls;

using DrawMoar.Shapes;
using System.Drawing;


namespace DrawMoar.BaseElements
{
    public class CompoundShape : IShape
    {
        public string Alias { get; set; }

        public double Thickness { get; set; }

        public Color Color { get; set; }

        public List<IShape> shapes = new List<IShape>();

        public void Draw(Canvas canvas) {
            foreach (var item in shapes) {
                item.Draw(canvas);
            }
        }
      
        public void Print()
        {

        }

        public void Transform(Transformation transformation) {
            foreach (var shape in shapes) {
                shape.Transform(transformation);
            }
        }

        
        public void Draw(Graphics g) {
            foreach(var shape in shapes) {
                shape.Draw(g);
            }
        }

        public object Clone()
        {
            var buf = new CompoundShape();
            buf.Alias = Alias;
            buf.Thickness = Thickness;
            buf.Color = Color;
            
            foreach(var shape in shapes)
            {
                buf.shapes.Add((IShape)shape.Clone());
            }

            return buf;
        }
    }
}