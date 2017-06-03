using System.Collections.Generic;

using System.Drawing;
using System.Windows.Controls;

using DrawMoar.Shapes;
using DrawMoar.Drawing;
using System;

namespace DrawMoar.BaseElements
{
    public class CompoundShape : IShape
    {
        public System.Windows.Point centre {
            get {
                return shapes[0].centre;
            }

            set {
                Center = value;
            }
        }
        public string Alias { get; set; }

        public double Thickness { get; set; }

        public Color Color { get; set; }
        private System.Windows.Point Center;
        public List<IShape> shapes = new List<IShape>();


        public void Draw(IDrawer drawer) {
            foreach (var item in shapes) {
                item.Draw(drawer);
            }
        }


        public void Transform(Transformation transformation) {
            foreach (var shape in shapes) {
                shape.Transform(transformation);
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

        public List<string> SaveToFile(string pathToDrm) {
            List<string> lines = new List<string>();
            foreach (var shape in shapes) {
                lines.AddRange(shape.SaveToFile(pathToDrm));
            }
            return lines;
        }
    }
}