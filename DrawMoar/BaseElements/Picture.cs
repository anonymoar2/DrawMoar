using System;

using System.Drawing;


namespace DrawMoar.BaseElements
{
    public class Picture
    {
        public System.Windows.Point Position { get; set; }

        public Image Image { get; set; }

        public void Draw(Graphics g) {
            g.DrawImage(Image, Convert.ToSingle(Position.X), Convert.ToSingle(Position.Y));
        }
        
    }
}
