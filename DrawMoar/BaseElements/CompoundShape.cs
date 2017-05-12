﻿using System.Collections.Generic;

using System.Drawing;
using System.Windows.Controls;

using NLog;
using DrawMoar.Shapes;
using DrawMoar.Drawing;


namespace DrawMoar.BaseElements
{
    public class CompoundShape : IShape
    {
        public string Alias { get; set; }
        public double Thickness { get; set; }
        public Color Color { get; set; }
        public List<IShape> shapes = new List<IShape>();

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


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


        public object Clone() {
            var buf = new CompoundShape() {
                Alias = Alias,
                Thickness = Thickness,
                Color = Color
            };
            foreach (var shape in shapes) {
                buf.shapes.Add((IShape)shape.Clone());
            }

            return buf;
        }
    }
}