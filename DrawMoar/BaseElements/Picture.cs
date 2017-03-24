using System;

using System.Drawing;


namespace DrawMoar.BaseElements
{
    public class Picture
    {
        public System.Windows.Point Position { get; set; }


        public Image Image { get; set; }


        public Picture() {
            Position = new System.Windows.Point(0, 0);
            Image = null;
            // may be Image = Image.GetThumbnailImage()
        }


        public void Draw(Graphics g) {
            g.DrawImage(Image, Convert.ToSingle(Position.X), Convert.ToSingle(Position.Y));
        }
        

    }
}
