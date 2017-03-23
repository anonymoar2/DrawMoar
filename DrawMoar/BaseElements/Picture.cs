using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
