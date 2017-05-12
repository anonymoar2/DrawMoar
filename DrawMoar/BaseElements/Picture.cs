﻿using System;

using System.Drawing;
using DrawMoar.Drawing;


namespace DrawMoar.BaseElements
{
    public class Picture : ICloneable
    {
        public System.Windows.Point Position { get; set; }
        public Image Image { get; set; }


        public Picture() {
            Position = new System.Windows.Point(0, 0);
            Image = null;
        }


        public void Draw(IDrawer drawer) {
            drawer.DrawImage(Image, Convert.ToSingle(Position.X), Convert.ToSingle(Position.Y));
        }


        public object Clone() {
            var buf = new Picture() {
                Position = Position,
                Image = (Image)Image.Clone()
            };
            return buf;
        }
    }
}
