using System.Drawing;

namespace DrawMoar.BaseElements
{
    public class Text
    {
        public string Txt { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public PointF Position { get; set; }
        public StringFormat Format { get; set; }
        
        public Text(string txt, Font font, Brush brush, PointF position, StringFormat format)
        {
            Txt = txt;
            Font = font;
            Brush = brush;
            Position = position;
            Format = format;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(Txt, Font, Brush, Position, Format);
        }
    }
}
